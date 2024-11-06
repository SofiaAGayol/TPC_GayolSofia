using negocio;
using dominio;
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
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnBaja.Visible = false;
            CbBajaDef.Visible = false;
            btnBajaDef.Visible = false;
            btnRestablecer.Visible = false;
            btnGuardar.Visible = false;
            btnModificar.Visible = false;

            //NORMALMENTE SERIA PARA AGREGAR
            if (!IsPostBack)
            {
                CargarRoles();
                btnGuardar.Visible = true;

                //SI SE LE PASA POR PARAMETRO UN ID VA A HABILITARSE PARA "DAR BAJA"
                if (Request.QueryString["id"] != null)
                {
                    id = int.Parse(Request.QueryString["id"]);

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    Usuario usuario = new Usuario();
                    usuario = usuarioNegocio.ObtenerUsuarioPorId(id);

                    txtUsuario.Text = usuario.NombreUsuario;
                    txtClave.Text = usuario.Clave;
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtDNI.Text = usuario.DNI;
                    txtEmail.Text = usuario.Email;
                    txtTelefono.Text = usuario.Telefono;

                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnBaja.Visible = true;
                    CbBajaDef.Visible = false;
                    btnBajaDef.Visible = false;

                    //SI SE LE PASA POR PARAMETRO UN ID Y ADEMÁS ESTA DADO DE BAJA SE ACTIVA EL BOTON "RESTABLECER"
                    if (!usuarioNegocio.estaBaja(id))
                    {
                        btnModificar.Visible = true;
                        btnBaja.Visible = false;
                        CbBajaDef.Visible = false;
                        btnBajaDef.Visible = false;
                        btnRestablecer.Visible = true;
                    }
                }
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

            string mensajeError = string.Empty;

            if (string.IsNullOrEmpty(usuario))
            {
                mensajeError = "Usuario no válido.";
            }
            else if (usuarioNegocio.ExisteUsuario(usuario))
            {
                mensajeError = "Usuario ya registrado.";
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
            else if (string.IsNullOrEmpty(dni))
            {
                mensajeError = "DNI no válido.";
            }
            else if (usuarioNegocio.ExisteDNI(dni))
            {
                mensajeError = "DNI ya registrado.";
            }
            else if (string.IsNullOrEmpty(email))
            {
                mensajeError = "Email no válido.";
            }
            else if (usuarioNegocio.ExisteEmail(email))
            {
                mensajeError = "Email ya registrado.";
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                mensajeError = "Telefono no válido.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

                usuarioNegocio.Agregar(usuario, clave, nombre, apellido, dni, email, telefono, idRol);
                pnlMensaje.Style["display"] = "block";

                divAlert.Style["display"] = "none";
            
        }


        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            pnlMensaje.Style["display"] = "none";
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            btnBaja.Visible = false;
            CbBajaDef.Visible = true;
            btnBajaDef.Visible = true;
        }

        protected void btnBajaDef_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (CbBajaDef.Checked)
            {
                negocio.BajaLogica(id);
                Response.Redirect("Usuarios.aspx");
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            UsuarioNegocio negocio = new UsuarioNegocio();

            negocio.RestablecerLogica(id);
            Response.Redirect("Usuarios.aspx");
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            btnModificar.Visible = true;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            string usuario = txtUsuario.Text;
            string clave = txtClave.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string dni = txtDNI.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;
            int idRol = Convert.ToInt32(ddlRol.SelectedValue);

            string mensajeError = string.Empty;

            if (string.IsNullOrEmpty(usuario))
            {
                mensajeError = "Usuario no válido.";
            }
            else if (usuarioNegocio.ExisteUsuarioNuevo(usuario,id))
            {
                mensajeError = "Usuario ya registrado.";
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
            else if (string.IsNullOrEmpty(dni))
            {
                mensajeError = "DNI no válido.";
            }
            else if (usuarioNegocio.ExisteDNINuevo(dni,id))
            {
                mensajeError = "DNI ya registrado.";
            }
            else if (string.IsNullOrEmpty(email))
            {
                mensajeError = "Email no válido.";
            }
            else if (usuarioNegocio.ExisteEmailNuevo(email,id))
            {
                mensajeError = "Email ya registrado.";
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                mensajeError = "Telefono no válido.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }
            usuarioNegocio.Modificar(id,usuario, clave, nombre, apellido, dni, email, telefono, idRol);



        }

    }

}



