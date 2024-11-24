using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.vista
{
    public partial class FrmSplashScreen : Form
    {
        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        private void timerCarga_Tick(object sender, EventArgs e)
        {
            panelCarga.Width += 3;
            if (panelCarga.Width >= 640)
            {
                timerCarga.Stop();
                Form login = new FrmLogin();
                login.Show();
                this.Hide();
            }
        }
    }
}
