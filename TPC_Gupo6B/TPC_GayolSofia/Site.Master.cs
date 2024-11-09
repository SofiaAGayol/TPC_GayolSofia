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
    public partial class SiteMaster : MasterPage
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UsuarioActivo"] != null)
                {
                    Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];
                    CarritoNegocio carritoNegocio = new CarritoNegocio();

                    menu.Visible = true;
                    lblNombreUsuario.Text = $"{usuarioActivo.Nombre} {usuarioActivo.Apellido}";

                    if (usuarioActivo.Rol.IDRol == 4)
                    {
                        pnlClientNav.Visible = true;
                        LogoLink.HRef = "Home.aspx";
                        pnlCartIcon.Visible = true;
                        int cantCarrito = carritoNegocio.ContarLibrosEnCarrito(usuarioActivo.IdUsuario);
                        Session["ItemCount"] = cantCarrito;
                    }
                    else
                    {
                        pnlClientNav.Visible = false;
                        LogoLink.HRef = "Informes.aspx";
                        pnlCartIcon.Visible = false;
                    }
                }
                else
                {
                    menu.Visible = false;
                    LogoLink.HRef = "Login.aspx";
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("UsuarioActivo");
            Session.Clear();

            Response.Redirect("Login.aspx");
        }
    }
}