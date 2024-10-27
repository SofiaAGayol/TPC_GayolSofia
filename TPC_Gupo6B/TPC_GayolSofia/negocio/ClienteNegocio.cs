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
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=BIBLIO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;

                // Asegúrate de incluir todos los campos requeridos en la consulta SELECT
                comando.CommandText = "SELECT DNI, Nombre, Apellido, Email, Telefono FROM Clientes WHERE IDUsuario = @IDUsuario";
                comando.Parameters.AddWithValue("@IDUsuario", idUsuario);
                comando.Connection = conexion;

                conexion.Open();

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    // Crear el objeto Cliente y llenar sus propiedades con los datos del lector
                    Cliente cliente = new Cliente
                    {
                        DNI = Convert.ToInt32(lector["DNI"]),
                        Nombre = lector["Nombre"].ToString(),
                        Apellido = lector["Apellido"].ToString(),
                        Email = lector["Email"].ToString(),
                        Telefono = lector["Telefono"].ToString(),
                    };

                    return cliente;
                }
                else
                {
                    return null;
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

