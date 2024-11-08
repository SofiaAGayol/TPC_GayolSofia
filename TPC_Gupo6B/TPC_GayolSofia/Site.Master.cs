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

                    if (usuarioActivo.Rol.IDRol == 4)
                    {
                        LogoLink.HRef = "Home.aspx";
                        pnlCartIcon.Visible = true;
                        Session["ItemCount"] = 1;// contarLibros(); aca tengo que hacer la funcion para contar libros del carrito
                    }
                    else
                    {
                        LogoLink.HRef = "Informes.aspx";
                        pnlCartIcon.Visible = false;
                    }
                }
                else
                {
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