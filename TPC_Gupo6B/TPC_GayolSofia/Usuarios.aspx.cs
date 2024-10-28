using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.negocio;

namespace TPC_GayolSofia
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Solo carga los datos si no es un postback
            {
                UsuarioNegocio usuario = new UsuarioNegocio();
                Dgv_Usuarios.DataSource = usuario.Listar(); // Asegúrate de que este método retorna la lista correcta
                Dgv_Usuarios.DataBind();
            }
        }
        public string ObtenerNombreRol(int IDRol)
        {
            RolNegocio rolNegocio = new RolNegocio(); // Crea una instancia de RolNegocio
            return rolNegocio.NombreRol(IDRol); // Llama al método y devuelve el nombre del rol
        }

        protected void BtnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUsuario.aspx");
        }
    }
}