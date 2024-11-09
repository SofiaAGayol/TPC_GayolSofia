using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class Carrito : System.Web.UI.Page
    {
        Usuario usuarioActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActivo"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            usuarioActivo = (Usuario)Session["UsuarioActivo"];
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }
        private void CargarCarrito()
        {
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            List<Libro> librosEnCarrito = carritoNegocio.CargarCarrito(usuarioActivo.IdUsuario);

            if (librosEnCarrito.Count > 0)
            {
                PanelNoLibros.Visible = false;
                RepeaterCarrito.DataSource = librosEnCarrito;
                RepeaterCarrito.DataBind();
            }
            else
            {
                PanelNoLibros.Visible = true;
                RepeaterCarrito.DataSource = null;
                RepeaterCarrito.DataBind();
            }

            lblTotalLibros.Text = librosEnCarrito.Count.ToString();
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int idLibro = Convert.ToInt32(btnEliminar.CommandArgument);

            CarritoNegocio carritoNegocio = new CarritoNegocio();
            carritoNegocio.EliminarLibroDelCarrito(usuarioActivo.IdUsuario, idLibro);

            CargarCarrito();
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCarrito();
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Lógica para solicitar préstamo
        }

    }
}