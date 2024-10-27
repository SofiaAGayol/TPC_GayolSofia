using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPC_GayolSofia
{
    public partial class Inicio : System.Web.UI.Page
    {

        int IDUsuarioActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IDUsuarioActivo"] != null)
            {
                int IDUsuarioActivo = (int)Session["IDUsuarioActivo"];

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                ClienteNegocio clienteNegocioActivo = new ClienteNegocio();
                Cliente clienteActivo = clienteNegocioActivo.ObtenerClientePorId(IDUsuarioActivo);


                Saludo.Text = "Hola, " + clienteActivo.Nombre ;
            }
            else
            {
                // Redirige al login si no hay un usuario en la sesión
                Response.Redirect("Login.aspx");
            }
        }





    }
}