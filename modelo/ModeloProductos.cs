using SistemaGestionProductos.controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.modelo
{
    internal class ModeloProductos
    {

        public static DataTable GetProveedores()
        {

            //metodo para llenar combobox de proveedores
            DataTable data;
            try
            {
                //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmd = new SqlCommand("GetProveedores", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();

                //llenado adaptador para fuente de datos del combobox
                adapter.Fill(data);


                //agregando una fila para indicar selección del dato
                DataRow newRow = data.NewRow();
                newRow["Id_Proveedor"] = 0;
                newRow["Empresa"] = "Seleccione...";
                data.Rows.InsertAt(newRow, 0);
                return data;
            }
            catch (Exception)
            {

                return data = new DataTable();
            }

        }

        //metodo para llenar combobox de proveedores
        public static DataTable GetCategorias()
        {
            DataTable data;
            try
            {   //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmd = new SqlCommand("GetCategorias", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();

                //llenado adaptador para fuente de datos del combobox
                adapter.Fill(data);

                //agregando una fila para indicar selección del dato
                DataRow newRow = data.NewRow();
                newRow["Id_Categoria"] = 0; 
                newRow["Nombre_Categoria"] = "Seleccione...";
                data.Rows.InsertAt(newRow, 0);

                return data;
            }
            catch (Exception)
            {
                return data = new DataTable();
            }
        }

        //metodo para mostrar productos
        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {    
               //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmd = new SqlCommand("GetProductosList", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();


                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {

                return data = new DataTable();
            }
        }

        //metodo para agregar registros a la tabla productos
        public static int AgregarRegistro(ControladorProducto add)
        {
            int retorno = 0;

            try
            {
                //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmdadd = new SqlCommand("AgregarProducto", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;
                //agregando atributos del controlador a los parametros del procedimiento almacenado
                cmdadd.Parameters.AddWithValue("@NombreProducto", add.Nombre);
                cmdadd.Parameters.AddWithValue("@Precio", add.Precio);
                cmdadd.Parameters.AddWithValue("@IdProveedor", add.Id_proveedor);
                cmdadd.Parameters.AddWithValue("@IdCategoria", add.Id_categoria);
                cmdadd.Parameters.AddWithValue("@Descripcion", add.Descripcion);
                cmdadd.Parameters.AddWithValue("@Stock", add.Stock);
                cmdadd.Parameters.AddWithValue("@IdUsuario", VariablesGlobales.UsuarioID);
                cmdadd.Parameters.AddWithValue("@Imagen", add.Imagen);


                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());
                //si retorna un numero mayor o igual a 1, notifica de la inserción

                if (retorno >= 1)
                {
                    MessageBox.Show("Producto ingresado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Producto no ingresado", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        //metodo para actualizar registros de la tabla productos
        public static bool ActualizarRegistro(ControladorProducto upd)
        {
            bool retorno = false;

            try
            {
                // Se ejecuta el procedimiento almacenado ActualizarProductos 
                using (SqlCommand cmdupdate = new SqlCommand("ActualizarProductos", Conexion.GetConnection()))
                {
                    cmdupdate.CommandType = CommandType.StoredProcedure; // Especifica que se ejecutará un procedimiento almacenado

                    //agregando atributos del controlador a los parametros del procedimiento almacenado
                    cmdupdate.Parameters.AddWithValue("@NombreProducto", upd.Nombre); 
                    cmdupdate.Parameters.AddWithValue("@Precio", upd.Precio); 
                    cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor); 
                    cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria); 
                    cmdupdate.Parameters.AddWithValue("@Descripcion", upd.Descripcion);
                    cmdupdate.Parameters.AddWithValue("@Stock", upd.Stock); 
                    cmdupdate.Parameters.AddWithValue("@IdProducto", upd.Id_producto); 

                    // Verifica si hay una nueva imagen para el producto
                    if (upd.Imagen != null && upd.Imagen.Length > 0)
                    {
                        // Si hay imagen, se agrega como parámetro
                        cmdupdate.Parameters.AddWithValue("@Imagen", upd.Imagen);
                    }
                    else
                    {
                        // Si no hay imagen, se obtiene la imagen existente del producto
                        byte[] imagenExistente = ObtenerImagenPorId(upd.Id_producto);
                        // Si no existe imagen, se agrega un valor nulo
                        cmdupdate.Parameters.AddWithValue("@Imagen", imagenExistente ?? (object)DBNull.Value);
                    }

                    //si retorna true, notifica de la actualización
                    retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
                    if (retorno)
                    {
                        MessageBox.Show("Datos actualizados correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Si no se actualizaron los datos, muestra un mensaje de error
                        MessageBox.Show("Datos no actualizados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return retorno;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        //metodo donde se obtiene la imagen existente del producto
        public static byte[] ObtenerImagenPorId(int id_producto)
        {
            byte[] imagen = null;

            try
            {
                //se obtiene la conexion
                using (SqlConnection connection = Conexion.GetConnection())
                {
                    //se crea la consulta
                    using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Productos WHERE IdProducto = @IdProducto", connection))
                    {
                        //se le agregarn parametros necesarios
                        cmd.Parameters.AddWithValue("@IdProducto", id_producto);

                        connection.Open();
                        var result = cmd.ExecuteScalar();
                        //se evalua si el resultado es nulo o si hay un dato en el campo solicitado
                        if (result != DBNull.Value && result != null)
                        {
                            //se le asigna el resultado de la consulta (un byte) al valro a retornar
                            imagen = (byte[])result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener la imagen existente: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //se retorna el dato de la imagen
            return imagen;
        }

        //metodo para eliminar registros de la tabla categorias
        public static bool EliminarRegistro(ControladorProducto upd)
        {
            bool retorno = false;
            try
            {
                //se da el nombre del procedimiento almacenado a usar
                SqlCommand cmdupdate = new SqlCommand("EliminarProducto", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                //agregando atributos del controlador a los parametros del procedimiento almacenado
                cmdupdate.Parameters.AddWithValue("@IdProducto", upd.Id_producto);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
                //si retorna true, notifica de la eliminación
                if (retorno == true)
                {
                    MessageBox.Show("Registro eliminado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registro no eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                return retorno;


            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un problema" + e, "Error critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

    }
}
