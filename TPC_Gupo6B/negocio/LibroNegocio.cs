using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;
using static System.Net.Mime.MediaTypeNames;
using TPC_GayolSofia.dominio;
using static System.Net.WebRequestMethods;

namespace negocio
{
    public class LibroNegocio
    {

        public List<Libro> Listar()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, " +
                                     "a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, " +
                                     "c.IDCategoria, c.Descripcion AS DescripcionCategoria " +
                                     "FROM Libro l " +
                                     "LEFT JOIN Autores a ON a.IDAutor = l.IDAutor " +
                                     "LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria " +
                                     "ORDER BY l.IDLibro ASC;");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"],
                        Imagen = datos.Lector["ImagenURL"] != DBNull.Value ? datos.Lector["ImagenURL"].ToString() : null,

                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        },

                        Categoria = new Categoria
                        {
                            IdCategoria = (int)datos.Lector["IDCategoria"],
                            Descripcion = datos.Lector["DescripcionCategoria"].ToString()
                        }
                    };

                    lista.Add(libro);
                }

                return lista;
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

        //Libros disponibles
        public List<Libro> ListarDisponibles()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, " +
                     "a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, " +
                     "c.IDCategoria, c.Descripcion AS DescripcionCategoria " +
                     "FROM Libro l " +
                     "LEFT JOIN Autores a ON a.IDAutor = l.IDAutor " +
                     "LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria " +
                     "WHERE l.Disponibles > 0 AND l.Estado = 1 " +
                     "ORDER BY l.IDLibro ASC;");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"],
                        Imagen = datos.Lector["ImagenURL"] != DBNull.Value ? datos.Lector["ImagenURL"].ToString() : null,

                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        },

                        Categoria = new Categoria
                        {
                            IdCategoria = (int)datos.Lector["IDCategoria"],
                            Descripcion = datos.Lector["DescripcionCategoria"].ToString()
                        }
                    };

                    lista.Add(libro);
                }

                return lista;
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
        public int ContarLibrosDisponibles()
        {
            int cantidadDisponibles = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Libro WHERE Disponibles > 0 AND Estado = 1;");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cantidadDisponibles = (int)datos.Lector[0];
                }
                return cantidadDisponibles;
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
        public int ContarLibrosStock()
        {
            int cantidadDisponibles = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT SUM(Disponibles) FROM Libro;");

                datos.ejecutarLectura();

                if (datos.Lector.Read() && !datos.Lector.IsDBNull(0))
                {
                    cantidadDisponibles = datos.Lector.GetInt32(0);
                }
                return cantidadDisponibles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al sumar los ejemplares disponibles: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Libros prestados
        public int ContarLibrosEnPrestamo()
        {
            int cantidadEnPrestamo = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT COUNT(*) " +
                    "FROM PrestamoLibro pl " +
                    "JOIN Prestamo p ON pl.IDPrestamo = p.IDPrestamo " +
                    "WHERE p.Devuelto = 0;");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cantidadEnPrestamo = (int)datos.Lector[0];
                }

                return cantidadEnPrestamo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar los libros en préstamo: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Libro> ObtenerLibrosEnPrestamo()
        {
            List<Libro> librosEnPrestamo = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, " +
                                     "p.IDPrestamo, u.IDUsuario, u.Nombre AS NombreUsuario, u.Apellido AS ApellidoUsuario " +
                                     "FROM PrestamoLibro pl " +
                                     "JOIN Libro l ON pl.IDLibro = l.IDLibro " +
                                     "JOIN Autores a ON l.IDAutor = a.IDAutor " +
                                     "JOIN Prestamo p ON pl.IDPrestamo = p.IDPrestamo " +
                                     "JOIN Usuarios u ON p.IDUsuario = u.IDUsuario " +
                                     "WHERE p.Devuelto = 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        }
                    };

                    Usuario usuario = new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IDUsuario"],
                        Nombre = datos.Lector["NombreUsuario"].ToString(),
                        Apellido = datos.Lector["ApellidoUsuario"].ToString()
                    };

                    Prestamo prestamo = new Prestamo
                    {
                        IDPrestamo = (int)datos.Lector["IDPrestamo"],
                        Usuario = usuario,
                        Libros = new List<Libro> { libro }
                    };

                    libro.Prestamo = prestamo;

                    librosEnPrestamo.Add(libro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener libros en préstamo: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return librosEnPrestamo;
        }
        public List<Libro> ListarLibrosPorPrestamo(int idPrestamo)
        {
            List<Libro> listaLibros = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor " +
                                    "FROM PrestamoLibro pl " +
                                    "JOIN Libro l ON pl.IDLibro = l.IDLibro " +
                                    "JOIN Autores a ON l.IDAutor = a.IDAutor " +
                                    "WHERE pl.IDPrestamo = @IDPrestamo");

                datos.setearParametro("@IDPrestamo", idPrestamo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        }
                    };
                    listaLibros.Add(libro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los libros del préstamo: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaLibros;
        }
        public List<Libro> ListarLibrosEnPrestamoPorUsuario(int idUsuario)
        {
            List<Libro> listaLibros = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT l.IDLibro, l.Titulo, a.IDAutor, l.ImagenURL, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor " +
                    "FROM Prestamo p " +
                    "JOIN PrestamoLibro pl ON p.IDPrestamo = pl.IDPrestamo " +
                    "JOIN Libro l ON pl.IDLibro = l.IDLibro " +
                    "JOIN Autores a ON l.IDAutor = a.IDAutor " +
                    "WHERE p.IDUsuario = @IDUsuario AND p.Devuelto = 0");

                datos.setearParametro("@IDUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Imagen = datos.Lector["ImagenURL"] != DBNull.Value ? datos.Lector["ImagenURL"].ToString() : null,
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        }
                    };
                    listaLibros.Add(libro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los libros en préstamo: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaLibros;
        }

        //Filtros y busqueda
        public Libro LibroPorID(int IdLibro)
        {
            Libro libro = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, c.IDCategoria, c.Descripcion AS DescripcionCategoria FROM Libro l LEFT JOIN Autores a ON a.IDAutor = l.IDAutor LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria WHERE l.IDLibro = @IdLibro;");
                datos.setearParametro("@IdLibro", IdLibro);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"],
                        Imagen = datos.Lector["ImagenURL"].ToString(),
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        },

                        Categoria = new Categoria
                        {
                            IdCategoria = (int)datos.Lector["IDCategoria"],
                            Descripcion = datos.Lector["DescripcionCategoria"].ToString()
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener libro: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return libro;
        }
        public List<Libro> LibrosPorAutor(int IdAutor)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, c.IDCategoria, c.Descripcion AS DescripcionCategoria FROM Libro l LEFT JOIN Autores a ON a.IDAutor = l.IDAutor  LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria WHERE a.IDAutor = @IdAutor;");

                datos.setearParametro("@IdAutor", IdAutor);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"],
                        Imagen = datos.Lector["ImagenURL"].ToString(),
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        },

                        Categoria = new Categoria
                        {
                            IdCategoria = (int)datos.Lector["IDCategoria"],
                            Descripcion = datos.Lector["DescripcionCategoria"].ToString()
                        }
                    };
                    lista.Add(libro);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }
        public List<Libro> LibrosPorCategoria(int IdCategoria)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, a.IDAutor, a.Nombre AS NombreAutor, a.Apellido AS ApellidoAutor, c.IDCategoria, c.Descripcion AS DescripcionCategoria FROM Libro l LEFT JOIN Autores a ON a.IDAutor = l.IDAutor  LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria WHERE c.IDCategoria = @IdCategoria;");

                datos.setearParametro("@IdCategoria", IdCategoria);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IDLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"],
                        Imagen = datos.Lector["ImagenURL"].ToString(),
                        Autor = new Autor
                        {
                            IdAutor = (int)datos.Lector["IDAutor"],
                            Nombre = datos.Lector["NombreAutor"].ToString(),
                            Apellido = datos.Lector["ApellidoAutor"].ToString()
                        },

                        Categoria = new Categoria
                        {
                            IdCategoria = (int)datos.Lector["IDCategoria"],
                            Descripcion = datos.Lector["DescripcionCategoria"].ToString()
                        }
                    };
                    lista.Add(libro);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }
        public bool ExisteTitulo(string titulo)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Libro WHERE Titulo = @titulo");
                datos.setearParametro("@Titulo", titulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0) ? true : false;
        }
        public bool ExisteTituloNuevo(string titulo, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Libro WHERE Titulo = @Titulo AND IdLibro <> @IdLibro;");
                datos.setearParametro("@Titulo", titulo);
                datos.setearParametro("@IdLibro", idLibro);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0);
        }

        //ABM
        public bool Agregar(string titulo, DateTime fechaPublicacion, int ejemplares, bool estado, string imagen, int idAutor, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Libro (Titulo, FechaPublicacion, Ejemplares, Disponibles, Estado, ImagenURL, IdAutor, IdCategoria) " +
                                     "VALUES (@Titulo, @FechaPublicacion, @Ejemplares, @Disponibles, @Estado, @Imagen, @IdAutor, @IdCategoria);");

                datos.setearParametro("@Titulo", titulo);
                datos.setearParametro("@FechaPublicacion", fechaPublicacion);
                datos.setearParametro("@Ejemplares", ejemplares);
                datos.setearParametro("@Disponibles", ejemplares);
                datos.setearParametro("@Estado", estado);
                datos.setearParametro("@Imagen", imagen);
                datos.setearParametro("@IdAutor", idAutor);
                datos.setearParametro("@IdCategoria", idCategoria);

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

            return true;
        }
        public bool BajaLogica(int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Libro SET Estado = 0 WHERE IdLibro = @idLibro;");

                datos.setearParametro("@idLibro", idLibro);

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

            return true;
        }
        public bool RestablecerLogica(int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Libro SET Estado = 1 WHERE IdLibro = @idLibro;");


                datos.setearParametro("@idLibro", idLibro);

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

            return true;
        }
        public bool EstaBaja(int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;

            try
            {
                datos.setearConsulta("SELECT Estado FROM Libro WHERE IdLibro = @idLibro;");
                datos.setearParametro("@idLibro", idLibro);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    if (!Convert.IsDBNull(datos.Lector["Estado"]))
                    {
                        bool estado = (bool)datos.Lector["Estado"];
                        resultado = estado;
                    }
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

            return resultado;
        }

        public bool Modificar(int idLibro, string titulo, DateTime fechaPublicacion, int ejemplares, string imagen, int idAutor, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Libro SET Titulo = @Titulo, FechaPublicacion = @FechaPublicacion, Ejemplares = @Ejemplares, Disponibles = @Ejemplares, ImagenURL = @Imagen, IdAutor = @IdAutor, IdCategoria = @IdCategoria WHERE IdLibro = @IdLibro;");

                datos.setearParametro("@IdLibro", idLibro);
                datos.setearParametro("@Titulo", titulo);
                datos.setearParametro("@FechaPublicacion", fechaPublicacion);
                datos.setearParametro("@Ejemplares", ejemplares);
                datos.setearParametro("@Imagen", imagen);
                datos.setearParametro("@IdAutor", idAutor);
                datos.setearParametro("@IdCategoria", idCategoria);

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

            return true;
        }
        public bool ExisteLibro(string titulo)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Libro WHERE Titulo = @Titulo");
                datos.setearParametro("@Titulo", titulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0);
        }

        public bool ExisteLibroNuevo(string titulo, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Libro WHERE Titulo = @Titulo AND IdLibro <> @IdLibro;");
                datos.setearParametro("@Titulo", titulo);
                datos.setearParametro("@IdLibro", idLibro);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0);
        }

        public void RestarDisponibilidad(int idLibro, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Libro SET Disponibles = Disponibles - @Cantidad WHERE IDLibro = @IDLibro");

                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@IDLibro", idLibro);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la disponibilidad del libro: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void IncrementarStockLibro(int idPrestamo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDLibro FROM PrestamoLibro WHERE IDPrestamo = @IDPrestamo");
                datos.setearParametro("@IDPrestamo", idPrestamo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idLibro = (int)datos.Lector["IDLibro"];

                    AccesoDatos datosUpdate = new AccesoDatos();
                    datosUpdate.setearConsulta("UPDATE Libro SET Disponibles = Disponibles + 1 WHERE IDLibro = @IDLibro");
                    datosUpdate.setearParametro("@IDLibro", idLibro);
                    datosUpdate.ejecutarAccion();
                    datosUpdate.cerrarConexion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al incrementar el stock del libro: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        /*
        public void Agregar(Libro nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            //int idGenerado = 0;

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                             "VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio);" +
                             "SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

                int idArticulo = Convert.ToInt32(datos.ejecutarAccion()); // Ejecuta dentro de la transacción

                // Ahora, si hay imágenes, las insertamos
                if (nuevo.Imagenes != null && nuevo.Imagenes.Count > 0)
                {
                    foreach (Imagen imagen in nuevo.Imagenes)
                    {
                        datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl);");
                        datos.setearParametro("@IdArticulo", idArticulo); // Usamos el ID del artículo recién insertado
                        datos.setearParametro("@ImagenUrl", imagen.ImagenUrl);
                    }
                }
                datos.ejecutarConsulta();

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

        public void Modificar(Libro articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria  where Id = @Id");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Id", articulo.Id);

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
        }*/

    }

}
