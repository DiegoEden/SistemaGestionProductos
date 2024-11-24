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

        private int id_proveedor;
        private string empresa, email, direccion, numero_telefono;

        public int Id_proveedor { get => id_proveedor; set => id_proveedor = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public string Email { get => email; set => email = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Numero_telefono { get => numero_telefono; set => numero_telefono = value; }

        public int AgregarProveedor()
        {

            return ModeloProveedores.AgregarRegistro(this);
        }

        public DataTable ListarProveedores()
        {
            return ModeloProveedores.MostrarRegistros();
        }

        public bool ActualizarProveedor()
        {
            return ModeloProveedores.ActualizarRegistro(this);
        }

        public bool EliminarProveedor()
        {
            return ModeloProveedores.EliminarRegistro(this);
        }
    }
}
