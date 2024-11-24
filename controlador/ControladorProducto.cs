using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionProductos.controlador
{
    internal class ControladorProducto
    {

        private int id_producto, id_usuario, id_categoria, id_proveedor, stock;
        private string nombre, descripcion;
        private decimal precio;
        private byte[] imagen;

        public int Id_producto { get => id_producto; set => id_producto = value; }
        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public int Id_categoria { get => id_categoria; set => id_categoria = value; }
        public int Id_proveedor { get => id_proveedor; set => id_proveedor = value; }
        public int Stock { get => stock; set => stock = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public byte[] Imagen { get => imagen; set => imagen = value; }



    }
}
