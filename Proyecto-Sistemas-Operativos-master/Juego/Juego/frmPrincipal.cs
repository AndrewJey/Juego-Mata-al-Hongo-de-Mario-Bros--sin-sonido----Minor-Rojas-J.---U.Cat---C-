using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego
{
    public partial class Form1 : Form
    {
        int time = 60;
        int points = 0;
        public Form1()
        {
            InitializeComponent();



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            points = points + 1;
            label1.Text = "Puntuación: " + points;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Tu puntuación és " + points);
                points = 0;
                time = 60;
                label1.Text = "Puntuación: " + points;
                label2.Text = "Tiempo: " + time;
                timer1.Start();
                timer2.Start();
            }
            else
            {
                time = time - 1;
                label2.Text = "Tiempo: " + time;
            }
        }

        private void move_npc()
        {
            Random random = new Random();
            pictureBox2.Location = new Point(random.Next(917), random.Next(598));
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

        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
