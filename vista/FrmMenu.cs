using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionProductos.controlador;

namespace SistemaGestionProductos.vista
{
    public partial class FrmMenu : Form
    {

        Form currentForm;

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            //Buscar la coleccion del formulario
            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;

                if (currentForm != null)
                {
                    currentForm.Close();
                    panelContenedor.Controls.Remove(currentForm);
                }

                currentForm = formulario;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            else
            {
                formulario.BringToFront();
            }

        }

        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            foreach (var control in panelContenedor.Controls)
            {
                if (control is FrmMenu)
                {

                }

                else
                {

                }
            }
        }

        public FrmMenu()
        {
            InitializeComponent();
            lblUsuario.Text =VariablesGlobales.Usuario;
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void tsbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void tsbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void tlspNav_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("¿Está seguro de cerrar la sesión?", "Cerar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Show();
            }

          
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmProductos>();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmCategorias>();

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmUsuarios>();

        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmProveedores>();

        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {

            
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmPerfil>();

        }
    }
}
