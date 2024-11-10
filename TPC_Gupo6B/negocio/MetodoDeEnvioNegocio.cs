using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MetodoDeEnvioNegocio
    {
        public List<MetodoDeEnvio> ListarTodos()
        {
            // Lógica para obtener todos los métodos de envío desde la base de datos
            return new List<MetodoDeEnvio>();
        }

        public Libro ObtenerMetodoEnvioPorID(int IdLibro)
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
    }
}
