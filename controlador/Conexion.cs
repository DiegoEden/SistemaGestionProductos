using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.controlador
{
    internal class Conexion
    {
        // Método que devuelve una conexión a la base de datos SQL Server
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            string servidor = "LAPTOP-9P87HD02";// servidor SQL
            string base_datos = "gestionProductos";  // Nombre de la base de datos

            try
            {
                // Crear el string de conexión usando el servidor y la base de datos
                string connectionString = $"Server={servidor};Database={base_datos};Integrated Security=True;";

                // Crear la instancia de SqlConnection usando la cadena de conexión
                conn = new SqlConnection(connectionString);

                // Intentar abrir la conexión a la base de datos
                conn.Open();

                // Si la conexión se abrió correctamente, devolver la conexión
                return conn;
            }
            catch (Exception e)
            {
                // Si hay un error, devolver null para indicar que la conexión no fue exitosa

                MessageBox.Show("Ha ocurrido un error en la conexión: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return null;
            }
        }


    }
}
