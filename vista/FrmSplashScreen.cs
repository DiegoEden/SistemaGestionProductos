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
            //aumenta a 3px el ancho del panel por cada tick
            panelCarga.Width += 3;
            //al llegar a mas de 640px
            if (panelCarga.Width >= 640)
            {
                //deteniendo el timer
                timerCarga.Stop();
                //mostrando el formulario de login y ocultando la pantalla de carga
                Form login = new FrmLogin();
                login.Show();
                this.Hide();
            }
        }
    }
}
