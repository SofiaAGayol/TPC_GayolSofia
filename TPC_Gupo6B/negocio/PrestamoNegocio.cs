using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class PrestamoNegocio
    {
        public List<Prestamo> ListarPrestamos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Prestamo> listaPrestamos = new List<Prestamo>();
            LibroNegocio libroNegocio = new LibroNegocio();

            try
            {
                datos.setearConsulta(
                    "SELECT p.IDPrestamo, p.IDUsuario, p.FechaInicio, p.FechaFin, p.Devuelto, p.IDMetodoEnvio, p.IDMetodoRetiro, p.CostoEnvio, p.Estado, p.IdDireccion, " +
                    "me.Descripcion AS MetodoEnvioDescripcion, mr.Descripcion AS MetodoRetiroDescripcion, " +
                    "d.IDDireccion, d.Calle, d.Altura, d.CodigoPostal, d.Aclaracion " +
                    "FROM Prestamo p " +
                    "JOIN MetodosDeEnvio me ON p.IDMetodoEnvio = me.IDMetodoEnvio " +
                    "JOIN MetodosDeRetiro mr ON p.IDMetodoRetiro = mr.IDMetodoRetiro " +
                    "LEFT JOIN Direccion d ON p.IdDireccion = d.IDDireccion");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prestamo prestamo = new Prestamo
                    {
                        IDPrestamo = (int)datos.Lector["IDPrestamo"],
                        Usuario = new Usuario { IdUsuario = (int)datos.Lector["IDUsuario"] },
                        FechaInicio = (DateTime)datos.Lector["FechaInicio"],
                        FechaFin = (DateTime)datos.Lector["FechaFin"],
                        Devuelto = (bool)datos.Lector["Devuelto"],
                        Estado = datos.Lector["Estado"].ToString(),
                        CostoEnvio = (decimal)datos.Lector["CostoEnvio"],
                        Libros = libroNegocio.ListarLibrosPorPrestamo((int)datos.Lector["IDPrestamo"]),
                        MetodoEnvio = new MetodoDeEnvio
                        {
                            IdMetodoEnvio = (int)datos.Lector["IDMetodoEnvio"],
                            Descripcion = datos.Lector["MetodoEnvioDescripcion"].ToString()
                        },
                        MetodoRetiro = new MetodoDeRetiro
                        {
                            IdMetodoRetiro = (int)datos.Lector["IDMetodoRetiro"],
                            Descripcion = datos.Lector["MetodoRetiroDescripcion"].ToString()
                        },
                        Direccion = new Direccion
                        {
                            IdDireccion = datos.Lector["IDDireccion"] != DBNull.Value ? (int)datos.Lector["IDDireccion"] : 0,
                            Calle = datos.Lector["Calle"]?.ToString(),
                            Altura = datos.Lector["Altura"] != DBNull.Value ? (int)datos.Lector["Altura"] : 0,
                            CodigoPostal = datos.Lector["CodigoPostal"]?.ToString(),
                            Aclaracion = datos.Lector["Aclaracion"]?.ToString()
                        }
                    };

                    listaPrestamos.Add(prestamo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaPrestamos;
        }
        public List<Prestamo> ListarPrestamosPorUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Prestamo> listaPrestamos = new List<Prestamo>();
            LibroNegocio libroNegocio = new LibroNegocio();

            try
            {
                datos.setearConsulta(
                    "SELECT p.IDPrestamo, p.IDUsuario, p.FechaInicio, p.FechaFin, p.Devuelto, p.IDMetodoEnvio, p.IDMetodoRetiro, p.CostoEnvio, p.Estado, p.IdDireccion, " +
                    "me.Descripcion AS MetodoEnvioDescripcion, mr.Descripcion AS MetodoRetiroDescripcion, " +
                    "d.IDDireccion, d.Calle, d.Altura, d.CodigoPostal, d.Aclaracion " +
                    "FROM Prestamo p " +
                    "JOIN MetodosDeEnvio me ON p.IDMetodoEnvio = me.IDMetodoEnvio " +
                    "JOIN MetodosDeRetiro mr ON p.IDMetodoRetiro = mr.IDMetodoRetiro " +
                    "LEFT JOIN Direccion d ON p.IdDireccion = d.IDDireccion " +
                    "WHERE p.IDUsuario = @IDUsuario " +
                    "ORDER BY p.FechaInicio ASC;");

                datos.setearParametro("@IDUsuario", idUsuario);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prestamo prestamo = new Prestamo
                    {
                        IDPrestamo = (int)datos.Lector["IDPrestamo"],
                        Usuario = new Usuario { IdUsuario = (int)datos.Lector["IDUsuario"] },
                        FechaInicio = (DateTime)datos.Lector["FechaInicio"],
                        FechaFin = (DateTime)datos.Lector["FechaFin"],
                        Devuelto = (bool)datos.Lector["Devuelto"],
                        Estado = datos.Lector["Estado"].ToString(),
                        CostoEnvio = (decimal)datos.Lector["CostoEnvio"],
                        Libros = libroNegocio.ListarLibrosPorPrestamo((int)datos.Lector["IDPrestamo"]),
                        MetodoEnvio = new MetodoDeEnvio
                        {
                            IdMetodoEnvio = (int)datos.Lector["IDMetodoEnvio"],
                            Descripcion = datos.Lector["MetodoEnvioDescripcion"].ToString()
                        },
                        MetodoRetiro = new MetodoDeRetiro
                        {
                            IdMetodoRetiro = (int)datos.Lector["IDMetodoRetiro"],
                            Descripcion = datos.Lector["MetodoRetiroDescripcion"].ToString()
                        },
                        Direccion = new Direccion
                        {
                            IdDireccion = datos.Lector["IDDireccion"] != DBNull.Value ? (int)datos.Lector["IDDireccion"] : 0,
                            Calle = datos.Lector["Calle"]?.ToString(),
                            Altura = datos.Lector["Altura"] != DBNull.Value ? (int)datos.Lector["Altura"] : 0,
                            CodigoPostal = datos.Lector["CodigoPostal"]?.ToString(),
                            Aclaracion = datos.Lector["Aclaracion"]?.ToString()
                        }
                    };

                    listaPrestamos.Add(prestamo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaPrestamos;
        }
        public void GuardarPrestamo(Prestamo prestamo)
        {
            AccesoDatos datos = new AccesoDatos();
            int idPrestamo = 0;

            try
            {
                datos.setearConsulta("INSERT INTO Prestamo (IDUsuario, FechaInicio, FechaFin, Devuelto, IDMetodoEnvio, CostoEnvio, IDMetodoRetiro, Estado, IdDireccion) " +
                                     "OUTPUT INSERTED.IDPrestamo " +
                                     "VALUES (@IDUsuario, @FechaInicio, @FechaFin, @Devuelto, @IDMetodoEnvio, @CostoEnvio, @IDMetodoRetiro, @Estado, @IdDireccion)");

                datos.setearParametro("@IDUsuario", prestamo.Usuario.IdUsuario);
                datos.setearParametro("@FechaInicio", prestamo.FechaInicio);
                datos.setearParametro("@FechaFin", prestamo.FechaFin);
                datos.setearParametro("@Devuelto", prestamo.Devuelto);
                datos.setearParametro("@IDMetodoEnvio", prestamo.MetodoEnvio.IdMetodoEnvio);
                datos.setearParametro("@CostoEnvio", prestamo.CostoEnvio);
                datos.setearParametro("@IDMetodoRetiro", prestamo.MetodoRetiro.IdMetodoRetiro);
                datos.setearParametro("@Estado", prestamo.Estado);
                datos.setearParametro("@IdDireccion", prestamo.Direccion.IdDireccion);

                datos.ejecutarLectura();

                if (datos.Lector.Read() && datos.Lector["IDPrestamo"] != DBNull.Value)
                {
                    idPrestamo = Convert.ToInt32(datos.Lector["IDPrestamo"]);
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID del préstamo insertado.");
                }
                datos.cerrarConexion();

                foreach (var libro in prestamo.Libros)
                {
                    datos.setearConsulta("INSERT INTO PrestamoLibro (IDPrestamo, IDLibro) VALUES (@IDPrestamo, @IDLibro)");
                    datos.setearParametro("@IDPrestamo", idPrestamo);
                    datos.setearParametro("@IDLibro", libro.IdLibro);
                    datos.ejecutarAccion();
                    datos.cerrarConexion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el préstamo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ActualizarEstadoPrestamo(int idPrestamo, bool devuelto, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Prestamo SET Devuelto = @Devuelto, Estado = @Estado WHERE IDPrestamo = @IDPrestamo");

                datos.setearParametro("@Devuelto", devuelto);
                datos.setearParametro("@Estado", nuevoEstado);
                datos.setearParametro("@IDPrestamo", idPrestamo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado del préstamo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
