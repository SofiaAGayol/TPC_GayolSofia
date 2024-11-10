using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PrestamoNegocio
    {
        public List<Prestamo> ListarPrestamos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Prestamo> listaPrestamos = new List<Prestamo>();
            /*
            try
            {
                datos.setearConsulta(
                    "SELECT p.IdPrestamo, p.IdCliente, p.FechaPrestamo, p.FechaDevolucion, p.Estado, " +
                    "l.IdLibro, l.Titulo, a.IdAutor, a.Nombre AS NombreAutor " +
                    "FROM Prestamos p " +
                    "JOIN Libros l ON p.IdLibro = l.IdLibro " +
                    "JOIN Autores a ON l.IdAutor = a.IdAutor");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prestamo prestamo = new Prestamo
                    {
                        IDPrestamo = (int)datos.Lector["IdPrestamo"],
                        IDUsuario = (int)datos.Lector["IdUsuario"],
                        FechaInicio = (DateTime)datos.Lector["FechaPrestamo"],
                        FechaFin = (DateTime)datos.Lector["FechaDevolucion"],
                        Estado = (int)datos.Lector["Estado"],

                        Libro = new Libro
                        {
                            IdLibro = (int)datos.Lector["IdLibro"],
                            Titulo = datos.Lector["Titulo"].ToString(),
                            Autor = new Autor
                            {
                                IdAutor = (int)datos.Lector["IdAutor"],
                                Nombre = datos.Lector["NombreAutor"].ToString()
                            }
                        }
                    };

                    listaPrestamos.Add(prestamo);
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
            */
            return listaPrestamos;
        }

        public void GuardarPrestamo(Prestamo prestamo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Prestamo (IDLibro, IDUsuario, FechaInicio, FechaFin, Devuelto, MetodoEnvio, CostoEnvio, MetodoRetiro, Estado) " +
                                     "VALUES (@IDLibro, @IDUsuario, @FechaInicio, @FechaFin, @Devuelto, @MetodoEnvio, @CostoEnvio, @MetodoRetiro, @Estado)");

                datos.setearParametro("@IDLibro", prestamo.Libro.IdLibro);
                datos.setearParametro("@IDUsuario", prestamo.Usuario.IdUsuario);
                datos.setearParametro("@FechaInicio", prestamo.FechaInicio);
                datos.setearParametro("@FechaFin", prestamo.FechaFin);
                datos.setearParametro("@Devuelto", prestamo.Devuelto);
                datos.setearParametro("@MetodoEnvio", prestamo.MetodoEnvio.IdMetodoEnvio); 
                datos.setearParametro("@CostoEnvio", prestamo.CostoEnvio);
                datos.setearParametro("@MetodoRetiro", prestamo.MetodoRetiro.IdMetodoRetiro);
                datos.setearParametro("@Estado", prestamo.Estado);

                datos.ejecutarAccion();
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

    }
}
