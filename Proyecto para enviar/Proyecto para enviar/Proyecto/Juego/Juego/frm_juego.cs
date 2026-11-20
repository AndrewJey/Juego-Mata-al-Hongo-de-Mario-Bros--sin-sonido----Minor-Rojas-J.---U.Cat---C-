using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Juego
{
    public partial class frm_juego : Form
    {
        private SoundPlayer _musica;
        private readonly System.Windows.Forms.Timer _countdownTimer;
        private readonly System.Windows.Forms.Timer _memoryTimer;
        private readonly GameSession _session;

        public frm_juego(string nombre)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            _session = new GameSession(nombre, 60);
            lbl_jugador.Text = _session.PlayerName;
            lbl_puntuacion.Text = "Puntuación: 0";
            lbl_tiempo.Text = "Tiempo: " + _session.RemainingSeconds;

            _countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _countdownTimer.Tick += CountdownTimer_Tick;

            _memoryTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _memoryTimer.Tick += MemoryTimer_Tick;

            TryPlayMusic();
            _countdownTimer.Start();
            _memoryTimer.Start();
            timer2.Start();
        }

        private void TryPlayMusic()
        {
            try
            {
                string wavPath = Path.Combine(Application.StartupPath, "Juego.wav");
                if (File.Exists(wavPath))
                {
                    _musica = new SoundPlayer(wavPath);
                    _musica.PlayLooping();
                }
            }
            catch
            {
                // Audio is optional when sound file or audio device is unavailable.
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int score = _session.RegisterHit();
            lbl_puntuacion.Text = "Puntuación: " + score;
            if (_session.IsRunning)
            {
                move_npc();
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            bool stillRunning = _session.TickSecond();
            lbl_tiempo.Text = "Tiempo: " + _session.RemainingSeconds;

            if (!stillRunning)
            {
                _countdownTimer.Stop();
                timer2.Stop();
                bool isRecord = ScoreRepository.SaveHighScore(_session.PlayerName, _session.Score);
                string msg = isRecord
                    ? string.Format("¡Nuevo récord! Tu puntuación es {0}.", _session.Score)
                    : string.Format("Fin del tiempo. Tu puntuación es {0}.", _session.Score);
                MessageBox.Show(this, msg, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (Process currentProc = Process.GetCurrentProcess())
                {
                    long megas = currentProc.PrivateMemorySize64 / (1024L * 1024L);
                    lbl_megas.Text = megas + " MB";
                    int clamped = (int)Math.Max(progressBar1.Minimum, Math.Min(progressBar1.Maximum, megas));
                    progressBar1.Value = clamped;
                }
            }
            catch
            {
                // Ignore transient process metrics errors.
            }
        }

        private void empezar()
        {
            _session.Reset(60);
            lbl_puntuacion.Text = "Puntuación: 0";
            lbl_tiempo.Text = "Tiempo: " + _session.RemainingSeconds;
            move_npc();
            _countdownTimer.Start();
            timer2.Start();
        }

        private void move_npc()
        {
            Point next = _session.NextTargetLocation(
                Math.Max(200, this.ClientSize.Width),
                Math.Max(200, this.ClientSize.Height),
                pictureBox2.Width,
                pictureBox2.Height);
            pictureBox2.Location = next;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            move_npc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frm_usuarionuevo un = new frm_usuarionuevo())
            {
                un.ShowDialog(this);
            }
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puntuacionesToolStripMenuItem_Click(sender, e);
        }

        private void frm_juego_Load(object sender, EventArgs e)
        {
            move_npc();
        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryPlayMusic();
            empezar();
        }

        private void puntuacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frm_puntuaciones puntuaciones = new frm_puntuaciones())
            {
                puntuaciones.ShowDialog(this);
            }
        }

        private void frm_juego_FormClosed(object sender, FormClosedEventArgs e)
        {
            _countdownTimer.Stop();
            _countdownTimer.Dispose();
            _memoryTimer.Stop();
            _memoryTimer.Dispose();
            timer2.Stop();
            if (_musica != null)
            {
                try { _musica.Stop(); } catch { }
                _musica.Dispose();
                _musica = null;
            }
        }
    }
}
