using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class MembresiasNegocio
    {

        public bool Agregar(int idUsuario, int idTipoMembresia, int mesesDuracion, int idPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                DateTime fechaInicio = DateTime.Now; // Fecha Inicio, es fecha actual
                DateTime fechaFin = fechaInicio.AddMonths(mesesDuracion); // Fecha fin calculada

                datos.setearConsulta("INSERT INTO Membresias (IDUsuario, IDTipoMembresia, FechaInicio, FechaFin, IDPago, Estado) " +
                                     "VALUES (@IDUsuario, @IDTipoMembresia, @FechaInicio, @FechaFin, @IDPago, @Estado);");

                datos.setearParametro("@IDUsuario", idUsuario);
                datos.setearParametro("@IDTipoMembresia", idTipoMembresia);
                datos.setearParametro("@FechaInicio", fechaInicio);
                datos.setearParametro("@FechaFin", fechaFin);
                datos.setearParametro("@IDPago", idPago); 
                datos.setearParametro("@Estado", 1); 

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la membresía: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return true;
        }
        public int ObtenerDuracionMeses(int idMembresia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT DuracionMeses FROM TipoMembresia WHERE IDTipoMembresia = @IDMembresia");
                datos.setearParametro("@IDMembresia", idMembresia);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return datos.Lector.GetInt32(0); // Devuelve el valor de DuracionMeses
                }
                else
                {
                    throw new Exception("No se encontró la membresía con el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la duración de la membresía: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
