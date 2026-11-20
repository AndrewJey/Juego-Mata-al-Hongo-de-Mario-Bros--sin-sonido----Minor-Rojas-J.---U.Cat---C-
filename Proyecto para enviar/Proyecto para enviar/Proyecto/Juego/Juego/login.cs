using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Juego
{
    public class PlayerRecord
    {
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public int Puntos { get; set; }
    }

    public class GameSession
    {
        private readonly Random _random;
        public string PlayerName { get; }
        public int Score { get; private set; }
        public int RemainingSeconds { get; private set; }
        public bool IsRunning { get; private set; }

        public GameSession(string playerName, int durationSeconds = 60, int? seed = null)
        {
            PlayerName = string.IsNullOrWhiteSpace(playerName) ? "Invitado" : playerName.Trim();
            RemainingSeconds = Math.Max(1, durationSeconds);
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
            IsRunning = true;
        }

        public void Reset(int durationSeconds = 60)
        {
            Score = 0;
            RemainingSeconds = Math.Max(1, durationSeconds);
            IsRunning = true;
        }

        public int RegisterHit()
        {
            if (!IsRunning)
            {
                return Score;
            }
            Score++;
            return Score;
        }

        public bool TickSecond()
        {
            if (!IsRunning)
            {
                return false;
            }

            if (RemainingSeconds > 0)
            {
                RemainingSeconds--;
            }

            if (RemainingSeconds <= 0)
            {
                IsRunning = false;
                return false;
            }

            return true;
        }

        public System.Drawing.Point NextTargetLocation(int containerWidth, int containerHeight, int targetWidth, int targetHeight, int topMargin = 40)
        {
            int maxX = Math.Max(1, containerWidth - targetWidth - 8);
            int minY = Math.Min(topMargin, Math.Max(0, containerHeight - targetHeight - 8));
            int maxY = Math.Max(minY + 1, containerHeight - targetHeight - 8);
            return new System.Drawing.Point(_random.Next(0, maxX), _random.Next(minY, maxY));
        }
    }

    public static class ScoreRepository
    {
        private static readonly object FileLock = new object();

        public static string GetSqlConnectionString()
        {
            string env = Environment.GetEnvironmentVariable("MARIO_HONGO_CONNECTION_STRING");
            return string.IsNullOrWhiteSpace(env) ? null : env.Trim();
        }

        public static string GetLocalStorePath()
        {
            string envDir = Environment.GetEnvironmentVariable("MARIO_HONGO_DATA_DIR");
            string dir = !string.IsNullOrWhiteSpace(envDir)
                ? envDir.Trim()
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MataAlHongo");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "jugadores.tsv");
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password ?? string.Empty));
                StringBuilder sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool RegisterPlayer(string nombre, string password, out string error)
        {
            error = null;
            string user = (nombre ?? string.Empty).Trim();
            string pass = password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                error = "Debe ingresar un nombre de usuario y una contraseña.";
                return false;
            }

            if (user.Contains("\t") || user.Contains("\r") || user.Contains("\n"))
            {
                error = "El nombre de usuario contiene caracteres no permitidos.";
                return false;
            }

            string connStr = GetSqlConnectionString();
            if (!string.IsNullOrEmpty(connStr))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(connStr))
                    using (SqlCommand check = new SqlCommand("SELECT COUNT(1) FROM Jugador WHERE nombre = @nombre", cn))
                    {
                        check.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = user;
                        cn.Open();
                        int exists = Convert.ToInt32(check.ExecuteScalar());
                        if (exists > 0)
                        {
                            error = "Ese nombre de usuario ya existe.";
                            return false;
                        }

                        using (SqlCommand insert = new SqlCommand(
                            "INSERT INTO Jugador (nombre, [contraseña], puntos) VALUES (@nombre, @pass, 0)", cn))
                        {
                            insert.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = user;
                            insert.Parameters.Add("@pass", SqlDbType.NVarChar, 128).Value = HashPassword(pass);
                            insert.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch
                {
                    // Fall back to local store when SQL Server is unavailable.
                }
            }

            lock (FileLock)
            {
                List<PlayerRecord> players = LoadLocalPlayersUnsafe();
                if (players.Any(p => string.Equals(p.Nombre, user, StringComparison.OrdinalIgnoreCase)))
                {
                    error = "Ese nombre de usuario ya existe.";
                    return false;
                }

                players.Add(new PlayerRecord
                {
                    Nombre = user,
                    PasswordHash = HashPassword(pass),
                    Puntos = 0
                });
                SaveLocalPlayersUnsafe(players);
                return true;
            }
        }

        public static bool ValidateLogin(string nombre, string password)
        {
            string user = (nombre ?? string.Empty).Trim();
            string pass = password ?? string.Empty;
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                return false;
            }

            string hashed = HashPassword(pass);
            string connStr = GetSqlConnectionString();
            if (!string.IsNullOrEmpty(connStr))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT nombre, [contraseña] FROM Jugador WHERE nombre = @nombre", cn))
                    {
                        cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = user;
                        cn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string stored = reader["contraseña"] == DBNull.Value ? string.Empty : reader["contraseña"].ToString();
                                return string.Equals(stored, hashed, StringComparison.OrdinalIgnoreCase)
                                    || string.Equals(stored, pass, StringComparison.Ordinal);
                            }
                        }
                    }
                    return false;
                }
                catch
                {
                    // Fall back to local store.
                }
            }

            lock (FileLock)
            {
                PlayerRecord player = LoadLocalPlayersUnsafe()
                    .FirstOrDefault(p => string.Equals(p.Nombre, user, StringComparison.OrdinalIgnoreCase));
                return player != null && string.Equals(player.PasswordHash, hashed, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool SaveHighScore(string nombre, int points)
        {
            string user = (nombre ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(user) || points < 0)
            {
                return false;
            }

            string connStr = GetSqlConnectionString();
            if (!string.IsNullOrEmpty(connStr))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(connStr))
                    {
                        cn.Open();
                        int previous = 0;
                        using (SqlCommand sel = new SqlCommand("SELECT puntos FROM Jugador WHERE nombre = @nombre", cn))
                        {
                            sel.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = user;
                            object val = sel.ExecuteScalar();
                            if (val != null && val != DBNull.Value)
                            {
                                previous = Convert.ToInt32(val);
                            }
                        }

                        if (points > previous)
                        {
                            using (SqlCommand upd = new SqlCommand("UPDATE Jugador SET puntos = @puntos WHERE nombre = @nombre", cn))
                            {
                                upd.Parameters.Add("@puntos", SqlDbType.Int).Value = points;
                                upd.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = user;
                                upd.ExecuteNonQuery();
                                return true;
                            }
                        }
                        return false;
                    }
                }
                catch
                {
                    // Fall back to local store.
                }
            }

            lock (FileLock)
            {
                List<PlayerRecord> players = LoadLocalPlayersUnsafe();
                PlayerRecord player = players.FirstOrDefault(p => string.Equals(p.Nombre, user, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    players.Add(new PlayerRecord { Nombre = user, PasswordHash = string.Empty, Puntos = points });
                    SaveLocalPlayersUnsafe(players);
                    return true;
                }

                if (points > player.Puntos)
                {
                    player.Puntos = points;
                    SaveLocalPlayersUnsafe(players);
                    return true;
                }
                return false;
            }
        }

        public static List<PlayerRecord> GetLeaderboard()
        {
            string connStr = GetSqlConnectionString();
            if (!string.IsNullOrEmpty(connStr))
            {
                try
                {
                    var result = new List<PlayerRecord>();
                    using (SqlConnection cn = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand("SELECT nombre, puntos FROM Jugador ORDER BY puntos DESC, nombre ASC", cn))
                    {
                        cn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new PlayerRecord
                                {
                                    Nombre = reader["nombre"].ToString(),
                                    Puntos = reader["puntos"] == DBNull.Value ? 0 : Convert.ToInt32(reader["puntos"])
                                });
                            }
                        }
                    }
                    return result;
                }
                catch
                {
                    // Fall back to local store.
                }
            }

            lock (FileLock)
            {
                return LoadLocalPlayersUnsafe()
                    .OrderByDescending(p => p.Puntos)
                    .ThenBy(p => p.Nombre, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
        }

        private static List<PlayerRecord> LoadLocalPlayersUnsafe()
        {
            string path = GetLocalStorePath();
            var list = new List<PlayerRecord>();
            if (!File.Exists(path))
            {
                return list;
            }

            foreach (string line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('\t');
                if (parts.Length < 3)
                {
                    continue;
                }

                int pts;
                int.TryParse(parts[2], out pts);
                list.Add(new PlayerRecord
                {
                    Nombre = parts[0],
                    PasswordHash = parts[1],
                    Puntos = pts
                });
            }
            return list;
        }

        private static void SaveLocalPlayersUnsafe(List<PlayerRecord> players)
        {
            string path = GetLocalStorePath();
            var lines = players.Select(p => string.Join("\t", p.Nombre, p.PasswordHash ?? string.Empty, p.Puntos));
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }
    }

    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
            txtcontraseña.UseSystemPasswordChar = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (frm_usuarionuevo un = new frm_usuarionuevo())
            {
                un.ShowDialog(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text.Trim();
            string pass = txtcontraseña.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ScoreRepository.ValidateLogin(usuario, pass))
            {
                this.Hide();
                frm_juego juego = new frm_juego(usuario);
                juego.FormClosed += (s, args) =>
                {
                    if (!this.IsDisposed)
                    {
                        this.Close();
                    }
                };
                juego.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcontraseña.Clear();
                txtusuario.Focus();
            }
        }
    }
}
