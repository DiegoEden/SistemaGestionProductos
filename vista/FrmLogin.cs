﻿using System;
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
        Validaciones Validaciones = new Validaciones();

        //metodo que devuelve el byte de la contraseña sin cifrar convertido en una contraseña cifrada
        string Hash(byte[] val)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(val);
                return Convert.ToBase64String(hash);
            }
        }


        //metodos para mover el formulario 
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
            //saliendo de la app
            Application.Exit();
        }

        //boton para minimizar el sistema
        private void tsbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void tlspNav_MouseDown(object sender, MouseEventArgs e)
        {
            //metodos para mover el formulario desde el tooltip
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }


        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //convierte el texto de la contraseña en un byte para convertirla en una contraseña cifrada
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(txtContrasenia.Text.ToString());

            //valida que los campos no esten vacios
            if (txtUsuario.Text.Trim() == "" || txtContrasenia.Text.Trim() == "")
            {

                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //asigna los valores necesarios para iniciar sesion
                usuario.Usuario = txtUsuario.Text;
                usuario.Contra = Hash(pass);
                //se ejecuta el metodo de acceso
                bool respuesta = usuario.Acceso();
                if (respuesta == true)
                {
                    //asignando valores a las variables globales de uso común
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
        //metodo que oculta la contraseña
        private void btnHide_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '*'; ;
            btnHide.Visible = false;
            btnShow.Visible = true;
            txtContrasenia.Focus();

        }

        //metodo que muestra la contraseña
        private void btnShow_Click(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '\0'; ;
            btnHide.Visible = true;
            btnShow.Visible = false;
            txtContrasenia.Focus();
        }

        //saliendo de la app
        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando nombre de usuario
            Validaciones.ValidarUsername(e);
            Validaciones.SetLongitudValores(sender, e, 50);
        }
    }
}
