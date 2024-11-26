using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.controlador
{
    internal class ControladorUsuario
    {
        // Declaración de variables privadas para los atributos de los usuarios
        private int id_usuario;
        private string nombre, apellido, direccion, usuario, contra, correo;
        private DateTime fecha_nacimiento;

        // Defincion de atributos para accesar a las variables privadas
        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contra { get => contra; set => contra = value; }
        public string Correo { get => correo; set => correo = value; }
        public DateTime Fecha_nacimiento { get => fecha_nacimiento; set => fecha_nacimiento = value; }


        // Método para verificar el acceso de un usuario, para iniciar sesión
        public bool Acceso()
        {
            // Llama al método 'Acceso' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloUsuarios.Acceso(this);
        }

        // Método para agregar un nuevo usuario a la base de datos
        public int AgregarUsuario()
        {
            // Llama al método 'AgregarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloUsuarios.AgregarRegistro(this);
        }

        // Método para actualizar el perfil de un usuario
        public bool ActualizarUsuario()
        {
            // Llama al método 'ActualizarPerfil' del modelo para actualizar los datos del usuario
            return ModeloUsuarios.ActualizarPerfil(this);
        }

        // Método para eliminar un usuario de la base de datos
        public bool EliminarUsuario()
        {
            // Llama al método 'EliminarUsuario' del modelo para eliminar al usuario
            return ModeloUsuarios.EliminarUsuario(this);
        }

        // Método para listar todos los usuarios en la base de datos
        public DataTable ListarUsuarios()
        {
            // Llama al método 'MostrarRegistros' del modelo para obtener un DataTable con los usuarios
            return ModeloUsuarios.MostrarRegistros();
        }


    }
}
