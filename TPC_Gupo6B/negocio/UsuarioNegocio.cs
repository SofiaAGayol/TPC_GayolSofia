using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;
using TPC_GayolSofia.dominio;
using static System.Net.WebRequestMethods;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]),
                        NombreUsuario = datos.Lector["Usuario"].ToString(),
                        Clave = datos.Lector["Clave"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        DNI = datos.Lector["DNI"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        IDRol = Convert.ToInt32(datos.Lector["IDRol"])
                    };

                    usuarios.Add(usuario);
                }

                return usuarios; // Devuelve la lista de usuarios
            }
            catch (Exception ex)
            {
                throw ex; // Considera registrar el error o manejarlo de manera más específica
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int VerificarEmailYContrasena(string usuario, string contrasenia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT CASE " +
                                     "WHEN Usuario = @Usuario AND Clave = @Contrasenia THEN 2 " +
                                     "WHEN Usuario = @Usuario THEN 1 " +
                                     "ELSE 0 " +
                                     "END " +
                                     "FROM Usuarios " +
                                     "WHERE Usuario = @Usuario;");

                datos.setearParametro("@Usuario", usuario);
                datos.setearParametro("@Contrasenia", contrasenia);

                object resultObj = datos.ejecutarAccion();

                if (resultObj != null)
                {
                    return Convert.ToInt32(resultObj);
                }
                else
                {
                    return 0;
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
        }

        public int ObtenerIdUsuario(string nombreUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario FROM Usuarios WHERE Usuario = @NombreUsuario");
                datos.setearParametro("@NombreUsuario", nombreUsuario);

                object resultObj = datos.ejecutarAccion();

                if (resultObj != null)
                {
                    return Convert.ToInt32(resultObj);
                }
                else
                {
                    return -1; // Usuario no encontrado
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
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol FROM Usuarios WHERE IDUsuario = @IDUsuario");
                datos.setearParametro("@IDUsuario", idUsuario);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]),
                        NombreUsuario = datos.Lector["Usuario"].ToString(),
                        Clave = datos.Lector["Clave"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        DNI = datos.Lector["DNI"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        IDRol = Convert.ToInt32(datos.Lector["IDRol"])
                    };

                    return usuario;
                }
                else
                {
                    return null; // Usuario no encontrado
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
        }



        public bool ExisteUsuario(string usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Usuarios WHERE Usuario = @Usuario");
                datos.setearParametro("@Usuario", usuario);
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
        public bool ExisteDNI(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Usuarios WHERE DNI = @DNI");
                datos.setearParametro("@DNI", dni);
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
        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Usuarios WHERE Email = @Email");
                datos.setearParametro("@Email", email);
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


        public bool Agregar(string usuario, string clave, string nombre, string apellido, string dni, string email, string telefono, int idRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol, estado) VALUES (@Usuario, @Clave, @Nombre, @Apellido, @DNI, @Email, @Telefono, @IDRol, 1);");

                datos.setearParametro("@Usuario", usuario);
                datos.setearParametro("@Clave", clave);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@DNI", dni);
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Telefono", telefono);
                datos.setearParametro("@IDRol", idRol);

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


        public bool Modificar(int idUsuario, string usuario, string clave, string nombre, string apellido, string dni, string email, string telefono, int idRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Usuario = @Usuario, Clave = @Clave, Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, Email = @Email, Telefono = @Telefono, IDRol = @IDRol WHERE IdUsuario = @idUsuario;");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@Usuario", usuario);
                datos.setearParametro("@Clave", clave);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@DNI", dni);
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Telefono", telefono);
                datos.setearParametro("@IDRol", idRol);

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

        public bool BajaLogica(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET estado = 0 WHERE IdUsuario = @idUsuario;");

                datos.setearParametro("@idUsuario", idUsuario);

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

        public bool estaBaja(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;

            try
            {
                datos.setearConsulta("SELECT estado FROM Usuarios WHERE IdUsuario = @idUsuario;");
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    if (!Convert.IsDBNull(datos.Lector["estado"]))
                    {
                        // Convertir el valor a bool
                        bool estado = (bool)datos.Lector["estado"];                      
                        resultado = estado; // Devuelve true si estado es 1, false si estado es 0
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

            return resultado; // Retorna true si está activo y false si está inactivo
        }
        public bool RestablecerLogica(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET estado = 1 WHERE IdUsuario = @idUsuario;");

                datos.setearParametro("@idUsuario", idUsuario);

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

