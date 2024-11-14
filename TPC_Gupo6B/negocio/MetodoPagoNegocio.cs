using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;
using dominio;

namespace negocio
{
    public class MetodoDePagoNegocio
    {
        public List<MetodoPago> ListarPorUsuario(int idUsuario)
        {
            List<MetodoPago> listaMetodosPago = new List<MetodoPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDMetodoPago, TipoTarjeta, NroTarjeta, Vencimiento, Cod, Predeterminado FROM MetodoDePago WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetodoPago metodoPago = new MetodoPago
                    {
                        IdMetodoPago = (int)datos.Lector["IDMetodoPago"],
                        Usuario = new Usuario { IdUsuario = idUsuario },
                        TipoTarjeta = datos.Lector["TipoTarjeta"].ToString(),
                        NroTarjeta = datos.Lector["NroTarjeta"].ToString(),
                        Vencimiento = (DateTime)datos.Lector["Vencimiento"],
                        Cod = datos.Lector["Cod"].ToString()
                    };
                    listaMetodosPago.Add(metodoPago);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los métodos de pago: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaMetodosPago;
        }
        public void Guardar(MetodoPago metodoPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MetodoDePago (IDUsuario, TipoTarjeta, NroTarjeta, Vencimiento, Cod, Predeterminado) VALUES (@IDUsuario, @TipoTarjeta, @NroTarjeta, @Vencimiento, @Cod, @Predeterminado)");
                datos.setearParametro("@IDUsuario", metodoPago.Usuario.IdUsuario);
                datos.setearParametro("@TipoTarjeta", metodoPago.TipoTarjeta);
                datos.setearParametro("@NroTarjeta", metodoPago.NroTarjeta);
                datos.setearParametro("@Vencimiento", metodoPago.Vencimiento);
                datos.setearParametro("@Cod", metodoPago.Cod);
                datos.setearParametro("@Predeterminado", false);  

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el método de pago: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ActualizarMetodoPago(MetodoPago metodoPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MetodoDePago SET TipoTarjeta = @TipoTarjeta, NroTarjeta = @NroTarjeta, Vencimiento = @Vencimiento, Cod = @Cod WHERE IDMetodoPago = @IDMetodoPago");
                datos.setearParametro("@TipoTarjeta", metodoPago.TipoTarjeta);
                datos.setearParametro("@NroTarjeta", metodoPago.NroTarjeta);
                datos.setearParametro("@Vencimiento", metodoPago.Vencimiento);
                datos.setearParametro("@Cod", metodoPago.Cod);
                datos.setearParametro("@IDMetodoPago", metodoPago.IdMetodoPago);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el método de pago: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void RegistrarPago(Pago pago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Pago (IDUsuario, IDMetodoPago, FechaPago, Monto, Estado) VALUES (@IDUsuario, @IDMetodoPago, @FechaPago, @Monto, @Estado)");
                datos.setearParametro("@IDUsuario", pago.Usuario.IdUsuario);
                datos.setearParametro("@IDMetodoPago", pago.MetodoPago.IdMetodoPago);
                datos.setearParametro("@FechaPago", pago.FechaPago ?? DateTime.Now);
                datos.setearParametro("@Monto", pago.Importe);
                datos.setearParametro("@Estado", pago.Estado); 

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el pago: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Pago ObtenerPagoPorID(int idPago)
        {
            Pago pago = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDPago, IDUsuario, IDMetodoPago, FechaPago, Monto, Estado FROM Pago WHERE IDPago = @IDPago");
                datos.setearParametro("@IDPago", idPago);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pago = new Pago
                    {
                        IdPago = (int)datos.Lector["IDPago"],
                        Usuario = new Usuario { IdUsuario = (int)datos.Lector["IDUsuario"] },
                        MetodoPago = new MetodoPago { IdMetodoPago = (int)datos.Lector["IDMetodoPago"] },
                        FechaPago = datos.Lector["FechaPago"] as DateTime?,
                        Importe = (decimal)datos.Lector["Monto"],
                        Estado = (bool)datos.Lector["Estado"]
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pago: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return pago;
        }

        public void ActualizarEstadoPago(int idPago, bool estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Pago SET Estado = @Estado WHERE IDPago = @IDPago");
                datos.setearParametro("@Estado", estado);
                datos.setearParametro("@IDPago", idPago);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado del pago: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}