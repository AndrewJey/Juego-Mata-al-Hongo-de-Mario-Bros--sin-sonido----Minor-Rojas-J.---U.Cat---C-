using System;
using System.Data;
using System.Windows.Forms;

namespace Juego
{
    public partial class frm_puntuaciones : Form
    {
        public frm_puntuaciones()
        {
            InitializeComponent();
        }

        private void frm_puntuaciones_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ScoreRepository.GetSqlConnectionString()))
                {
                    this.jugadorTableAdapter.Fill(this.dB_LoginDataSet1.Jugador);
                    return;
                }
            }
            catch
            {
                // Fall back to local/repository leaderboard below.
            }

            DataTable table = new DataTable();
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Puntos", typeof(int));

            foreach (PlayerRecord record in ScoreRepository.GetLeaderboard())
            {
                table.Rows.Add(record.Nombre, record.Puntos);
            }

            dataGridView2.DataSource = table;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
        }
    }
}
