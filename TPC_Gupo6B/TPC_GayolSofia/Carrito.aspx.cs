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
            int idUsuario = usuarioActivo.IdUsuario;
        }
        protected void btnMenos_Click(object sender, EventArgs e)
        {
            // Lógica para prestamo inmediato
        }
        protected void btnMas_Click(object sender, EventArgs e)
        {
            // Lógica para prestamo inmediato
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Lógica para prestamo inmediato
        }


    }
}