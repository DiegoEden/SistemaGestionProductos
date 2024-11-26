using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionProductos.controlador
{
    internal class ControladorProducto
    {
        // Declaración de variables privadas para los atributos del producto
        private int id_producto, id_usuario, id_categoria, id_proveedor, stock;  // ID's y stock del producto
        private string nombre, descripcion; 
        private decimal precio; 
        private byte[] imagen;  

        // Propiedades públicas para acceder y modificar las variables privadas
        public int Id_producto
        {
            get => id_producto; 
            set => id_producto = value; 
        }

        public int Id_usuario
        {
            get => id_usuario; 
            set => id_usuario = value; 
        }

        public int Id_categoria
        {
            get => id_categoria;  
            set => id_categoria = value;  
        }

        public int Id_proveedor
        {
            get => id_proveedor; 
            set => id_proveedor = value;  
        }

        public int Stock
        {
            get => stock;  
            set => stock = value;  
        }

        public string Nombre
        {
            get => nombre;  
            set => nombre = value;  
        }

        public string Descripcion
        {
            get => descripcion;  
            set => descripcion = value; 
        }

        public decimal Precio
        {
            get => precio;  
            set => precio = value;  
        }

        public byte[] Imagen
        {
            get => imagen;  
            set => imagen = value; 
        }

        // Método para agregar un nuevo producto utilizando el modelo de productos
        public int AgregarProducto()
        {
            // Llama al método 'AgregarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloProductos.AgregarRegistro(this);
        }

        // Método para listar todos los productos llamando al modelo para mostrar registros
        public DataTable ListarProductos()
        {
            // Llama al método 'MostrarRegistros' del modelo que devuelve un DataTable con los productos
            return ModeloProductos.MostrarRegistros();
        }

        // Método para listar todos los proveedores llamando al modelo para obtener los proveedores
        public DataTable ListarProveedores()
        {
            // Llama al método 'GetProveedores' del modelo que devuelve un DataTable con los proveedores
            return ModeloProductos.GetProveedores();
        }

        // Método para listar todas las categorías llamando al modelo para obtener las categorías
        public DataTable ListarCategorias()
        {
            // Llama al método 'GetCategorias' del modelo que devuelve un DataTable con las categorías
            return ModeloProductos.GetCategorias();
        }

        // Método para actualizar los datos de un producto utilizando el modelo de productos
        public bool ActualizarProducto()
        {
            // Llama al método 'ActualizarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloProductos.ActualizarRegistro(this);
        }

        // Método para obtener la imagen del producto a partir de su ID
        public byte[] GetImagenExistente()
        {
            // Llama al método 'ObtenerImagenPorId' del modelo que devuelve la imagen del producto en formato byte array
            return ModeloProductos.ObtenerImagenPorId(Id_producto);
        }

        // Método para eliminar un producto utilizando el modelo de productos
        public bool EliminarProducto()
        {
            // Llama al método 'EliminarRegistro' del modelo para eliminar el producto
            return ModeloProductos.EliminarRegistro(this);
        }


    }
}
