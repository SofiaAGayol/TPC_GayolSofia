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
    public partial class MisPrestamos : System.Web.UI.Page
    {
        PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
        LibroNegocio libroNegocio = new LibroNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPrestamos();
            }
        }


        private void CargarPrestamos()
        {
            Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];
            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            var prestamos = prestamoNegocio.ListarPrestamosPorUsuario(usuarioActivo.IdUsuario);

            var prestamosSimplificados = prestamos.Select(p => new
            {
                p.IDPrestamo,
                p.FechaInicio,
                p.MetodoEnvio,
                p.MetodoRetiro,
                p.Direccion,
                p.FechaFin,
                p.Estado,
                p.CostoEnvio,
                Libros = string.Join(", ", libroNegocio.ListarLibrosPorPrestamo(p.IDPrestamo).Select(l => l.Titulo + " - " + l.Autor.Nombre + " " + l.Autor.Apellido))
            }).ToList();

            Dgv_Prestamos.DataSource = prestamosSimplificados;
            Dgv_Prestamos.DataBind();
        }


        protected void gvPrestamos_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                var repeaterLibros = (System.Web.UI.WebControls.Repeater)e.Row.FindControl("rptLibros");
                if (repeaterLibros != null)
                {
                    var libros = DataBinder.Eval(e.Row.DataItem, "Libros") as List<object>;
                    repeaterLibros.DataSource = libros;
                    repeaterLibros.DataBind();
                }
            }
        }

        protected void Dgv_Prestamos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Dgv_Prestamos.PageIndex = e.NewPageIndex;
            CargarPrestamos();
        }


    }
}
