using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Data.SqlClient;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class LogIn : System.Web.UI.Page
    {
        //USUARIO CON EL QUE SE VA A INICIAR SESION:
        private string usuarioActivo;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Btn_Ingreso_Click(object sender, EventArgs e)
        {
            string usuario = Tb_Usuario.Text;
            string contrasenia = Tb_Contrasenia.Text;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            int resultado = usuarioNegocio.VerificarEmailYContrasena(usuario, contrasenia);

            switch (resultado)
            {
                case 0:
                    alertMessage.InnerText = "El usuario ingresado no se encuentra registrado.";
                    divAlert.Style["display"] = "block"; 
                    break;
                case 1:
                    alertMessage.InnerText = "La contraseña no coincide con el usuario al que desea acceder.";
                    divAlert.Style["display"] = "block";
                    break;
                case 2:
                    int IDUsuarioActivo = usuarioNegocio.ObtenerIdUsuario(usuario);

                    Usuario usuarioActivo = usuarioNegocio.ObtenerUsuarioPorId(IDUsuarioActivo);

                    Session.Add("UsuarioActivo", usuarioActivo);

                    Response.Redirect("Inicio.aspx");
                    break;
            }
        }
    }
}


