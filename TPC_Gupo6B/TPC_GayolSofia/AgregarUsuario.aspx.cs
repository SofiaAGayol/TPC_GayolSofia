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

            // Verificar si el usuario ya existe
            if (usuarioNegocio.ExisteUsuario(usuario))
            {
                alertMessage.InnerText = "El usuario ya existe.";
                divAlert.Style["display"] = "block";
                return; 
            }

            // Verificar si el DNI ya existe
            if (usuarioNegocio.ExisteDNI(dni))
            {
                alertMessage.InnerText = "El DNI ya existe.";
                divAlert.Style["display"] = "block";
                return;
            }

            // Verificar si el email ya existe
            if (usuarioNegocio.ExisteEmail(email))
            {
                alertMessage.InnerText = "El email ya existe.";
                divAlert.Style["display"] = "block";
                return;
            }

            usuarioNegocio.Agregar(usuario, clave, nombre, apellido, dni, email, telefono, idRol);
            pnlMensaje.Style["display"] = "block"; // Mostrar el panel de éxito

            divAlert.Style["display"] = "none"; 
        }
        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            pnlMensaje.Style["display"] = "none";
            Response.Redirect("Usuarios.aspx");
        }
    }
}