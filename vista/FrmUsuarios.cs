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
            //validando datos vacios o espacios en blanco 
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
              string.IsNullOrWhiteSpace(txtApellido.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) ||
              string.IsNullOrWhiteSpace(txtUsuario.Text) ||
              string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //validando el formato del correo electronico
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            //validando la mayoria de edad
            else if (!Validaciones.EsMayorDeEdad(dtpNacimiento.Value))
            {
                MessageBox.Show("El usuario debe ser mayor de 18 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                //asignando los valores de los textbox al constructor
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Usuario = txtUsuario.Text;
                usuario.Correo = txtMail.Text;
                usuario.Fecha_nacimiento = dtpNacimiento.Value;
                /*se convierte en byte el valor retornado por el método GetPass*/
                byte[] pass = System.Text.Encoding.UTF8.GetBytes(GetPass());
                /*se asigna el valor de la contraseña convirtiendo el byte en una contraseña cifrada con el metodo Hash*/
                usuario.Contra = Hash(pass);

                int datos =  usuario.AgregarUsuario();
                if(datos == 2)
                {
                    /*se le notifica del nombre de usuario del usuario creado y de la contraseña del mismo*/
                    MessageBox.Show("El nombre de usuario es " +
                    txtUsuario.Text + " y la contraseña es " + GetPass(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    NuevoRegistro();
                }
               
            }
        }

        private string GetPass()
        {
            /*devuelve un string donde Se concatenan la primera y segunda letra del nombre,
             * primera y segunda letra del apellido y el año de 
             nacimiento del usuario creado*/
            string pass = txtNombre.Text[0].ToString() + txtNombre.Text[1].ToString()+ txtApellido.Text[0].ToString()
                + txtApellido.Text[1].ToString() + dtpNacimiento.Value.Year.ToString();
            
            return pass;

        }

        //metodo que limpia todos los textbox para una nueva inserción
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

        //metodo que devuelve el byte de la contraseña sin cifrar convertido en una contraseña cifrada
        string Hash(byte[] val)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(val);
                return Convert.ToBase64String(hash);
            }
        }

        //metodo que asigna la fuente de datos al datagridview
        private void mostrarDatos()
        {
            dgvDatos.DataSource = usuario.ListarUsuarios();


        }

        //asigna los valores del registro selccionado a los textbox
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
            //validando datos vacios o espacios en blanco 
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
              string.IsNullOrWhiteSpace(txtApellido.Text) ||
              string.IsNullOrWhiteSpace(txtDireccion.Text) ||
              string.IsNullOrWhiteSpace(txtUsuario.Text) ||
              string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //validando el formato del correo electronico
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            //validando mayoria de edad
            else if (!Validaciones.EsMayorDeEdad(dtpNacimiento.Value))
            {
                MessageBox.Show("El usuario debe ser mayor de 18 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //asignando los valores de los textbox al constructor
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Usuario = txtUsuario.Text;
                usuario.Correo = txtMail.Text;
                usuario.Fecha_nacimiento = dtpNacimiento.Value;
                usuario.Id_usuario = Convert.ToInt32(txtId.Text);

                //ejecutando el metodo para actualizar el dato
                bool datos = usuario.ActualizarUsuario();
                if(datos == true)
                {
                    NuevoRegistro();

                }


            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //se asigna el id del registro a eliminar
            usuario.Id_usuario = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //si la respuesta es si, elimina el registro seleccionado
            if (dialaog == DialogResult.Yes)
            {
                bool datos = usuario.EliminarUsuario();
                NuevoRegistro();
            }
        }

        //limpia todos los campos para agregar un nuevo registro 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                // Elimina la selección actual en el DataGridView
                dgvDatos.CurrentCell = null;

                // Oculta todas las filas del DataGridView inicialmente
                foreach (DataGridViewRow r in dgvDatos.Rows)
                {
                    r.Visible = false;
                }

                // Recorre todas las filas del DataGridView
                foreach (DataGridViewRow r in dgvDatos.Rows)
                {
                    // Recorre todas las celdas de la fila
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        // Compara el valor de la celda con el texto de búsqueda, sin importar mayúsculas o minúsculas
                        //compara los valores de toda la cadena de texto con el registro de cada celda
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtBuscar.Text.ToUpper()) >= 0)
                        {
                            // Si encuentra una coincidencia, hace visible la fila
                            r.Visible = true;
                            break; // Sale del ciclo al encontrar el registro buscado
                        }
                    }
                }
            }
            else
            {
                dgvDatos.DataSource = usuario.ListarUsuarios();
            }
        }

        /*inicio validaciones de eventos para longitud de datos y tipo de dato ingresado*/
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

        /*FIn de validaciones*/
    }
}
