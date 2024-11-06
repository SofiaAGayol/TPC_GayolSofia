using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GayolSofia
{
    public partial class Autores : System.Web.UI.Page
    {
        private AutorNegocio autorNegocio = new AutorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarAutores();
        }

        private void CargarAutores()
        {
            Dgv_Autores.DataSource = autorNegocio.Listar();
            Dgv_Autores.DataBind();
        }


        protected void BtnAgregarAutor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarAutor.aspx");
        }

        protected void Dgv_Autores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = Dgv_Autores.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarAutor.aspx?id=" + id);
        }

        protected void Dgv_Autores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Dgv_Autores.PageIndex = e.NewPageIndex;
            CargarAutores();
        }
    }
}