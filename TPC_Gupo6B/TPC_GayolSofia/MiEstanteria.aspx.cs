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
    public partial class MiEstanteria : System.Web.UI.Page
    {
        Usuario usuarioActivo;
        Libro libroActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CargarLibros();
            }
        }

        protected void CargarLibros()
        {
            usuarioActivo = (Usuario)Session["UsuarioActivo"];
            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.ListarLibrosEnPrestamoPorUsuario(usuarioActivo.IdUsuario);

            Session.Add("listaEnPrestamo", negocio.ListarLibrosEnPrestamoPorUsuario(usuarioActivo.IdUsuario));

            RepeaterArticulos.DataSource = Session["listaEnPrestamo"];
            RepeaterArticulos.DataBind();
        }
        protected void botonDevolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("DevolverLibro.aspx");
        }
    }
}