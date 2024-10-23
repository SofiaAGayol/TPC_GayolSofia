using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {

        public void agregar(Imagen nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES ( IdArticulo, ImagenUrl) " + "VALUES (@IdArticulo, @ImagenUrl)" + "SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);

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
