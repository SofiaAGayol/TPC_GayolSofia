using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                datos.setearConsulta("SELECT IDCategoria, Descripcion FROM Categoria ORDER BY Descripcion ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        IdCategoria = Convert.ToInt32(datos.Lector["IDCategoria"]),
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };

                    categorias.Add(categoria);
                }

                return categorias;
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
