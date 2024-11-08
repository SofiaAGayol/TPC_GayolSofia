using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CarritoNegocio
    {
        public void AgregarLibroAlCarrito(int idUsuario, int idLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Carrito (IDUsuario, IDLibro) VALUES (@IDUsuario, @IDLibro)");
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.setearParametro("@IDLibro", idLibro);
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
