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
        //metodo para obtener los datos de la tabla categorias
        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                /*creando consulta*/
                string query = "SELECT * FROM  categorias";
                SqlCommand cmdselect = new SqlCommand(string.Format(query), Conexion.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(cmdselect);
                data = new DataTable();
                //llenado la los datos en un array datatable para colocar en datagridview
                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {

                return data = new DataTable();
            }
        }

        //metodo para agregar registros a la tabla categorias
        public static int AgregarRegistro(ControladorCategoria add)
        {
            int retorno = 0;

            try
            {
                //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmdadd = new SqlCommand("AgregarCategoria", Conexion.GetConnection());
                cmdadd.CommandType = CommandType.StoredProcedure;

                //agregando atributos del controlador a los parametros del procedimiento almacenado
                cmdadd.Parameters.AddWithValue("@NombreCategoria", add.Nombre_categoria);
                cmdadd.Parameters.AddWithValue("@Descripcion", add.Descripcion);

                retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

                //si retorna un numero mayor o igual a 1, notifica de la inserción
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

        //metodo para actualizar registros de la tabla categorias
        public static bool ActualizarRegistro(ControladorCategoria upd)
        {
            bool retorno = false;
            try
            {
                //indicando el nombre del procedimiento almcacenado a utilizar
                SqlCommand cmdupdate = new SqlCommand("ActualizarCategoria", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                //agregando atributos del controlador a los parametros del procedimiento almacenado
                cmdupdate.Parameters.AddWithValue("@NombreCategoria", upd.Nombre_categoria);
                cmdupdate.Parameters.AddWithValue("@Descripcion", upd.Descripcion);
                cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
                //si retorna true, notifica de la actualización
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

        //metodo para eliminar registros de la tabla categorias
        public static bool EliminarRegistro(ControladorCategoria upd)
        {
            bool retorno = false;
            try
            {
                //se da el nombre del procedimiento almacenado a usar
                SqlCommand cmdupdate = new SqlCommand("EliminarCategoria", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                //agregando atributos del controlador a los parametros del procedimiento almacenado
                cmdupdate.Parameters.AddWithValue("@IdCategoria", upd.Id_categoria);

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
