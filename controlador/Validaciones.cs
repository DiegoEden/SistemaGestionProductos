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

        public void SoloLetras(KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);

        }

        public void SoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void ValidarUsername(KeyPressEventArgs e) {

            char[] caracteresNoPermitidos = { '\'', '\"', ';', '-', '=', '(', ')', '{', '}', '[', ']', '\\', '/', '<', '>', '%', '*' };

            if (Array.Exists(caracteresNoPermitidos, caracter => caracter == e.KeyChar))
            {
                e.Handled = true;
            }

        }

        public void LimiteCantidad(object sender, KeyPressEventArgs e) {


            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length >= 3 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void SetLongitudValores(object sender, KeyPressEventArgs e, int longitud)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length >= longitud && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        public void ValidarDecimales(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Permitir números, un solo punto decimal y la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquear la tecla
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }

            // Validar que solo haya dos decimales
            if (textBox.Text.Contains(".") && textBox.Text.Split('.')[1].Length >= 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        public void ValidarTextoLargo(KeyPressEventArgs e)
        {
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


        public bool ValidarCorreo(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento > hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad >= 18;
        }

    }
}
