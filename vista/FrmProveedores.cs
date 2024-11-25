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
        private void mostrarDatos()
        {
            dgvDatos.DataSource = proveedor.ListarProveedores();


        }

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

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    

            if (txtEmpresa.Text.Trim() == "" || txtMail.Text.Trim() == "" || txtDireccion.Text.Trim() == "" ||mskTel.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if (!Regex.IsMatch(txtMail.Text, pattern))
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else
            {
               
                proveedor.Empresa = txtEmpresa.Text;
                proveedor.Email = txtMail.Text;
                proveedor.Numero_telefono = mskTel.Text;
                proveedor.Direccion = txtDireccion.Text;

                int datos = proveedor.AgregarProveedor();
                NuevoRegistro();

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int posicion = this.dgvDatos.CurrentRow.Index;
            txtId.Text = dgvDatos[0, posicion].Value.ToString();
            txtEmpresa.Text = dgvDatos[1, posicion].Value.ToString();
            txtMail.Text = dgvDatos[2, posicion].Value.ToString();
            txtDireccion.Text = dgvDatos[3, posicion].Value.ToString();
            mskTel.Text = dgvDatos[4, posicion].Value.ToString();



            btnIngresar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
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
                dgvDatos.DataSource = proveedor.ListarProveedores();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {


            if (txtEmpresa.Text.Trim() == "" || txtMail.Text.Trim() == "" || txtDireccion.Text.Trim() == "" || mskTel.Text.Trim() == "")
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            else
            {

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
            proveedor.Id_proveedor = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialaog == DialogResult.Yes)
            {
                bool datos = proveedor.ActualizarProveedor();
                NuevoRegistro();
            }
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

        }

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
    }
}
