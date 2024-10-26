using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GayolSofia
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                string usuarioActivo = Session["Usuario"].ToString();
                Label1.Text = "Hola, " + usuarioActivo;
            }
            else
            {
                // Redirige al login si no hay un usuario en la sesión
                Response.Redirect("Login.aspx");
            }

        }
    }
}