using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class Inicio : System.Web.UI.Page
    {

        int IDUsuarioActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActivo"] != null)
            {
                Usuario usuarioActivo = Session["UsuarioActivo"] as Usuario;

                if (usuarioActivo != null)
                {
                    Saludo.Text = "Hola, " + usuarioActivo.Nombre;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }





    }
}