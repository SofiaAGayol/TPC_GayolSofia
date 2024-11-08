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
                        IdPrestamo = (int)datos.Lector["IdPrestamo"],
                        IdCliente = (int)datos.Lector["IdCliente"],
                        FechaPrestamo = datos.Lector["FechaPrestamo"] as DateTime?,
                        FechaDevolucion = datos.Lector["FechaDevolucion"] as DateTime?,
                        estado = (int)datos.Lector["Estado"],

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

            return listaPrestamos;
        }


    }
}
