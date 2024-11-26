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
            //asigna los metodos para mostrar los proveedores existentes en combobox
            cboProveedores.DataSource = producto.ListarProveedores();
            cboProveedores.ValueMember = "Id_Proveedor";
            cboProveedores.DisplayMember = "Empresa";

            //asigna los metodos para mostrar las categorias existentes en combobox
            cboCategorias.DataSource = producto.ListarCategorias();
            cboCategorias.ValueMember = "Id_Categoria";
            cboCategorias.DisplayMember = "Nombre_Categoria";
            mostrarDatos();

            //ocultando columnas
            this.dgvDatos.Columns[0].Visible = false;
            this.dgvDatos.Columns[5].Visible = false;
            this.dgvDatos.Columns[6].Visible = false;
            this.dgvDatos.Columns[10].Visible = false;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //se vertifica que los campos del formulario no estén vacíos
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" || txtStock.Text.Trim() == "" || cboCategorias.SelectedIndex == 0 ||
                cboProveedores.SelectedIndex == 0 || picImagen.Image ==null)
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                //se le asignan a los atributos los valores del formulario
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDesc.Text;
                producto.Precio = (decimal)Convert.ToSingle(txtPrecio.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);
                producto.Id_categoria = Convert.ToInt32(cboCategorias.SelectedValue);
                producto.Id_proveedor = Convert.ToInt32(cboProveedores.SelectedValue);
                /*libreria para almecenaje temporal de la imagen*/
                MemoryStream ms1 = new MemoryStream();
                /*guarda la imagen en la memoria*/
                picImagen.Image.Save(ms1, ImageFormat.Jpeg);
                /*convierte a bytes la imagen de la memoria*/
                byte[] abyte = ms1.ToArray();
                /*agrega como argumento el byte al atributo imagen*/
                producto.Imagen = abyte;

                //se asignan los atributos y se ejecuta el metodo para insertar datos

                int datos = producto.AgregarProducto();
                //limpia los campos y actualiza el datagridview
                NuevoRegistro();
            }
           
        }

        //metodo que establece la fuente de datos del datagridview
        private void mostrarDatos()
        {
            dgvDatos.DataSource = producto.ListarProductos();


        }

        //metodo que vacía los textbox y actualiza los datos del datagridview
        private void NuevoRegistro()
        {
            txtDesc.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtNombre.Focus();
            txtPrecio.Clear();
            txtStock.Clear();
            cboProveedores.SelectedValue = 0;
            cboCategorias.SelectedValue = 0;
            mostrarDatos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = true;
            picImagen.Image = null;
        }
        private void frmProductos_Load(object sender, EventArgs e)
        {

        }

        //limpia los campos
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
        }

        //asigna los valores del registro selccionado a los textbox y la imagen al picturebox
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

            /*seleccionar el valor de la posicion 10 de la consulta*/
            if (dgvDatos[10, posicion].Value != DBNull.Value)
            {
                /*se selecciona el valor de la imgen*/
                byte[] imageBytes = (byte[])dgvDatos[10, posicion].Value;

                /*almacena el byte y lo convierte en la memoria*/
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    /*asigna la imagen al picturebox*/
                    picImagen.Image = Image.FromStream(ms);
                }
            }
            else
            {
                /*deja imagen null si esta no existe*/
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
                dgvDatos.DataSource = producto.ListarProductos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //se vertifica que los campos del formulario no estén vacíos
            if (txtNombre.Text.Trim() == "" || txtDesc.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" || txtStock.Text.Trim() == "" || cboCategorias.SelectedIndex == 0 ||
                cboProveedores.SelectedIndex == 0 || picImagen.Image == null)
            {
                MessageBox.Show("Los campos son requeridos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                //se le asignan a los atributos los valores del formulario
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDesc.Text;
                producto.Precio = (decimal)Convert.ToSingle(txtPrecio.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);
                producto.Id_categoria = Convert.ToInt32(cboCategorias.SelectedValue);
                producto.Id_proveedor = Convert.ToInt32(cboProveedores.SelectedValue);
                producto.Id_producto =  Convert.ToInt32(txtId.Text);

                //se evalua si hay o no una imagen en el picturebox
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
                    //si hay una imagen en el registro, la asigna 
                    producto.Imagen = producto.GetImagenExistente();
                }



                bool datos = producto.ActualizarProducto();
                NuevoRegistro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //se asigna el id del registro a eliminar
            producto.Id_producto = Convert.ToInt32(txtId.Text);
            DialogResult dialaog = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //si la respuesta es si, elimina el registro seleccionado
            if (dialaog == DialogResult.Yes)
            {
                bool datos = producto.EliminarProducto();
                NuevoRegistro();
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            //limita a 3 digitos y solo numeros
            Validaciones.SoloNumeros(e);
            Validaciones.LimiteCantidad(sender, e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando cantidades con decmilaes
            Validaciones.ValidarDecimales(sender, e);
        }


        /*abre una ventana para elegir la imagen del producto a ingresar*/
        /*validar para que sea una img*/
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
            //validando longitud de texto y algunos caracteres
            Validaciones.ValidarTextoLargo(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando longitud de texto y algunos caracteres
            Validaciones.ValidarTextoLargo(e);
        }
    }
}
