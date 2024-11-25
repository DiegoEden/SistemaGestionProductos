using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;

namespace SistemaGestionProductos.vista
{
    
    public partial class FrmLogin : Form
    {
        ControladorUsuario usuario =  new ControladorUsuario();
        string Hash(byte[] val)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(val);
                return Convert.ToBase64String(hash);
            }
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmLogin()
        {
            InitializeComponent();
        }

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

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }


        private void btnIngresar_Click(object sender, EventArgs e)
        {
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(txtContrasenia.Text.ToString());
            string password = Hash(pass);

            if (txtUsuario.Text.Trim() == "" || txtContrasenia.Text.Trim() == "")
            {

                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                usuario.Usuario = txtUsuario.Text;
                usuario.Contra = password;

                bool respuesta = usuario.Acceso();
                if (respuesta == true)
                {
                    VariablesGlobales.UsuarioID = usuario.Id_usuario;
                    VariablesGlobales.Usuario = usuario.Usuario;
                    VariablesGlobales.Nombre = usuario.Nombre;
                    VariablesGlobales.Apellido = usuario.Apellido;
                    VariablesGlobales.Direccion = usuario.Direccion;
                    VariablesGlobales.Correo = usuario.Correo;
                    VariablesGlobales.FechaNac = usuario.Fecha_nacimiento;
                    FrmMenu frmMenu = new FrmMenu();
                    frmMenu.Show();
                    this.Hide();
                }

            }
        
            

          
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '*'; ;
            btnHide.Visible = false;
            btnShow.Visible = true;
            txtContrasenia.Focus();

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '\0'; ;
            btnHide.Visible = true;
            btnShow.Visible = false;
            txtContrasenia.Focus();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
