using SistemaGestionProductos.controlador;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.modelo
{
    internal class ModeloProveedores
    {

        // Método para mostrar todos los registros de proveedores
        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                // Consulta  para obtener todos los registros'
                string query = "SELECT * FROM proveedores";
                SqlCommand cmdselect = new SqlCommand(string.Format(query), Conexion.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(cmdselect);
                data = new DataTable();

                // Llenado de la tabla 'data' con los resultados obtenidos de la consulta
                adapter.Fill(data);
                return data; 
            }
            catch (Exception)
            {
                return data = new DataTable();
            }
        }

        // Método para agregar un nuevo proveedor
        public static int AgregarRegistro(ControladorProveedor add)
        {
            int retorno = 0;

            try
            {
                // Se ejecuta el procedimiento almacenado 'AgregarProveedor'
                SqlCommand cmdadd = new SqlCommand("AgregarProveedor", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;

                // Agrega los parámetros necesarios al procedimiento almacenado
                cmdadd.Parameters.AddWithValue("@Empresa", add.Empresa); 
                cmdadd.Parameters.AddWithValue("@Email", add.Email); 
                cmdadd.Parameters.AddWithValue("@Direccion", add.Direccion); 
                cmdadd.Parameters.AddWithValue("@NumeroTelefono", add.Numero_telefono);

                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

                // Si el retorno es mayor o igual a 1, se notifica de la operacion
                if (retorno >= 1)
                {
                    MessageBox.Show("Proveedor ingresado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Proveedor no ingresado", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        // Método para actualizar los registros de un proveedor
        public static bool ActualizarRegistro(ControladorProveedor upd)
        {
            bool retorno = false;

            try
            {
                // Se ejecuta el procedimiento almacenado 'ActualizarProveedor'
                SqlCommand cmdupdate = new SqlCommand("ActualizarProveedor", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                // Se agregan los parámetros con los valores del proveedor a actualizar
                cmdupdate.Parameters.AddWithValue("@Empresa", upd.Empresa);
                cmdupdate.Parameters.AddWithValue("@Email", upd.Email);
                cmdupdate.Parameters.AddWithValue("@Direccion", upd.Direccion);
                cmdupdate.Parameters.AddWithValue("@NumeroTelefono", upd.Numero_telefono);
                cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());

                // Si la actualización fue exitosa, se muestra un mensaje de confirmación
                if (retorno == true)
                {
                    MessageBox.Show("Datos actualizados correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Datos no actualizados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        // Método para eliminar un proveedor
        public static bool EliminarRegistro(ControladorProveedor upd)
        {
            bool retorno = false;

            try
            {
                // Se ejecuta el procedimiento almacenado 'EliminarProveedor'
                SqlCommand cmdupdate = new SqlCommand("EliminarProveedor", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                // Se agrega el parámetro para identificar al proveedor a eliminar
                cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());

                // Si la eliminación fue exitosa, muestra un mensaje de confirmación
                if (retorno == true)
                {
                    MessageBox.Show("Registro eliminado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Si no se eliminó el registro, muestra un mensaje de error
                    MessageBox.Show("Registro no eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

    }
}
