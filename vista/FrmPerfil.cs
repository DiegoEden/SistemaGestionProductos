using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.vista
{
    public partial class FrmPerfil : Form
    {
        ControladorUsuario controlador = new ControladorUsuario();
        public FrmPerfil()
        {
            InitializeComponent();
        }

        private void FrmPerfil_Load(object sender, EventArgs e)
        {

            GetInfo();
        }


        private void GetInfo()
        {
            txtApellido.Text = VariablesGlobales.Apellido;
            txtNombre.Text = VariablesGlobales.Nombre;  
            txtMail.Text = VariablesGlobales.Correo;
            txtUsuario.Text = VariablesGlobales.Usuario;
            txtId.Text = VariablesGlobales.UsuarioID.ToString();
            txtDireccion.Text = VariablesGlobales.Direccion;
            dtpNacimiento.Value = VariablesGlobales.FechaNac;
            lblNombre.Text ="Hola, "+ VariablesGlobales.Nombre + " " + VariablesGlobales.Apellido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Regex.IsMatch(txtMail.Text, pattern))
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else if (!EsMayorDeEdad(dtpNacimiento.Value))
            {
                MessageBox.Show("El usuario debe ser mayor de 18 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                controlador.Nombre = txtNombre.Text.Trim();
                controlador.Apellido = txtApellido.Text.Trim();
                controlador.Direccion = txtDireccion.Text.Trim();
                controlador.Usuario = txtUsuario.Text.Trim();
                controlador.Correo = txtMail.Text.Trim();
                controlador.Fecha_nacimiento = dtpNacimiento.Value;
                controlador.Id_usuario = VariablesGlobales.UsuarioID;
                bool datos = controlador.ActualizarUsuario();

                if(txtUsuario.Text.Trim() != VariablesGlobales.Usuario)
                {

                    DialogResult dialog = MessageBox.Show("Ha actualizado su nombre de usuario, por cuestiones de seguridad " +
                        "deberá iniciar sesión de nuevo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dialog == DialogResult.OK)
                    {
                        this.Hide();
                        FrmLogin frmLogin = new FrmLogin();
                        frmLogin.Show();
                    
                    }

                }
            }


        }

        private bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento > hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad >= 18;
        }
    }
}
