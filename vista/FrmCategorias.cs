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

        ControladorCategoria categoria = new ControladorCategoria();

        public FrmCategorias()
        {
            InitializeComponent();
            mostrarDatos();
            this.dgvDatos.Columns[0].Visible = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;


        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {


            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "") {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                categoria.Nombre_categoria = txtNombre.Text;
                categoria.Descripcion = txtDesc.Text;


                int datos = categoria.AgregarCategoria();
                NuevoRegistro();
            }
           
        }


        private void mostrarDatos()
        {
            dgvDatos.DataSource = categoria.ListarCategorias();


        }

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

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


            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                categoria.Nombre_categoria = txtNombre.Text;
                categoria.Descripcion = txtDesc.Text;
                categoria.Id_categoria = Convert.ToInt32(txtID.Text);


                bool datos = categoria.EditarCategoria();
                NuevoRegistro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            categoria.Id_categoria = Convert.ToInt32(txtID.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?","Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if(dialaog == DialogResult.Yes)
            {
                bool datos = categoria.EliminarCategoria();
                NuevoRegistro();
            }

           
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
                dgvDatos.DataSource = categoria.ListarCategorias();
            }
        }
    }
}
