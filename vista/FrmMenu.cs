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

        // Método que abre un formulario en el panel principal
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;  // Variable que representará el formulario a abrir.

            // Buscar si ya existe una instancia del formulario en la colección de controles del panel.
            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();

            // Si no se encontró el formulario (es decir, no está abierto aún)
            if (formulario == null)
            {
                // Crear una nueva instancia del formulario
                formulario = new MiForm();

                // Configurar propiedades del formulario:
                formulario.TopLevel = false; 
                formulario.FormBorderStyle = FormBorderStyle.None;  
                formulario.Dock = DockStyle.Fill; 

                // Si ya hay un formulario abierto, ciérralo antes de abrir el nuevo.
                if (currentForm != null)
                {
                    currentForm.Close();  
                    panelContenedor.Controls.Remove(currentForm); 
                }

                // Asignar el nuevo formulario como el formulario actual.
                currentForm = formulario;

                // Agregar el formulario al panel
                panelContenedor.Controls.Add(formulario);

                // Asignar el formulario al tag del contenedor
                panelContenedor.Tag = formulario;

                // Mostrar el formulario.
                formulario.Show();

                // Llevar el formulario al frente de otros controle
                formulario.BringToFront();

                //maneja el cierre de los formularios
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            else
            {
                // Si el formulario ya está abierto, lo trae al frente
                formulario.BringToFront();
            }
        }


        //metodo que cierra el formulario actual al abrir otro
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

        /*metodos para poder mover el sistema a traves del toolstrip*/
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
            //metodos para mover el formulario 
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //metodo para cerrar sesion
            DialogResult dialogResult = MessageBox.Show("¿Está seguro de cerrar la sesión?", "Cerar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //si el resultado el si, redirige al login
            if(dialogResult == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Show();
            }

          
        }

        private void setCurrentForm(Button currentform) {

            currentform.BackColor = Color.FromArgb(166, 87, 217);
           
        }

        //abrirendo los formularios
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

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cerrando la aplicacion
            Application.Exit(); 
        }
    }
}
