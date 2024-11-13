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
        public bool EsCodigoPostalAMBA(string codigoPostal)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM CodigosPostalesAMBA WHERE CodigoPostal = @CodigoPostal");
                datos.setearParametro("@CodigoPostal", codigoPostal);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    return count > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el código postal: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //METODOS DE ENVIO
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
        public decimal ObtenerCostoEnvio(string codigoPostal, int IdMetodoDeEnvio)
        {
            MetodoDeEnvio metodoEnvio = ObtenerMetodoEnvioPorID(IdMetodoDeEnvio);
            bool esAMBA = EsCodigoPostalAMBA(codigoPostal);

            if (metodoEnvio != null)
            {
                return esAMBA ? metodoEnvio.CostoAMBA : metodoEnvio.CostoExterior;
            }

            return 0;
        }

        //METODOS DE RETIRO
        public List<MetodoDeRetiro> ListarTodosRetiro()
        {
            List<MetodoDeRetiro> lista = new List<MetodoDeRetiro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDMetodoRetiro, Descripcion, CostoAMBA, CostoExterior FROM MetodosDeRetiro");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetodoDeRetiro metodo = new MetodoDeRetiro
                    {
                        IdMetodoRetiro = (int)datos.Lector["IDMetodoRetiro"],
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        CostoAMBA = (decimal)datos.Lector["CostoAMBA"],
                        CostoExterior = (decimal)datos.Lector["CostoExterior"]
                    };
                    lista.Add(metodo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }
        public MetodoDeRetiro ObtenerMetodoRetiroPorID(int IdMetodoDeRetiro)
        {
            MetodoDeRetiro metodoDeRetiro = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDMetodoRetiro, Descripcion, CostoAMBA, CostoExterior FROM MetodosDeRetiro WHERE IDMetodoRetiro = @IdMetodoRetiro;");
                datos.setearParametro("@IdMetodoRetiro", IdMetodoDeRetiro);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    metodoDeRetiro = new MetodoDeRetiro
                    {
                        IdMetodoRetiro = (int)datos.Lector["IDMetodoRetiro"],
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        CostoAMBA = (decimal)datos.Lector["CostoAMBA"],
                        CostoExterior = (decimal)datos.Lector["CostoExterior"]
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el método de retiro: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return metodoDeRetiro;
        }
        public decimal ObtenerCostoRetiro(string codigoPostal, int IdMetodoDeRetiro)
        {
            MetodoDeRetiro metodoDeRetiro = ObtenerMetodoRetiroPorID(IdMetodoDeRetiro);
            bool esAMBA = EsCodigoPostalAMBA(codigoPostal);

            if (metodoDeRetiro != null)
            {
                return esAMBA ? metodoDeRetiro.CostoAMBA : metodoDeRetiro.CostoExterior;
            }

            return 0;
        }
    }
}
