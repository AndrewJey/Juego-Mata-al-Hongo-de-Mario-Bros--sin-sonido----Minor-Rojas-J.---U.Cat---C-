using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego
{
    public partial class frm_juego : Form
    {
       
       SoundPlayer musica = new SoundPlayer(Application.StartupPath + "\\Juego.wav");
           
        Thread tiempo1;
        
        int time = 60;
        int points = 0;
        string nombre;
        ThreadStart d;
       
      
        
        public frm_juego(string nombre)
        {
         
            musica.Play();
            InitializeComponent();
            
            /* Inicio de hilo  */
            Thread control;
            control = new Thread(controlMemoria);
            control.Start();
            this.nombre = nombre;
            d = new ThreadStart(metodoTiempo);
            tiempo1 = new Thread(d);
            tiempo1.Start();
            lbl_tiempo.Text = "Tiempo: " + time;
            string usuario;


            /*se abre la conexion a la base de datos*/
            SqlConnection cn = new SqlConnection("Data Source=MINOR\\SQLEXPRESS;Initial Catalog=DB_Login;Integrated Security=True;");


            /*Se da valor de la etiqueta para que lleve el nombre del usuario logueado*/
            SqlCommand cmd = new SqlCommand("select nombre from Jugador where nombre = '" +nombre + "'", cn);
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        usuario = reader["nombre"].ToString();
                        lbl_jugador.Text = usuario;
                        cn.Close();
                    }

                }
        }

        /*se le da la accion al click para sumar los punto cuando se cliquea la imagen*/
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            points = points + 1;
            lbl_puntuacion.Text = "Puntuación: " + points;
            
        }
        

        /*metodo donde lleva el tiempo del juego para jugar*/
        void metodoTiempo()
        {
            bool flag = true;

            while (flag)
            {
                Thread.Sleep(1000);
                if (time == 0)
                {
                    timer2.Stop();
                    flag = false;
                    MessageBox.Show("Tu puntuación és " + points);
                    
                    guardarPuntacion();
                }
                else
                {
                    time = time - 1;

                    try
                    {
                        MethodInvoker inv = delegate
                            {
                                lbl_tiempo.Text = "Tiempo: " + time;

                            };
                        this.Invoke(inv);
                    }

                    catch { }

                }

            }
        }
       
        /*se guarda la puntuacion de los jugadores a la base de datos*/
        
        private void guardarPuntacion() 
        { 
        SqlConnection cn = new SqlConnection("Data Source=MINOR\\SQLEXPRESS;Initial Catalog=DB_Login;Integrated Security=True;");
        cn.Open();
        SqlCommand cmd2 = new SqlCommand(string.Format("update Jugador set puntos ='{0}' where nombre like '{1}'", points, nombre), cn);
           if(consultarMayor())
                cmd2.ExecuteNonQuery();
            cn.Close();        
            
        
        }

        /*aqui se evalua si la puntuacion del usuario es mayor a la anterior para hacer almacenada*/
        private bool consultarMayor() {

            SqlConnection cn = new SqlConnection("Data Source=MINOR\\SQLEXPRESS;Initial Catalog=DB_Login;Integrated Security=True;");
            int punto=0;
            try
            {
                SqlCommand cmd = new SqlCommand("select puntos from Jugador where nombre = '" +nombre + "'", cn);
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()){
                
                if(reader.Read()){
                    punto = (int) reader["puntos"];
                     
                }
                }


                if (punto < points)
                    return true;
                else
                    return false;
                


            }

            finally
            {

                cn.Close();

            }

        
        
        }
       
        private void empezar() {
            
            points = 0;
            time = 60;
            lbl_puntuacion.Text = "Puntuación: " + points;
            lbl_tiempo.Text = "Tiempo: " + time;
            tiempo1 = new Thread(d);
            tiempo1.Start();
            timer2.Start();
        
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_usuarionuevo un = new frm_usuarionuevo();
            un.Show();

        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void frm_juego_Load(object sender, EventArgs e)
        {

        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musica.Play();
            empezar();
        }
        void controlMemoria()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(100);
                    Process currentProc = Process.GetCurrentProcess();
                    currentProc.Refresh();
                    var counter = new PerformanceCounter("Process", "Working Set - Private", currentProc.ProcessName);
                    MethodInvoker inv = delegate
                    {
                        var tamaño = (counter.RawValue / 1024) / 1000;
                        lbl_megas.Text = (tamaño.ToString() + "MB");
                        progressBar1.Refresh();
                        progressBar1.Value = (int)tamaño;


                    };
                    this.Invoke(inv);
                }
            }
            catch {

            }

        }

        private void puntuacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_puntuaciones puntuaciones = new frm_puntuaciones();
            puntuaciones.Show();
        }

        private void frm_juego_FormClosed(object sender, FormClosedEventArgs e)
        {

            musica.Stop(); 
        }
    }
   
}
