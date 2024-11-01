using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class AutorNegocio
    {
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
