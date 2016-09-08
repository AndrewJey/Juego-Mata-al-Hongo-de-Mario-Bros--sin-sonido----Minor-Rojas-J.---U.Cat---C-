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
    public partial class frm_puntuaciones : Form
    {
        public frm_puntuaciones()
        {
            InitializeComponent();
        }

        private void frm_puntuaciones_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_LoginDataSet1.Jugador' table. You can move, or remove it, as needed.
            this.jugadorTableAdapter.Fill(this.dB_LoginDataSet1.Jugador);
            // TODO: This line of code loads data into the 'dB_LoginDataSet.Jugador' table. You can move, or remove it, as needed.
          
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
