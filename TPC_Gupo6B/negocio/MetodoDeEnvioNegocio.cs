using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MetodoDeEnvioNegocio
    {
        public List<MetodoDeEnvio> ListarTodos()
        {
            List<MetodoDeEnvio> listaMetodos = new List<MetodoDeEnvio>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDMetodoEnvio, Descripcion, CostoAMBA, CostoExterior FROM MetodosDeEnvio");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetodoDeEnvio metodo = new MetodoDeEnvio
                    {
                        IdMetodoEnvio = (int)datos.Lector["IDMetodoEnvio"],
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        CostoAMBA = (decimal)datos.Lector["CostoAMBA"],
                        CostoExterior = (decimal)datos.Lector["CostoExterior"]
                    };
                    listaMetodos.Add(metodo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los métodos de envío: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaMetodos;
        }
        public MetodoDeEnvio ObtenerMetodoEnvioPorID(int IdMetodoDeEnvio)
        {
            MetodoDeEnvio metodoDeEnvio = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDMetodoEnvio, Descripcion, CostoAMBA, CostoExterior FROM MetodosDeEnvio WHERE IDMetodoEnvio = @IdMetodoEnvio;");
                datos.setearParametro("@IdMetodoEnvio", IdMetodoDeEnvio);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    metodoDeEnvio = new MetodoDeEnvio
                    {
                        IdMetodoEnvio = (int)datos.Lector["IDMetodoEnvio"],
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        CostoAMBA = (decimal)datos.Lector["CostoAMBA"],
                        CostoExterior = (decimal)datos.Lector["CostoExterior"]
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el método de envío: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return metodoDeEnvio;
        }

    }
}
