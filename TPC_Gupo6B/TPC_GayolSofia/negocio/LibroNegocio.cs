using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class LibroNegocio
    {
        
        public List<Libro> listar()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT l.IDLibro, l.Titulo, l.FechaPublicacion, l.Ejemplares, l.Disponibles, l.Estado, l.ImagenURL, " +
                                     "a.IDAutor, a.Nombre as NombreAutor, a.Apellido as ApellidoAutor, " +
                                     "c.IDCategoria, c.Descripcion as DescripcionCategoria " +
                                     "FROM Libro l " +
                                     "LEFT JOIN Autores a ON a.IDAutor = l.IDAutor " +
                                     "LEFT JOIN Categoria c ON c.IDCategoria = l.IDCategoria " +
                                     "ORDER BY l.IDLibro ASC;");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.IdLibro = (int)datos.Lector["IdLibro"];
                    libro.Titulo = (string)datos.Lector["Titulo"];
                    libro.AñoPublicacion = ((DateTime)datos.Lector["FechaPublicacion"]).Year;
                    libro.Ejemplares = (int)datos.Lector["Ejemplares"];
                    libro.Disponibles = (int)datos.Lector["Disponibles"];
                    libro.Estado = datos.Lector["Estado"] is DBNull ? false : (bool)datos.Lector["Estado"]; ;
                    libro.Imagen = (string)datos.Lector["ImagenURL"];

                    // Asignar autor
                    libro.Autor = new Autor();
                    libro.Autor.IdAutor = (int)datos.Lector["IDAutor"];
                    libro.Autor.Nombre = (string)datos.Lector["NombreAutor"];
                    libro.Autor.Apellido = (string)datos.Lector["ApellidoAutor"];

                    // Asignar categoría
                    libro.Categoria = new Categoria();
                    libro.Categoria.IdCategoria = (int)datos.Lector["IDCategoria"];
                    libro.Categoria.Descripcion = (string)datos.Lector["DescripcionCategoria"];

                    lista.Add(libro);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*
        public List<Libro> listarConSP()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                //datos.setearConsulta("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, m.Id as IdMarca, m.Descripcion as DescripcionM, c.Id as IdCategoria, c.Descripcion as DescripcionC, i.Id as IdImagen, i.ImagenUrl as Imagen FROM ARTICULOS a LEFT JOIN CATEGORIAS c ON c.Id = a.IdCategoria LEFT JOIN MARCAS m ON m.Id = a.IdMarca LEFT JOIN IMAGENES i ON i.IdArticulo = a.Id ");
                //datos.ejecutarLectura();

                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["Id"];
                    string codigoArticulo = (string)datos.Lector["Codigo"];

                    Libro articulo = lista.FirstOrDefault(a => a.Codigo == codigoArticulo);

                    if (articulo == null)
                    {
                        articulo = new Libro();
                        articulo.Id = (int)datos.Lector["Id"];
                        articulo.Codigo = (string)datos.Lector["Codigo"];
                        articulo.Nombre = (string)datos.Lector["Nombre"];
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];
                        articulo.Precio = (decimal)datos.Lector["Precio"];

                        //Marca
                        articulo.Marca = new Marca();
                        articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                        articulo.Marca.Descripcion = (string)datos.Lector["DescripcionM"];

                        //Categoria
                        articulo.Categoria = new Categoria();
                        articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        articulo.Categoria.Descripcion = (string)datos.Lector["DescripcionC"];

                        // Inicializar la lista de imágenes
                        articulo.Imagenes = new List<Imagen>();

                        // Agregar artículo a la lista
                        lista.Add(articulo);
                    }
                                        
                    if (!(datos.Lector["IdImagen"] is DBNull))
                    {
                        Imagen imagen = new Imagen();
                        imagen.Id = (int)datos.Lector["IdImagen"];
                        imagen.IdArticulo = (int)datos.Lector["Id"];
                        imagen.ImagenUrl = (string)datos.Lector["Imagen"];
                                                
                        articulo.Imagenes.Add(imagen);
                    }
                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Libro> filtrar(string campo, string criterio, string filtro)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, a.IdMarca, a.IdCategoria, " +
                             "m.Id as IdMarca, m.Descripcion as DescripcionM, " +
                             "c.Id as IdCategoria, c.Descripcion as DescripcionC, " +
                             "i.Id as IdIm, i.IdArticulo, i.ImagenUrl as Imagen " +
                             "FROM ARTICULOS a " +
                             "LEFT JOIN CATEGORIAS c ON c.Id = a.IdCategoria " +
                             "LEFT JOIN MARCAS m ON m.Id = a.IdMarca " +
                             "LEFT JOIN IMAGENES i ON i.IdArticulo = a.Id " +
                             "ORDER BY a.Id ASC;";

                if (campo == "Id")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.Id > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "a.Id < " + filtro;
                            break;
                        default:
                            consulta += "a.Id = " + filtro;
                            break;
                    }
                }
                else if (campo == "Codigo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "a.Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "a.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    consulta += "m.Descripcion = '" + filtro + "'";
                }
                else if (campo == "Categoria")
                {
                    consulta += "c.Descripcion = '" + filtro + "'";
                }
                else if (campo == "a.Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "a.Precio < " + filtro;
                            break;
                        default:
                            consulta += "a.Precio = " + filtro;
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "a.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["Id"];

                    Libro articulo = lista.FirstOrDefault(a => a.Id == idArticulo);

                    if (articulo == null)
                    {
                        articulo = new Libro();
                        articulo.Id = (int)datos.Lector["Id"];
                        articulo.Codigo = (string)datos.Lector["Codigo"];
                        articulo.Nombre = (string)datos.Lector["Nombre"];
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];
                        articulo.Precio = (decimal)datos.Lector["Precio"];

                        //Marca
                        articulo.Marca = new Marca();
                        articulo.Marca.Id = (int)datos.Lector["IdM"];
                        articulo.Marca.Descripcion = (string)datos.Lector["DescripcionM"];

                        //Categoria
                        articulo.Categoria = new Categoria();
                        articulo.Categoria.Id = (int)datos.Lector["IdC"];
                        articulo.Categoria.Descripcion = (string)datos.Lector["DescripcionC"];

                        //Imagenes
                        if (!(datos.Lector["IdIm"] is DBNull))
                        {
                            articulo.Imagenes = new List<Imagen>();
                            Imagen imagen = new Imagen();

                            imagen.Id = (int)datos.Lector["IdIm"];
                            imagen.IdArticulo = (int)datos.Lector["IdArticulo"];
                            imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                            articulo.Imagenes.Add(imagen);
                        }

                        lista.Add(articulo);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Libro buscarLibro(int articuloID)
        {
            AccesoDatos datos = new AccesoDatos();


            datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.Precio Precio, M.Descripcion AS NombreMarca, M.Id AS MarcaId, I.ImagenUrl AS ImagenUrl, I.Id AS imgId, C.Descripcion AS categoriaDescripcion, C.Id AS CatID FROM ARTICULOS A, MARCAS M, IMAGENES I, CATEGORIAS C where A.Id = @ArtId and A.IdMarca = M.Id and C.Id = A.Id;");

            datos.setearParametro("@ArtId", articuloID);

            datos.ejecutarLectura();

            Libro articulo = new Libro();

            if (datos.Lector.Read())
            {
                articulo.Id = (int)datos.Lector["Id"];
                articulo.Codigo = (string)datos.Lector["Codigo"];
                articulo.Nombre = (string)datos.Lector["Nombre"];
                articulo.Precio = (decimal)datos.Lector["Precio"];
                articulo.Descripcion = (string)datos.Lector["Descripcion"];

                //Agrego la categoria
                articulo.Categoria = new Categoria();
                articulo.Categoria.Id = (int)datos.Lector["CatID"];
                articulo.Categoria.Descripcion = (string)datos.Lector["categoriaDescripcion"];

                //Agrego la marca
                articulo.Marca = new Marca();
                articulo.Marca.Id = (int)datos.Lector["MarcaId"];
                articulo.Marca.Descripcion = (string)datos.Lector["NombreMarca"];

                //Agrego la imagen
                articulo.Imagenes = new List<Imagen>();
                Imagen img = new Imagen();

                img.Id = (int)datos.Lector["imgId"];
                img.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                articulo.Imagenes.Add(img);
            }

            return articulo;
        }

        public void agregar(Libro nuevo)
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

        public void modificar(Libro articulo)
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
        }

        */

    }

}
