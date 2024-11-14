using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CarritoNegocio
    {
        public void AgregarLibroAlCarrito(int idUsuario, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (!ExisteEnCarrito(idUsuario, idLibro))
                {
                    datos.setearConsulta("INSERT INTO Carrito (IDUsuario, IDLibro) VALUES (@IDUsuario, @IDLibro)");
                    datos.setearParametro("@IDUsuario", idUsuario);
                    datos.setearParametro("@IDLibro", idLibro);
                    datos.ejecutarAccion();

                }
                else
                {
                    throw new Exception("El libro ya está en el carrito.");
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool ExisteEnCarrito(int idUsuario, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Carrito WHERE IDUsuario = @IDUsuario AND IDLibro = @IDLibro");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.setearParametro("@IDLibro", idLibro);
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
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Libro> CargarCarrito(int idUsuario)
        {
            List<Libro> listaLibrosCarrito = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT C.IDCarrito, L.IDLibro, L.Titulo, L.ImagenURL, A.Nombre AS NombreAutor, A.Apellido AS ApellidoAutor " +
                                     "FROM Carrito C " +
                                     "INNER JOIN Libro L ON L.IDLibro = C.IDLibro " +
                                     "INNER JOIN Autores A ON A.IDAutor = L.IDAutor " +
                                     "WHERE C.IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Imagen = datos.Lector["ImagenURL"].ToString(),
                        Autor = new Autor
                        {
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        }
                    };
                    listaLibrosCarrito.Add(libro);
                }

                return listaLibrosCarrito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Libro> ListarLibrosEnCarrito(int idUsuario)
        {
            List<Libro> listaLibrosCarrito = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT C.IDCarrito, L.IDLibro, L.Titulo, L.ImagenURL, A.Nombre AS NombreAutor, A.Apellido AS ApellidoAutor " +
                                     "FROM Carrito C " +
                                     "INNER JOIN Libro L ON L.IDLibro = C.IDLibro " +
                                     "INNER JOIN Autores A ON A.IDAutor = L.IDAutor " +
                                     "WHERE C.IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Imagen = datos.Lector["ImagenURL"].ToString(),
                        Autor = new Autor
                        {
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        }
                    };
                    listaLibrosCarrito.Add(libro);
                }

                return listaLibrosCarrito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarLibroDelCarrito(int idUsuario, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Carrito WHERE IDUsuario = @IDUsuario AND IDLibro = @IDLibro");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.setearParametro("@IDLibro", idLibro);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int ContarLibrosEnCarrito(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Carrito WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0];
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void VaciarCarrito(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Carrito WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al vaciar el carrito: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }

}
