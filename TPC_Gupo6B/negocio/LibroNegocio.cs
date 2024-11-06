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
        
        //Libros prestados
        public int ContarLibrosEnPrestamo()
        {
            int cantidadEnPrestamo = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Libro WHERE Estado = 0;");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cantidadEnPrestamo = (int)datos.Lector[0];
                }
                return cantidadEnPrestamo;
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
        public List<Libro> ObtenerLibrosEnPrestamo()
        {
            List<Libro> librosEnPrestamo = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();
            AutorNegocio autorNegocio = new AutorNegocio(); 
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio(); 

            try
            {
                datos.setearConsulta("SELECT IdLibro, Titulo, AutorId, CategoriaId, AñoPublicacion, Ejemplares, Disponibles, Estado, Imagen FROM Libro WHERE Estado = 0;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        IdLibro = (int)datos.Lector["IdLibro"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Autor = autorNegocio.ObtenerAutorPorId((int)datos.Lector["AutorId"]), 
                        Categoria = categoriaNegocio.ObtenerCategoriaPorId((int)datos.Lector["CategoriaId"]),
                        FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"],
                        Ejemplares = (int)datos.Lector["Ejemplares"],
                        Disponibles = (int)datos.Lector["Disponibles"],
                        Estado = (bool)datos.Lector["Estado"], 
                        Imagen = datos.Lector["Imagen"].ToString()
                    };

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
                        Imagen= datos.Lector["ImagenURL"].ToString(),
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
                        Imagen= datos.Lector["ImagenURL"].ToString(),
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
                datos.setearConsulta("UPDATE Libros SET Estado = 1 WHERE IdLibro = @idLibro;");


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
                        resultado = !estado;  
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


        //ABM
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
