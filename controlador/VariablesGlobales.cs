using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionProductos.controlador
{
    public static class VariablesGlobales
    {
        //Variables globales estáticas y de acceso común para poder acceder a ellas desde cualquier parte del sistema
        //estas son usadas en la sesión actual, para mostrar datos de la personas logueada
        public static int UsuarioID { get; set; }
        public static string Usuario { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string Direccion { get; set; }
        public static string Correo { get; set; }

        public static DateTime FechaNac { get; set; }


    }
}
