using negocio;
using dominio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.negocio;

namespace TPC_GayolSofia
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Solo carga los datos si no es un postback
            {
                CargarUsuarios();


            }
        }

        private void CargarUsuarios()
        {
            Dgv_Usuarios.DataSource = usuarioNegocio.Listar(); 
            Dgv_Usuarios.DataBind();
        }


        protected void BtnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUsuario.aspx");
        }

        protected void Dgv_Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = Dgv_Usuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarUsuario.aspx?id=" + id);
        }

        protected void Dgv_Usuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Dgv_Usuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }
    }
}