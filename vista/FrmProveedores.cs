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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SistemaGestionProductos.vista
{
    public partial class FrmProveedores : Form
    {
        ControladorProveedor proveedor =  new ControladorProveedor();
        Validaciones Validaciones = new Validaciones();
        public FrmProveedores()
        {
            InitializeComponent();
            mostrarDatos();
            this.dgvDatos.Columns[0].Visible = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        //metodo que establece como fuente de datos el metodo ListarProveedores
        private void mostrarDatos()
        {
            dgvDatos.DataSource = proveedor.ListarProveedores();


        }

        //metodo usado para limpiar los textbox para ingresar un nuevo registro 
        private void NuevoRegistro()
        {
            txtEmpresa.Clear();
            txtId.Clear();
            txtMail.Clear();
            txtDireccion.Clear();
            mskTel.Clear();
            txtEmpresa.Focus();
            mostrarDatos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = true;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {

    
            //validando campos vacios
            if (txtEmpresa.Text.Trim() == "" || txtMail.Text.Trim() == "" || txtDireccion.Text.Trim() == "" ||mskTel.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            //verificando el formato del correo a ingresar
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)

            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else
            {
                //asignando los valores de los textbox al constructor y ejecutando la consulta
                proveedor.Empresa = txtEmpresa.Text;
                proveedor.Email = txtMail.Text;
                proveedor.Numero_telefono = mskTel.Text;
                proveedor.Direccion = txtDireccion.Text;

                int datos = proveedor.AgregarProveedor();
                NuevoRegistro();

            }
        }

        //limpiando los campos para agregar otro registro 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //asgnando los valores de los textbox en orden a los valores de cada celda del registro seleccionado
            int posicion = this.dgvDatos.CurrentRow.Index;
            txtId.Text = dgvDatos[0, posicion].Value.ToString();
            txtEmpresa.Text = dgvDatos[1, posicion].Value.ToString();
            txtMail.Text = dgvDatos[2, posicion].Value.ToString();
            txtDireccion.Text = dgvDatos[3, posicion].Value.ToString();
            mskTel.Text = dgvDatos[4, posicion].Value.ToString();


            //deshabilitando botones para no ingesar valores repetidos
            btnIngresar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
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
                //cargando todo la tabla de proveedores
                dgvDatos.DataSource = proveedor.ListarProveedores();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            //validando campos vacios
            if (txtEmpresa.Text.Trim() == "" || txtMail.Text.Trim() == "" || txtDireccion.Text.Trim() == "" || mskTel.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            //verificando el formato del correo a ingresar
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else
            {

                //asignando los valores de los textbox al constructor y ejecutando la consulta
                proveedor.Empresa = txtEmpresa.Text;
                proveedor.Email = txtMail.Text;
                proveedor.Numero_telefono = mskTel.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.Id_proveedor = Convert.ToInt32(txtId.Text);

                bool datos = proveedor.ActualizarProveedor();
                NuevoRegistro();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //se asigna el id del registro a eliminar
            proveedor.Id_proveedor = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //si la respuesta es si, elimina el registro seleccionado
            if (dialaog == DialogResult.Yes)
            {
                bool datos = proveedor.EliminarProveedor();
                NuevoRegistro();
            }
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

        }

        /*inicio de validaciones con eventos de longitud y tipo de dato a ingrear*/
        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarTextoLargo(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarTextoLargo(e);
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SetLongitudValores(sender, e, 50);

        }
        /*fin de validacion de eventos*/
    }
}
