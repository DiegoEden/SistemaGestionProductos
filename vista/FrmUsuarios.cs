using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.vista
{
    public partial class FrmUsuarios : Form
    {
        ControladorUsuario usuario = new ControladorUsuario();
        public FrmUsuarios()
        {
            InitializeComponent();
            mostrarDatos();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
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

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Usuario = txtUsuario.Text;
                usuario.Correo = txtMail.Text;
                usuario.Fecha_nacimiento = dtpNacimiento.Value;
                byte[] pass = System.Text.Encoding.UTF8.GetBytes(getPass());
                usuario.Contra = Hash(pass);

                int datos =  usuario.AgregarUsuario();

                MessageBox.Show("El nombre de usuario es " +
                    txtUsuario.Text + " y la contraseña es "+getPass(),"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private string getPass()
        {
            string pass = txtNombre.Text[0].ToString() + txtNombre.Text[1].ToString()+ txtApellido.Text[0].ToString()
                + txtApellido.Text[1].ToString() + dtpNacimiento.Value.Year.ToString();
            
            return pass;

        }

        string Hash(byte[] val)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(val);
                return Convert.ToBase64String(hash);
            }
        }

        private void mostrarDatos()
        {
            dgvDatos.DataSource = usuario.ListarUsuarios();


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
