using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GayolSofia
{
    public partial class Libros : System.Web.UI.Page
    {
        LibroNegocio libroNegocio = new LibroNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Solo carga los datos si no es un postback
            {
                CargarLibros();


            }
        }

        private void CargarLibros()
        {
            Dgv_Libros.DataSource = libroNegocio.Listar(); 
            Dgv_Libros.DataBind();
        }
        protected void Dgv_Libros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Dgv_Libros.PageIndex = e.NewPageIndex;
            CargarLibros(); 
        }
        protected void Dgv_Libros_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = Dgv_Libros.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarUsuario.aspx?id=" + id);
        }

        protected void BtnAgregarLibro_Click(object sender, EventArgs e)
        {

        }
    }
}