using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.controlador
{
    internal class Validaciones
    {

        // Permite solo letras, números y espacio, y bloquea cualquier otro carácter
        public void SoloLetras(KeyPressEventArgs e)
        {
            // Si la tecla presionada no es retroceso, espacio, letra o número, se bloquea la entrada
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        // Permite solo números, bloquea cualquier otro carácter
        public void SoloNumeros(KeyPressEventArgs e)
        {
            // Si la tecla presionada no es un número o control (como retroceso), se bloquea la entrada
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Bloquea caracteres no permitidos para el nombre de usuario
        public void ValidarUsername(KeyPressEventArgs e)
        {
            // Definir un arreglo con caracteres no permitidos
            char[] caracteresNoPermitidos = { '\'', '\"', ';', '-', '=', '(', ')', '{', '}', '[', ']', '\\', '/', '<', '>', '%', '*' };

            // Si la tecla presionada está en el arreglo de caracteres no permitidos, se bloquea
            if (Array.Exists(caracteresNoPermitidos, caracter => caracter == e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Limita la cantidad de caracteres que se pueden ingresar a 3 en un TextBox
        public void LimiteCantidad(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Si el texto tiene 3 caracteres o más y la tecla presionada no es de control, se bloquea la entrada
            if (textBox.Text.Length >= 3 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Limita la longitud de caracteres que se pueden ingresar en un TextBox, según un valor especificado
        public void SetLongitudValores(object sender, KeyPressEventArgs e, int longitud)
        {
            TextBox textBox = sender as TextBox;

            // Si el texto alcanza la longitud especificada y la tecla presionada no es de control, se bloquea la entrada
            if (textBox.Text.Length >= longitud && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Valida la entrada de texto para asegurarse de que sea un número decimal con hasta 2 decimales
        public void ValidarDecimales(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Permite números, un solo punto decimal y la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquear la tecla
            }

            // Permite solo un punto decimal
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }

            // Valida que solo haya dos decimales
            if (textBox.Text.Contains(".") && textBox.Text.Split('.')[1].Length >= 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Valida que el texto ingresado solo contenga caracteres permitidos para un campo de texto largo
        public void ValidarTextoLargo(KeyPressEventArgs e)
        {
            // Bloquea cualquier carácter que no sea letra, número, espacio, algunos caracteres especiales o control
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != ' ' &&
                e.KeyChar != '.' &&
                e.KeyChar != ',' &&
                e.KeyChar != '-' &&
                e.KeyChar != '#' &&
                e.KeyChar != '/' &&
                e.KeyChar != '(' &&
                e.KeyChar != ')' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Valida un correo electrónico mediante expresión regular
        public bool ValidarCorreo(string email)
        {
            // Expresión regular para validar un correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Si el correo no coincide con el patrón, devuelve false
            if (!Regex.IsMatch(email, pattern))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Verifica si una persona es mayor de edad
        public bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            // Si aún no ha cumplido años este año, disminuye la edad
            if (fechaNacimiento > hoy.AddYears(-edad))
            {
                edad--;
            }

            // Devuelve verdadero si la edad es 18 o más
            return edad >= 18;
        }


    }
}
