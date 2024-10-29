using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;
using TPC_GayolSofia.negocio;

namespace TPC_GayolSofia
{
    public partial class AgregarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        private void CargarRoles()
        {
            RolNegocio rolNegocio = new RolNegocio();
            List<Rol> roles = rolNegocio.Listar();

            ddlRol.DataSource = roles;
            ddlRol.DataTextField = "Descripcion";
            ddlRol.DataValueField = "IDRol";
            ddlRol.DataBind();


            if (ddlRol.Items.Count > 0)
            {
                ddlRol.SelectedIndex = 0;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            string usuario = txtUsuario.Text;
            string clave = txtClave.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string dni = txtDNI.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;
            int idRol = Convert.ToInt32(ddlRol.SelectedValue);

            // Inicializa un mensaje de error
            string mensajeError = string.Empty;

            // Verificar si el usuario ya existe o si da null
            if (string.IsNullOrEmpty(usuario) || usuarioNegocio.ExisteUsuario(usuario))
            {
                mensajeError = "Usuario no válido.";
            }
            else if (string.IsNullOrEmpty(clave))
            {
                mensajeError = "Clave no válida.";
            }
            else if (string.IsNullOrEmpty(nombre))
            {
                mensajeError = "Nombre no válido.";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                mensajeError = "Apellido no válido.";
            }

            else if (string.IsNullOrEmpty(apellido) || usuarioNegocio.ExisteDNI(dni))
            {
                mensajeError = "DNI no válido.";
            }
            else if (string.IsNullOrEmpty(email) || usuarioNegocio.ExisteEmail(email))
            {
                mensajeError = "Email no válido.";
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                mensajeError = "Telefono no válido.";
            }

            // Si hay un mensaje de error, mostrarlo y salir del método
            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

            // Si todas las verificaciones son exitosas, agregar el nuevo usuario
            usuarioNegocio.Agregar(usuario, clave, nombre, apellido, dni, email, telefono, idRol);
            pnlMensaje.Style["display"] = "block"; // Mostrar el panel de éxito

            // Ocultar la alerta
            divAlert.Style["display"] = "none";
        }


        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            pnlMensaje.Style["display"] = "none";
            Response.Redirect("Usuarios.aspx");
        }
    }
}