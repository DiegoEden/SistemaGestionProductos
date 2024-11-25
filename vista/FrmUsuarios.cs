using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Validaciones Validaciones = new Validaciones();
        public FrmUsuarios()
        {
            InitializeComponent();
            mostrarDatos();
            this.dgvDatos.Columns[0].Visible = false;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
              string.IsNullOrWhiteSpace(txtApellido.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) ||
              string.IsNullOrWhiteSpace(txtUsuario.Text) ||
              string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else if (!Validaciones.EsMayorDeEdad(dtpNacimiento.Value))
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
                byte[] pass = System.Text.Encoding.UTF8.GetBytes(GetPass());
                usuario.Contra = Hash(pass);

                int datos =  usuario.AgregarUsuario();
                if(datos == 2)
                {
                    MessageBox.Show("El nombre de usuario es " +
                    txtUsuario.Text + " y la contraseña es " + GetPass(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    NuevoRegistro();
                }
               
            }
        }

        private string GetPass()
        {
            string pass = txtNombre.Text[0].ToString() + txtNombre.Text[1].ToString()+ txtApellido.Text[0].ToString()
                + txtApellido.Text[1].ToString() + dtpNacimiento.Value.Year.ToString();
            
            return pass;

        }

        private void NuevoRegistro()
        {
            txtNombre.Focus();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtMail.Clear();    
            txtUsuario.Clear();
            txtId.Clear();
            dtpNacimiento.Value = DateTime.Now; 
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = true;
            mostrarDatos();
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
 
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int posicion = this.dgvDatos.CurrentRow.Index;
            txtId.Text = dgvDatos[0, posicion].Value.ToString();
            txtNombre.Text = dgvDatos[1, posicion].Value.ToString();
            txtApellido.Text = dgvDatos[2, posicion].Value.ToString();
            txtDireccion.Text = dgvDatos[3, posicion].Value.ToString();
            txtUsuario.Text = dgvDatos[4, posicion].Value.ToString();
            txtMail.Text = dgvDatos[5, posicion].Value.ToString();
            dtpNacimiento.Value =Convert.ToDateTime(dgvDatos[6, posicion].Value.ToString());

            btnIngresar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
              string.IsNullOrWhiteSpace(txtApellido.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) ||
              string.IsNullOrWhiteSpace(txtUsuario.Text) ||
              string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else if (!Validaciones.EsMayorDeEdad(dtpNacimiento.Value))
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
                usuario.Id_usuario = Convert.ToInt32(txtId.Text);

                bool datos = usuario.ActualizarUsuario();
                if(datos == true)
                {
                    NuevoRegistro();

                }


            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            usuario.Id_usuario = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialaog == DialogResult.Yes)
            {
                bool datos = usuario.EliminarUsuario();
                NuevoRegistro();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                dgvDatos.CurrentCell = null;
                foreach (DataGridViewRow r in dgvDatos.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvDatos.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtBuscar.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                dgvDatos.DataSource = usuario.ListarUsuarios();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
            Validaciones.SetLongitudValores(sender, e, 50);


        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarTextoLargo(e);

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarUsername(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SetLongitudValores(sender, e, 50);

        }
    }
}
