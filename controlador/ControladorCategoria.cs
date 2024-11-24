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
        private int id_categoria;
        private string nombre_categoria, descripcion;

        public int Id_categoria { get => id_categoria; set => id_categoria = value; }
        public string Nombre_categoria { get => nombre_categoria; set => nombre_categoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }


        public int AgregarCategoria()
        {

            return ModeloCategorias.AgregarRegistro(this);
        } 

        public DataTable ListarCategorias()
        {
            return ModeloCategorias.MostrarRegistros();
        }

        public bool EditarCategoria()
        {

            return ModeloCategorias.ActualizarRegistro(this);
        }


        public bool EliminarCategoria() { 
        

            return ModeloCategorias.EliminarRegistro(this);
        
        }
    }
}
