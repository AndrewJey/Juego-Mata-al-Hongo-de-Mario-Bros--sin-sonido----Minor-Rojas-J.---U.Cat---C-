using System;
using System.Drawing;
using System.Windows.Forms;

namespace Juego
{
    public partial class Form1 : Form
    {
        private readonly Random _random = new Random();
        private int time = 60;
        private int points = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            points++;
            label1.Text = "Puntuación: " + points;
            move_npc();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time <= 0)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(this, "Tu puntuación es " + points, "Fin de la partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                points = 0;
                time = 60;
                label1.Text = "Puntuación: " + points;
                label2.Text = "Tiempo: " + time;
                move_npc();
                timer1.Start();
                timer2.Start();
            }
            else
            {
                time--;
                label2.Text = "Tiempo: " + time;
            }
        }

        private void move_npc()
        {
            int maxX = Math.Max(1, this.ClientSize.Width - pictureBox2.Width - 8);
            int minY = Math.Min(40, Math.Max(0, this.ClientSize.Height - pictureBox2.Height - 8));
            int maxY = Math.Max(minY + 1, this.ClientSize.Height - pictureBox2.Height - 8);
            pictureBox2.Location = new Point(_random.Next(0, maxX), _random.Next(minY, maxY));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            move_npc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Puntuación: " + points;
            label2.Text = "Tiempo: " + time;
            move_npc();
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmGestor_memoria gestor = new frmGestor_memoria())
            {
                gestor.ShowDialog(this);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmNuevo_Jugador nuevo = new frmNuevo_Jugador())
            {
                if (nuevo.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(nuevo.NombreJugador))
                {
                    this.Text = "Juego - Jugador: " + nuevo.NombreJugador;
                }
            }
        }
    }
}
