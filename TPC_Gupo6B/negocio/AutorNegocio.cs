using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AutorNegocio
    {
        public List<Autor> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Autor> autores = new List<Autor>();

            try
            {
                datos.setearConsulta("SELECT IDAutor, Nombre, Apellido FROM Autores ORDER BY Apellido ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Autor autor = new Autor
                    {
                        IdAutor = Convert.ToInt32(datos.Lector["IDAutor"]),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString()
                    };

                    autores.Add(autor);
                }

                return autores;
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


        public Autor ObtenerAutorPorId(int autorId)
        {
            Autor autor = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdAutor, Nombre, Apellido, Nacionalidad, BestSeller FROM Autor WHERE IdAutor = @IdAutor;");
                datos.setearParametro("@IdAutor", autorId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    autor = new Autor
                    {
                        IdAutor = (int)datos.Lector["IdAutor"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Nacionalidad = datos.Lector["Nacionalidad"].ToString(),
                        BestSeller = datos.Lector["BestSeller"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener autor: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return autor;
        }
    }
}
