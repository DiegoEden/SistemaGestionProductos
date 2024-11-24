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
     class ModeloCategorias
    {

        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                string query = "SELECT * FROM  categorias";
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


        public static int AgregarRegistro(ControladorCategoria add)
        {
            int retorno = 0;

            try
            {
                SqlCommand cmdadd = new SqlCommand("AgregarCategoria", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;

                cmdadd.Parameters.AddWithValue("@NombreCategoria", add.Nombre_categoria);
                cmdadd.Parameters.AddWithValue("@Descripcion", add.Descripcion);

                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

                if (retorno >= 1)
                {
                    MessageBox.Show("Categoría ingresada correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Categoría no ingresada", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        public static bool ActualizarRegistro(ControladorCategoria upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("ActualizarCategoria", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                cmdupdate.Parameters.AddWithValue("@NombreCategoria", upd.Nombre_categoria);
                cmdupdate.Parameters.AddWithValue("@Descripcion", upd.Descripcion);
                cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria);

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


        public static bool EliminarRegistro(ControladorCategoria upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("EliminarCategoria", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria);

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
