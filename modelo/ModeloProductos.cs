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


            DataTable data;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProveedores", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();


                adapter.Fill(data);


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


        public static DataTable GetCategorias()
        {
            DataTable data;
            try
            {
                SqlCommand cmd = new SqlCommand("GetCategorias", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();

                adapter.Fill(data);

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

        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
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

        public static int AgregarRegistro(ControladorProducto add)
        {
            int retorno = 0;

            try
            {
                SqlCommand cmdadd = new SqlCommand("AgregarProducto", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;

                cmdadd.Parameters.AddWithValue("@NombreProducto", add.Nombre);
                cmdadd.Parameters.AddWithValue("@Precio", add.Precio);
                cmdadd.Parameters.AddWithValue("@IdProveedor", add.Id_proveedor);
                cmdadd.Parameters.AddWithValue("@IdCategoria", add.Id_categoria);
                cmdadd.Parameters.AddWithValue("@Descripcion", add.Descripcion);
                cmdadd.Parameters.AddWithValue("@Stock", add.Stock);
                cmdadd.Parameters.AddWithValue("@IdUsuario", VariablesGlobales.UsuarioID);
                cmdadd.Parameters.AddWithValue("@Imagen", add.Imagen);


                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

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


        public static bool ActualizarRegistro(ControladorProducto upd)
        {
            bool retorno = false;
            try
            {
                using (SqlCommand cmdupdate = new SqlCommand("ActualizarProductos", Conexion.GetConnection()))
                {
                    cmdupdate.CommandType = CommandType.StoredProcedure;

                    cmdupdate.Parameters.AddWithValue("@NombreProducto", upd.Nombre);
                    cmdupdate.Parameters.AddWithValue("@Precio", upd.Precio);
                    cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor);
                    cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria);
                    cmdupdate.Parameters.AddWithValue("@Descripcion", upd.Descripcion);
                    cmdupdate.Parameters.AddWithValue("@Stock", upd.Stock);
                    cmdupdate.Parameters.AddWithValue("@IdProducto", upd.Id_producto);

                    if (upd.Imagen != null && upd.Imagen.Length > 0)
                    {
                        cmdupdate.Parameters.AddWithValue("@Imagen", upd.Imagen);
                    }
                    else
                    {
                        byte[] imagenExistente = ObtenerImagenPorId(upd.Id_producto);
                        cmdupdate.Parameters.AddWithValue("@Imagen", imagenExistente ?? (object)DBNull.Value);
                    }

                    retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());

                    if (retorno)
                    {
                        MessageBox.Show("Datos actualizados correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
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

        public static byte[] ObtenerImagenPorId(int id_producto)
        {
            byte[] imagen = null;

            try
            {
                using (SqlConnection connection = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Productos WHERE IdProducto = @IdProducto", connection))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", id_producto);

                        connection.Open();
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            imagen = (byte[])result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener la imagen existente: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return imagen;
        }

        public static bool EliminarRegistro(ControladorProducto upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("EliminarProducto", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                cmdupdate.Parameters.AddWithValue("@IdProducto", upd.Id_producto);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
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
