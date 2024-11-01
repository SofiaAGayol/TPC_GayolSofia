using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class CategoriaNegocio
    {
        public Categoria ObtenerCategoriaPorId(int categoriaId)
        {
            Categoria categoria = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCategoria, Descripcion FROM Categoria WHERE IdCategoria = @IdCategoria;");
                datos.setearParametro("@IdCategoria", categoriaId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    categoria = new Categoria
                    {
                        IdCategoria = (int)datos.Lector["IdCategoria"],
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener categoría: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return categoria;
        }

    }
}
