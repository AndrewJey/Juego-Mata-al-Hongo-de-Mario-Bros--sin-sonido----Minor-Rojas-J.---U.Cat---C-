using System;
using System.Windows.Forms;

namespace Juego
{
    public partial class frm_usuarionuevo : Form
    {
        public frm_usuarionuevo()
        {
            InitializeComponent();
            txtcontraseña.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string error;
            if (ScoreRepository.RegisterPlayer(txtusuario.Text, txtcontraseña.Text, out error))
            {
                MessageBox.Show("Usuario creado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(error ?? "No se pudo registrar el usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
