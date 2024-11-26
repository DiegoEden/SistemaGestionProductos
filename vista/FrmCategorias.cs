using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionProductos.controlador;
using System.Xml.Linq;

namespace SistemaGestionProductos.vista
{
    public partial class FrmCategorias : Form
    {
        //instanciando clases necesarias
        ControladorCategoria categoria = new ControladorCategoria();
        Validaciones Validaciones = new Validaciones();

        public FrmCategorias()
        {
            InitializeComponent();
            mostrarDatos();
            //se establece como oculta la columna del id, asi como botones actualizar y eliminar deshabilitados
            this.dgvDatos.Columns[0].Visible = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;


        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            //se vertifica que los campos del formulario no estén vacíos
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "") {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                //se asignan los atributos y se ejecuta el metodo para insertar datos
                categoria.Nombre_categoria = txtNombre.Text;
                categoria.Descripcion = txtDesc.Text;


                int datos = categoria.AgregarCategoria();
                //limpia los campos y actualiza el datagridview
                NuevoRegistro();
            }
           
        }


        //metodo que establece la fuente de datos del datagridview
        private void mostrarDatos()
        {
            dgvDatos.DataSource = categoria.ListarCategorias();


        }

        //metodo que vacía los textbox y actualiza los datos del datagridview
        private void NuevoRegistro()
        {
            txtDesc.Clear();
            txtID.Clear();
            txtNombre.Clear();
            txtNombre.Focus();
            mostrarDatos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = true;
        }

        //limpia los campos
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        //asigna los valores del registro selccionado a los textbox
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int posicion = this.dgvDatos.CurrentRow.Index;
            txtID.Text = dgvDatos[0, posicion].Value.ToString();
            txtNombre.Text = dgvDatos[1, posicion].Value.ToString();
            txtDesc.Text = dgvDatos[2, posicion].Value.ToString();
           
            btnIngresar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            //se vertifica que los campos del formulario no estén vacíos
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                categoria.Nombre_categoria = txtNombre.Text;
                categoria.Descripcion = txtDesc.Text;
                categoria.Id_categoria = Convert.ToInt32(txtID.Text);

                //se asignan los atributos y se ejecuta el metodo para insertar datos
                bool datos = categoria.EditarCategoria();
                //limpia los campos y actualiza el datagridview
                NuevoRegistro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //se asigna el id del registro a eliminar
            categoria.Id_categoria = Convert.ToInt32(txtID.Text);
            //se pregunta al usuario antes de eliminar el registro
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?","Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            //si la respuesta es si, elimina el registro seleccionado
            if(dialaog == DialogResult.Yes)
            {
                bool datos = categoria.EliminarCategoria();
                NuevoRegistro();
            }

           
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Verifica si el campo de texto de búsqueda no está vacío
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
                // Si el campo de búsqueda está vacío, muestra todos los registros nuevamente
                dgvDatos.DataSource = categoria.ListarCategorias();
            }

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando textbox para solo letras y 50 caracteres máximos
            Validaciones.SoloLetras(e);
            Validaciones.SetLongitudValores(sender, e, 50);
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando textbox para textos largos
            Validaciones.ValidarTextoLargo(e);
        }
    }
}
