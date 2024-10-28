using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GayolSofia
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioNegocio usuario = new UsuarioNegocio();
            Dgv_Usuarios.DataSource = usuario.Listar();
            Dgv_Usuarios.DataBind();

        }
    }
}