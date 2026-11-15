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
{//hola
    public partial class frmGestor_memoria : Form
    {
        public frmGestor_memoria()
        {
            InitializeComponent();


            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer(components ?? (components = new System.ComponentModel.Container()));
            _timer.Interval = 150;
            _timer.Tick += _timer_Tick;
            _timer.Start();
 


        }

        private void frmGestor_memoria_Load(object sender, EventArgs e)
        {
            _timer_Tick(sender, e);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            var pcInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            double memoriaUsadaMiB = (pcInfo.TotalPhysicalMemory - pcInfo.AvailablePhysicalMemory) / (1024.0 * 1024.0);
            Text = String.Format("Memoria física en uso: {0:F1} MiB", memoriaUsadaMiB);
}
        
        
    }
}
