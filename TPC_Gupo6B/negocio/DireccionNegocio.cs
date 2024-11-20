using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class DireccionNegocio
    {

        public List<Direccion> ListarDireccionesPorUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Direccion> listaDirecciones = new List<Direccion>();

            try
            {
                datos.setearConsulta(
                    "SELECT IDDireccion, IDUsuario, Calle, Altura, CodigoPostal, Aclaracion, Predeterminada " +
                    "FROM Direccion " +
                    "WHERE IDUsuario = @IDUsuario " +
                    "ORDER BY Predeterminada DESC");

                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion direccion = new Direccion
                    {
                        IdDireccion = (int)datos.Lector["IDDireccion"],
                        Usuario = new Usuario { IdUsuario = (int)datos.Lector["IDUsuario"] },
                        Calle = datos.Lector["Calle"].ToString(),
                        Altura = (int)datos.Lector["Altura"],
                        CodigoPostal = datos.Lector["CodigoPostal"].ToString(),
                        Aclaracion = datos.Lector["Aclaracion"] != DBNull.Value ? datos.Lector["Aclaracion"].ToString() : null,
                        Predeterminada = (bool)datos.Lector["Predeterminada"]
                    };

                    listaDirecciones.Add(direccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las direcciones: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaDirecciones;
        }

        public int AgregarYRetornarId(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Direccion (IDUsuario, Calle, Altura, CodigoPostal, Aclaracion, Predeterminada) " +
                                     "VALUES (@IDUsuario, @Calle, @Altura, @CodigoPostal, @Aclaracion, @Predeterminada); SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@IDUsuario", direccion.Usuario.IdUsuario);
                datos.setearParametro("@Calle", direccion.Calle);
                datos.setearParametro("@Altura", direccion.Altura);
                datos.setearParametro("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametro("@Aclaracion", direccion.Aclaracion);
                datos.setearParametro("@Predeterminada", direccion.Predeterminada);

                return Convert.ToInt32(datos.ejecutarAccion()); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la dirección: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int ObtenerIdDireccion(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDDireccion FROM Direccion WHERE IDUsuario = @IDUsuario AND Calle = @Calle AND Altura = @Altura AND CodigoPostal = @CodigoPostal");
                datos.setearParametro("@IDUsuario", direccion.Usuario.IdUsuario);
                datos.setearParametro("@Calle", direccion.Calle);
                datos.setearParametro("@Altura", direccion.Altura);
                datos.setearParametro("@CodigoPostal", direccion.CodigoPostal);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDDireccion"];
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la dirección: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Direccion ObtenerDireccionPorID(int idUsuario)
        {
            Direccion direccion = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDDireccion, IDUsuario, Calle, Altura, CodigoPostal, Aclaracion, Predeterminada " +
                                     "FROM Direccion " +
                                     "WHERE IDUsuario = @IDUsuario AND Predeterminada = 1");

                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    direccion = new Direccion
                    {
                        IdDireccion = (int)datos.Lector["IDDireccion"],
                        Usuario = new Usuario { IdUsuario = (int)datos.Lector["IDUsuario"] },
                        Calle = datos.Lector["Calle"].ToString(),
                        Altura = (int)datos.Lector["Altura"],
                        CodigoPostal = datos.Lector["CodigoPostal"].ToString(),
                        Aclaracion = datos.Lector["Aclaracion"].ToString(),
                        Predeterminada = (bool)datos.Lector["Predeterminada"]
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la dirección: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return direccion;
        }
    }
}
