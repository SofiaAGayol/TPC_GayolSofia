using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Data.SqlClient;

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


            int resultado = VerificarEmailYContrasena(usuario, contrasenia);

            switch (resultado)
            {
                case 0:
                    LblError.Text = "El user ingresado no se encuentra en nuestra Base de datos.";
                    break;
                case 1:
                    LblError.Text = "";
                    LblError.Text = "Contraseña no coincide.";
                    break;
                case 2:
                    usuarioActivo = Tb_Usuario.Text;
                    Session["Usuario"] = usuarioActivo;
                    Response.Redirect("Inicio.aspx");
                    break;

            }
        }









        ///ESTO DEBERIA IR EN NEGOCIO//
        public int VerificarEmailYContrasena(string usuario, string contrasenia)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=BIBLIO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "SELECT CASE " +
                                      "WHEN Usuario = @Usuario AND Clave = @Contrasenia THEN 2 " +
                                      "WHEN Usuario = @Usuario THEN 1 " +
                                      "ELSE 0 " +
                                      "END " +
                                      "FROM Usuarios " +
                                      "WHERE Usuario = @Usuario;";
                comando.Parameters.AddWithValue("@Usuario", usuario);
                comando.Parameters.AddWithValue("@Contrasenia", contrasenia);
                comando.Connection = conexion;

                conexion.Open();

                object resultObj = comando.ExecuteScalar();

                if (resultObj != null)
                {
                    int result = (int)resultObj;
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}


