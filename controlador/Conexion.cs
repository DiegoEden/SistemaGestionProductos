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
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            string servidor = "LAPTOP-9P87HD02";
            string base_datos = "gestionProductos"; 
          

            try
            {

                string connectionString = $"Server={servidor};Database={base_datos};Integrated Security=True;";



                conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error en la conexión: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null; 
            }
        }

    }
}
