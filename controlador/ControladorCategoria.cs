using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionProductos.controlador
{
    internal class ControladorCategoria
    {
        // Declaración de variables privadas para los atributos de la categoría
        private int id_categoria;          
        private string nombre_categoria;   
        private string descripcion;         

        // Propiedades públicas para acceder y modificar las variables privadas para mayor seguridad
        public int Id_categoria
        {
            get => id_categoria;  
            set => id_categoria = value;  
        }

        public string Nombre_categoria
        {
            get => nombre_categoria; 
            set => nombre_categoria = value; 
        }

        public string Descripcion
        {
            get => descripcion;  
            set => descripcion = value;  
        }

        // Método para agregar una nueva categoría utilizando el modelo de categorías
        public int AgregarCategoria()
        {
            // Llama al método 'AgregarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloCategorias.AgregarRegistro(this);
        }

        // Método para listar todas las categorías llamando al modelo para mostrar registros
        public DataTable ListarCategorias()
        {
            // Llama al método 'MostrarRegistros' del modelo que devuelve un DataTable con las categorías
            return ModeloCategorias.MostrarRegistros();
        }

        // Método para editar una categoría existente utilizando el modelo de categorías
        public bool EditarCategoria()
        {
            // Llama al método 'ActualizarRegistro' del modelo y pasa la instancia de la clase actual ('this')
            return ModeloCategorias.ActualizarRegistro(this);
        }

        // Método para eliminar una categoría utilizando el modelo de categorías
        public bool EliminarCategoria()
        {
            // Llama al método 'EliminarRegistro' del modelo para eliminar la categoría
            return ModeloCategorias.EliminarRegistro(this);
        }

    }
}
