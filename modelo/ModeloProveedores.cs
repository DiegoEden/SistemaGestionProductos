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

        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                string query = "SELECT * FROM  proveedores";
                SqlCommand cmdselect = new SqlCommand(string.Format(query), Conexion.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(cmdselect);
                data = new DataTable();

                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {

                return data = new DataTable();
            }
        }


        public static int AgregarRegistro(ControladorProveedor add)
        {
            int retorno = 0;

            try
            {
                SqlCommand cmdadd = new SqlCommand("AgregarProveedor", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;

                cmdadd.Parameters.AddWithValue("@Empresa", add.Empresa);
                cmdadd.Parameters.AddWithValue("@Email", add.Email);
                cmdadd.Parameters.AddWithValue("@Direccion", add.Direccion);
                cmdadd.Parameters.AddWithValue("@NumeroTelefono", add.Numero_telefono);


                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

                if (retorno >= 1)
                {
                    MessageBox.Show("Proveedor ingresado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Proveedor no ingresada", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        public static bool ActualizarRegistro(ControladorProveedor upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("ActualizarProveedor", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                cmdupdate.Parameters.AddWithValue("@Empresa", upd.Empresa);
                cmdupdate.Parameters.AddWithValue("@Email", upd.Email);
                cmdupdate.Parameters.AddWithValue("@Direccion", upd.Direccion);
                cmdupdate.Parameters.AddWithValue("@NumeroTelefono", upd.Numero_telefono);
                cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
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

                MessageBox.Show("Ha ocurrido un problema" + e, "Error critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }


        public static bool EliminarRegistro(ControladorProveedor upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("EliminarProveedor", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                cmdupdate.Parameters.AddWithValue("@IdProveedor", upd.Id_proveedor);

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
