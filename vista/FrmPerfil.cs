using SistemaGestionProductos.controlador;
using SistemaGestionProductos.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionProductos.vista
{
    public partial class FrmPerfil : Form
    {
        ControladorUsuario controlador = new ControladorUsuario();
        Validaciones Validaciones = new Validaciones();
        public FrmPerfil()
        {
            InitializeComponent();
        }

        private void FrmPerfil_Load(object sender, EventArgs e)
        {

            //llamando el metodo GetInfo
            GetInfo();
        }


        //metodo para obtener la información de las variables de sesion y asignarlas a los textbox
        private void GetInfo()
        {
            txtApellido.Text = VariablesGlobales.Apellido;
            txtNombre.Text = VariablesGlobales.Nombre;  
            txtMail.Text = VariablesGlobales.Correo;
            txtUsuario.Text = VariablesGlobales.Usuario;
            txtId.Text = VariablesGlobales.UsuarioID.ToString();
            txtDireccion.Text = VariablesGlobales.Direccion;
            dtpNacimiento.Value = VariablesGlobales.FechaNac;
            lblNombre.Text ="Hola, "+ VariablesGlobales.Nombre + " " + VariablesGlobales.Apellido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //validando que no existan valores nulos o vacios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //validando el correo electróncico
            else if (Validaciones.ValidarCorreo(txtMail.Text) == false)
            {

                MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus();
            }
            //validando que la fecha de nacimiento concuerde con la mayoria de edad
            else if (!Validaciones.EsMayorDeEdad(dtpNacimiento.Value))
            {
                MessageBox.Show("El usuario debe ser mayor de 18 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //si todo está correcto, asgina los valores del textbox a los atributos del controlador
                controlador.Nombre = txtNombre.Text.Trim();
                controlador.Apellido = txtApellido.Text.Trim();
                controlador.Direccion = txtDireccion.Text.Trim();
                controlador.Usuario = txtUsuario.Text.Trim();
                controlador.Correo = txtMail.Text.Trim();
                controlador.Fecha_nacimiento = dtpNacimiento.Value;
                controlador.Id_usuario = VariablesGlobales.UsuarioID;
                bool datos = controlador.ActualizarUsuario();


                if (datos == true)
                {
                    //si ha cambiado el nombre de usuario, se reinicia el sistema para volver a iniciar sesión
                    if (txtUsuario.Text.Trim() != VariablesGlobales.Usuario)
                    {
                        DialogResult dialog = MessageBox.Show("Ha actualizado su nombre de usuario, por cuestiones de seguridad " +
                            "la aplicación se reiniciará", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (dialog == DialogResult.OK)
                        {
                            Application.Restart();

                        }
                    }

                    //se vuelven a asignar los valores, por si se han cambiado
                    VariablesGlobales.Apellido = txtApellido.Text ;
                    VariablesGlobales.Nombre = txtNombre.Text ;
                    VariablesGlobales.Correo = txtMail.Text;
                    VariablesGlobales.Usuario = txtUsuario.Text;
                    txtId.Text = VariablesGlobales.UsuarioID.ToString();
                    VariablesGlobales.Direccion = txtDireccion.Text;
                    VariablesGlobales.FechaNac = dtpNacimiento.Value;
                    string mensaje = "Hola, " + VariablesGlobales.Nombre + " " + VariablesGlobales.Apellido ;
                    lblNombre.Text = mensaje ;
                }

               


            }


        }

        

       

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando ingreso de datos y longitud
            Validaciones.ValidarUsername(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando ingreso de datos y longitud
            Validaciones.SoloLetras(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando ingreso de datos y longitud
            Validaciones.SoloLetras(e);
            Validaciones.SetLongitudValores(sender, e, 50);

        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validando ingreso de datos y longitud
            Validaciones.ValidarTextoLargo(e);
            Validaciones.SetLongitudValores(sender, e, 255);

        }
    }
}
