using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.vista
{
    public partial class FrmProductos : Form
    {
        ControladorProducto producto = new ControladorProducto();
        Validaciones Validaciones = new Validaciones();
        public FrmProductos()
        {
            InitializeComponent();
            cboProveedores.DataSource = producto.ListarProveedores();
            cboProveedores.ValueMember = "Id_Proveedor";
            cboProveedores.DisplayMember = "Empresa";


            cboCategorias.DataSource = producto.ListarCategorias();
            cboCategorias.ValueMember = "Id_Categoria";
            cboCategorias.DisplayMember = "Nombre_Categoria";
            mostrarDatos();

            this.dgvDatos.Columns[0].Visible = false;
            this.dgvDatos.Columns[5].Visible = false;
            this.dgvDatos.Columns[6].Visible = false;
            this.dgvDatos.Columns[10].Visible = false;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" || txtStock.Text.Trim() == "" || cboCategorias.SelectedIndex == 0 ||
                cboProveedores.SelectedIndex == 0 || picImagen.Image ==null)
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDesc.Text;
                producto.Precio = (decimal)Convert.ToSingle(txtPrecio.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);
                producto.Id_categoria = Convert.ToInt32(cboCategorias.SelectedValue);
                producto.Id_proveedor = Convert.ToInt32(cboProveedores.SelectedValue);
                MemoryStream ms1 = new MemoryStream();
                picImagen.Image.Save(ms1, ImageFormat.Jpeg);
                byte[] abyte = ms1.ToArray();
                producto.Imagen = abyte;

                int datos = producto.AgregarProducto();

                NuevoRegistro();
            }
           
        }

        private void mostrarDatos()
        {
            dgvDatos.DataSource = producto.ListarProductos();


        }

        private void NuevoRegistro()
        {
            txtDesc.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtNombre.Focus();
            txtPrecio.Clear();
            txtStock.Clear();
            cboProveedores.SelectedIndex = 0;
            cboCategorias.SelectedIndex = 0;
            mostrarDatos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = true;
            picImagen.Image = null;
        }
        private void frmProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int posicion = this.dgvDatos.CurrentRow.Index;
            txtId.Text = dgvDatos[0, posicion].Value.ToString();
            txtNombre.Text = dgvDatos[1, posicion].Value.ToString();
            txtPrecio.Text= dgvDatos[2, posicion].Value.ToString();
            txtDesc.Text = dgvDatos[7, posicion].Value.ToString();
            txtStock.Text = dgvDatos[8, posicion].Value.ToString();
            cboProveedores.SelectedValue = dgvDatos[6, posicion].Value.ToString();
            cboCategorias.SelectedValue = dgvDatos[5, posicion].Value.ToString();

            if (dgvDatos[9, posicion].Value != DBNull.Value) // Asegúrate de que el índice sea correcto
            {
                byte[] imageBytes = (byte[])dgvDatos[10, posicion].Value;

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picImagen.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picImagen.Image = null;
            }

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
                dgvDatos.DataSource = producto.ListarProductos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" || txtStock.Text.Trim() == "" || cboCategorias.SelectedIndex == 0 ||
                cboProveedores.SelectedIndex == 0 || picImagen.Image == null)
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDesc.Text;
                producto.Precio = (decimal)Convert.ToSingle(txtPrecio.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);
                producto.Id_categoria = Convert.ToInt32(cboCategorias.SelectedValue);
                producto.Id_proveedor = Convert.ToInt32(cboProveedores.SelectedValue);
                producto.Id_producto =  Convert.ToInt32(txtId.Text);

                if (picImagen.Image != null) 
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var clonedImage = new Bitmap(picImagen.Image);
                        clonedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        producto.Imagen = ms.ToArray(); 
                    }
                }
                else
                {
                    producto.Imagen = producto.GetImagenExistente();
                }



                bool datos = producto.ActualizarProducto();
                NuevoRegistro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            producto.Id_producto = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialaog == DialogResult.Yes)
            {
                bool datos = producto.EliminarProducto();
                NuevoRegistro();
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {

            Validaciones.SoloNumeros(e);
            Validaciones.LimiteCantidad(sender, e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarDecimales(sender, e);
        }

        public Image ByteToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data)) {
            
            
                return Image.FromStream(ms);
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = " Archivo de imagen(.jpg)|*.jpg|Archivo de imagen(.png)|*.png|Archivos de imagen(.jpeg)|*.jpeg|Todos los archivos (*.*)|*.*";
                DialogResult resultado = openFileDialog1.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    picImagen.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarTextoLargo(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ValidarTextoLargo(e);
        }
    }
}
