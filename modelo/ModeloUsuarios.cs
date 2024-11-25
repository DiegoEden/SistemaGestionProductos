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

        public static bool Acceso(ControladorUsuario usuario)
        {
            bool retorno = false;

            try
            {

                string query = "SELECT * FROM usuarios where Usuario =@usuario COLLATE SQL_Latin1_General_CP1_CS_AS";

                SqlCommand cmd = new SqlCommand(query, Conexion.GetConnection());
                cmd.Parameters.Add(new SqlParameter("usuario", usuario.Usuario));
                retorno = Convert.ToBoolean(cmd.ExecuteScalar());

                if (retorno == true) {


                    string query2 = "SELECT * FROM  Usuarios WHERE Usuario =@usuario COLLATE SQL_Latin1_General_CP1_CS_AS and Contra=@contra";
                    SqlCommand cmd2 = new SqlCommand(query2, Conexion.GetConnection());
                    cmd2.Parameters.Add(new SqlParameter("usuario", usuario.Usuario));
                    cmd2.Parameters.Add(new SqlParameter("contra", usuario.Contra));
                    retorno = Convert.ToBoolean(cmd2.ExecuteScalar());

                    if (retorno == true) { 

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {

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
                Conexion.GetConnection().Close();
            }
        }


        public static int AgregarRegistro(ControladorUsuario add)
        {
            int retorno = 0;

            try
            {
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
                    MessageBox.Show("Usuario ingresada correctamente", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no ingresada", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return retorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un problema: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }
        }

        public static bool ActualizarPerfil(ControladorUsuario upd)
        {
            bool retorno = false;
            try
            {
                SqlCommand cmdupdate = new SqlCommand("ActualizarUsuario", Conexion.GetConnection());
                cmdupdate.CommandType = CommandType.StoredProcedure;

                cmdupdate.Parameters.AddWithValue("@Nombre", upd.Nombre);
                cmdupdate.Parameters.AddWithValue("@Apellido", upd.Apellido);
                cmdupdate.Parameters.AddWithValue("@Direccion",  upd.Direccion);
                cmdupdate.Parameters.AddWithValue("@Usuario",upd.Usuario);
                cmdupdate.Parameters.AddWithValue("@Correo", upd.Correo);
                cmdupdate.Parameters.AddWithValue("@FechaNac", upd.Fecha_nacimiento);
                cmdupdate.Parameters.AddWithValue("@IdUsuario", upd.Id_usuario);



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



        public static DataTable MostrarRegistros()
        {
            DataTable data;
            try
            {
                SqlCommand cmd = new SqlCommand("GetUsuarios", Conexion.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", VariablesGlobales.UsuarioID);

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


    }
}
