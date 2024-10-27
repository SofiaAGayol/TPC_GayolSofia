using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace negocio
{
    public class ClienteNegocio
    {

        public Cliente ObtenerClientePorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT DNI, Nombre, Apellido, Email, Telefono FROM Usuarios WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        DNI = Convert.ToInt32(datos.Lector["DNI"]),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                    };

                    return cliente;
                }
                else
                {
                    return null; // Usuario no encontrado
                }
            }
            catch (Exception ex)
            {
                throw ex; // Considera registrar el error o manejarlo de manera más específica
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }

}

