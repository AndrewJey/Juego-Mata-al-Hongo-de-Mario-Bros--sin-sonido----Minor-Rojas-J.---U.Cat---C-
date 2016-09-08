using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Juego
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_usuarionuevo un = new frm_usuarionuevo();
            un.Show();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            SqlConnection cn = new SqlConnection("Data Source=MINOR\\SQLEXPRESS;Initial Catalog=DB_Login;Integrated Security=True;");

            try 
            {

                SqlCommand cmd = new SqlCommand("select Nombre,contraseña from Jugador where Nombre = '" + txtusuario.Text + "' and contraseña = '" + txtcontraseña.Text + "' ", cn);

                cn.Open();
                cmd.ExecuteNonQuery();


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Jugador");
                DataRow dro;
                dro = ds.Tables["Jugador"].Rows[0];

                if ((txtusuario.Text == dro["Nombre"].ToString() || (txtcontraseña.Text == dro["contraseña"].ToString())))
                {
                    this.Hide();
                    frm_juego j = new frm_juego(txtusuario.Text);
                    j.Show();



                }
                
                
                
                
            }
            catch 
            {

                MessageBox.Show("USUARIO O CONTRASEÑA INCORRECTO");

                txtcontraseña.Text = "";
                txtusuario.Text = "";
            
            }

            finally 
            {

                cn.Close();
               
            
            }


        }

    }
}