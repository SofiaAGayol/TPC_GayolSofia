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
                    LblError.Text = "El usuario ingresado no se encuentra en nuestra Base de datos.";
                    break;
                case 1:
                    LblError.Text = "Contraseña no coincide.";
                    break;
                case 2:
                    int IDUsuarioActivo = usuarioNegocio.ObtenerIdUsuario(usuario);

                    // Obtén el objeto Usuario completo
                    Usuario usuarioActivo = usuarioNegocio.ObtenerUsuarioPorId(IDUsuarioActivo);

                    // Guarda el objeto Usuario en la sesión utilizando Session.Add
                    Session.Add("UsuarioActivo", usuarioActivo);

                    // Redirige a la página de inicio
                    Response.Redirect("Inicio.aspx");
                    break;
            }
        }
    }
}


