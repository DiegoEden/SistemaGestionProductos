using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;

namespace SistemaGestionProductos.modelo
{
    internal class ModeloUsuarios
    {


        // Método para verificar si un usuario existe en la base de datos y realizar el acceso.
        public static bool Acceso(ControladorUsuario usuario)
        {
            bool retorno = false;

            try
            {
                // Consulta para verificar si el usuario existe en la base de datos.
                /*el collate compara el texto ingresado con lo almacenado en la base de datos,
                 haciendo de este sensible a mayusculas y minusculas y sensible a acentos*/

                /*Ejemplo, Usuario, usuario y USUARIO se consideran diferente*/
                
                string query = "SELECT * FROM usuarios where Usuario =@usuario COLLATE SQL_Latin1_General_CP1_CS_AS";
                SqlCommand cmd = new SqlCommand(query, Conexion.GetConnection());
                cmd.Parameters.Add(new SqlParameter("usuario", usuario.Usuario));

                // Se ejecuta la consulta para verificar si el usuario existe.
                retorno = Convert.ToBoolean(cmd.ExecuteScalar());

                if (retorno == true)
                {
                    // Si el usuario existe, se verifica si la contraseña es correcta.
                    string query2 = "SELECT * FROM  Usuarios WHERE Usuario =@usuario COLLATE SQL_Latin1_General_CP1_CS_AS and Contra=@contra";
                    SqlCommand cmd2 = new SqlCommand(query2, Conexion.GetConnection());
                    cmd2.Parameters.Add(new SqlParameter("usuario", usuario.Usuario));
                    cmd2.Parameters.Add(new SqlParameter("contra", usuario.Contra));

                    // si la contraseña también es correcta, se procede a obtener los detalles del usuario.
                    retorno = Convert.ToBoolean(cmd2.ExecuteScalar());
                    if (retorno == true)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //asigna los valores encontrados a las variables del constructor
                            usuario.Id_usuario = reader.GetInt32(0);
                            usuario.Nombre = reader.GetString(1);
                            usuario.Apellido = reader.GetString(2);
                            usuario.Direccion = reader.GetString(3);
                            usuario.Correo = reader.GetString(6);
                            usuario.Fecha_nacimiento = reader.GetDateTime(7);

                            MessageBox.Show("Acceso concedido: " + usuario.Usuario, "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("error en la base de datos" + e, "error critico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw;
            }
            finally
            {
             //cierra la conexión después de la ejecución.
                Conexion.GetConnection().Close();
            }
        }

        // Método para agregar un nuevo usuario.
        public static int AgregarRegistro(ControladorUsuario add)
        {
            int retorno = 0;

            try
            {
                // Verificar si el nombre de usuario ya existe en la base de datos.
                SqlCommand check = new SqlCommand("CheckNewUsername", Conexion.GetConnection());
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@Usuario", add.Usuario);
                retorno = Convert.ToInt32(check.ExecuteScalar());

                if (retorno >= 1)
                {
                    // Mensaje si el nombre de usuario ya existe.
                    MessageBox.Show("Este nombre de usuario ya existe en nuestros registros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retorno = 1;
                }
                else
                {
                   //de lo contrario, se agrega el nuevo usuario.
                    SqlCommand cmdadd = new SqlCommand("AgregarUsuario", Conexion.GetConnection());
                    cmdadd.CommandType = CommandType.StoredProcedure;

                    cmdadd.Parameters.AddWithValue("@Nombre", add.Nombre);
                    cmdadd.Parameters.AddWithValue("@Apellido", add.Apellido);
                    cmdadd.Parameters.AddWithValue("@Direccion", add.Direccion);
                    cmdadd.Parameters.AddWithValue("@Usuario", add.Usuario);
                    cmdadd.Parameters.AddWithValue("@Contra", add.Contra);
                    cmdadd.Parameters.AddWithValue("@Correo", add.Correo);
                    cmdadd.Parameters.AddWithValue("@FechaNac", add.Fecha_nacimiento);

                    retorno = Convert.ToInt32(cmdadd.ExecuteNonQuery());

                    if (retorno >= 1)
                    {
                        // mensaje de éxito si el usuario se agregó correctamente.
                        MessageBox.Show("Usuario ingresado correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        retorno = 2;
                    }
                    else
                    {
                        MessageBox.Show("Usuario no ingresado", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        // Método para actualizar los datos de un usuario.
        public static bool ActualizarPerfil(ControladorUsuario upd)
        {
            bool retorno = false;
            try
            {
                // verifica si el nombre de usuario ya existe en la base de datos.
                SqlCommand check = new SqlCommand("CheckUsername", Conexion.GetConnection());
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@Usuario", upd.Usuario);
                check.Parameters.AddWithValue("@IdUsuario", upd.Id_usuario);
                retorno = Convert.ToBoolean(check.ExecuteScalar());

                if (retorno == true)
                {
                    MessageBox.Show("Este nombre de usuario ya existe en nuestros registros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retorno = false;
                }
                else
                {
                    // Si el nombre de usuario no existe, se actualizan los datos del usuario.
                    SqlCommand cmdupdate = new SqlCommand("ActualizarUsuario", Conexion.GetConnection());
                    cmdupdate.CommandType = CommandType.StoredProcedure;

                    cmdupdate.Parameters.AddWithValue("@Nombre", upd.Nombre);
                    cmdupdate.Parameters.AddWithValue("@Apellido", upd.Apellido);
                    cmdupdate.Parameters.AddWithValue("@Direccion", upd.Direccion);
                    cmdupdate.Parameters.AddWithValue("@Usuario", upd.Usuario);
                    cmdupdate.Parameters.AddWithValue("@Correo", upd.Correo);
                    cmdupdate.Parameters.AddWithValue("@FechaNac", upd.Fecha_nacimiento);
                    cmdupdate.Parameters.AddWithValue("@IdUsuario", upd.Id_usuario);

                    retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
                    if (retorno == true)
                    {
                        // Mensaje de éxito si los datos fueron actualizados correctamente.
                        MessageBox.Show("Datos actualizados correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        retorno = true;
                    }
                    else
                    {
                        MessageBox.Show("Datos no actualizados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema" + e, "Error critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        // Método para eliminar un usuario.
        public static bool EliminarUsuario(ControladorUsuario upd)
        {
            bool retorno = false;
            try
            {
                // Ejecutar el procedimiento almacenado para eliminar el usuario.
                SqlCommand cmdupdate = new SqlCommand("EliminarUsuario", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;
                cmdupdate.Parameters.AddWithValue("@IdUsuario", upd.Id_usuario);

                retorno = Convert.ToBoolean(cmdupdate.ExecuteNonQuery());
                if (retorno == true)
                {
                    // Mensaje de éxito si el registro fue eliminado correctamente.
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

        // Método para obtener los registros de los usuarios.
        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                // Se ejecuta el procedimiento almacenado para obtener los registros de los usuarios.
                SqlCommand cmd = new SqlCommand("GetUsuarios", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", VariablesGlobales.UsuarioID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                data = new DataTable();

                // Se llena la tabla con los datos obtenidos.
                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {
                return data = new DataTable();
            }
        }



    }
}
