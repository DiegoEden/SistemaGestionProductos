using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionProductos.controlador
{
    internal class ControladorProveedor
    {
        // Declaración de variables privadas para los atributos de los proveedores
        private int id_proveedor;
        private string empresa, email, direccion, numero_telefono;

        // Propiedades públicas para acceder y modificar las variables privadas para mayor seguridad

        public int Id_proveedor { get => id_proveedor; set => id_proveedor = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public string Email { get => email; set => email = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Numero_telefono { get => numero_telefono; set => numero_telefono = value; }

        // Método para agregar un nuevo proveedor usando el modelo de proveedores
        public int AgregarProveedor()
        {
            // Llama al método 'AgregarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloProveedores.AgregarRegistro(this);
        }

        // Método para listar todos los proveedores
        public DataTable ListarProveedores()
        {
            // Llama al método 'MostrarRegistros' del modelo que devuelve un DataTable con los proveedores
            return ModeloProveedores.MostrarRegistros();
        }

        // Método para actualizar los datos de un proveedor
        public bool ActualizarProveedor()
        {
            // Llama al método 'ActualizarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloProveedores.ActualizarRegistro(this);
        }

        // Método para eliminar un proveedor usando el modelo de proveedores
        public bool EliminarProveedor()
        {
            // Llama al método 'EliminarRegistro' del modelo para eliminar el proveedor de la base de datos
            return ModeloProveedores.EliminarRegistro(this);
        }
    }
}
