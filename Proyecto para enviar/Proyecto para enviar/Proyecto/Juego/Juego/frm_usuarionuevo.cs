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
using System.Configuration;
using System.IO;


namespace Juego
{
    public partial class frm_usuarionuevo : Form
    {
        public frm_usuarionuevo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string nombre = txtusuario.Text;
        
            /*se hace la conexcion a la base de datos*/
         

            
            SqlConnection cn = new SqlConnection("Data Source=MINOR\\SQLEXPRESS;Initial Catalog=DB_Login;Integrated Security=True;");




            try
            {  
                /*se consulta a la base de datos para ser almacenados los usuarios*/
                    SqlCommand cmd2 = new SqlCommand(string.Format("Insert Into Jugador ( nombre,contraseña,puntos ) values ('{0}','{1}','{2}')",txtusuario.Text, txtcontraseña.Text,0), cn);


           
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    
                     
                    da.Fill(ds, "Jugador");                                 
                    MessageBox.Show("Usuario Creado");
                    cn.Close();
                                }
            catch 
            {
                MessageBox.Show("FALTA INFORMACÓN");

                
            }

            finally 
            {

                cn.Close();
            
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        }
    }

