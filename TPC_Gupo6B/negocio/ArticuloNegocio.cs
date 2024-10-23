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
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, a.IdMarca, a.IdCategoria, " +
                             "m.Id as IdMarca, m.Descripcion as DescripcionM, " +
                             "c.Id as IdCategoria, c.Descripcion as DescripcionC, " +
                             "i.Id as IdIm, i.IdArticulo, i.ImagenUrl as Imagen " +
                             "FROM ARTICULOS a " +
                             "LEFT JOIN CATEGORIAS c ON c.Id = a.IdCategoria " +
                             "LEFT JOIN MARCAS m ON m.Id = a.IdMarca " +
                             "LEFT JOIN IMAGENES i ON i.IdArticulo = a.Id " +
                             "ORDER BY a.Id ASC;");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["Id"];
                    string codigoArticulo = (string)datos.Lector["Codigo"];

                    Articulo articulo = lista.FirstOrDefault(a => a.Codigo == codigoArticulo);

                    if (articulo == null)
                    {
                        articulo = new Articulo();
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

                        //Imagenes
                        if (!(datos.Lector["IdIm"] is DBNull))
                        {
                            articulo.Imagenes = new List<Imagen>();
                            Imagen imagen = new Imagen();

                            imagen.Id = (int)datos.Lector["IdIm"];
                            imagen.IdArticulo = (int)datos.Lector["IdArticulo"];
                            imagen.ImagenUrl = (string)datos.Lector["Imagen"];

                            articulo.Imagenes.Add(imagen);
                        }

                        // Inicializamos la lista de imágenes del artículo
                        articulo.Imagenes = new List<Imagen>();

                        // Añadimos el artículo a la lista
                        lista.Add(articulo);
                    }

                    if (!(datos.Lector["IdIm"] is DBNull))
                    {
                        Imagen imagen = new Imagen();
                        imagen.Id = (int)datos.Lector["IdIm"];
                        imagen.IdArticulo = (int)datos.Lector["IdArticulo"];
                        imagen.ImagenUrl = (string)datos.Lector["Imagen"];

                        // Añadir la imagen a la lista del artículo
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

        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
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

                    Articulo articulo = lista.FirstOrDefault(a => a.Codigo == codigoArticulo);

                    if (articulo == null)
                    {
                        articulo = new Articulo();
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

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
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

                    Articulo articulo = lista.FirstOrDefault(a => a.Id == idArticulo);

                    if (articulo == null)
                    {
                        articulo = new Articulo();
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

        public Articulo buscarArticulo(int articuloID)
        {
            AccesoDatos datos = new AccesoDatos();


            datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.Precio Precio, M.Descripcion AS NombreMarca, M.Id AS MarcaId, I.ImagenUrl AS ImagenUrl, I.Id AS imgId, C.Descripcion AS categoriaDescripcion, C.Id AS CatID FROM ARTICULOS A, MARCAS M, IMAGENES I, CATEGORIAS C where A.Id = @ArtId and A.IdMarca = M.Id and C.Id = A.Id;");

            datos.setearParametro("@ArtId", articuloID);

            datos.ejecutarLectura();

            Articulo articulo = new Articulo();

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

        public void agregar(Articulo nuevo)
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

        public void modificar(Articulo articulo)
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



    }

}
