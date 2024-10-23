using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class VoucherNegocio
    {
        //Validador del voucher
        public bool ValidarVoucher(Prestamo voucher)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCliente, FechaCanje, IdArticulo FROM Vouchers WHERE CodigoVoucher = @codigo");
                datos.setearParametro("@codigo", voucher.CodigoVoucher);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    //IdCliente, FechaCanje y IdArticulo = null
                    if (datos.Lector["IdCliente"] == DBNull.Value && datos.Lector["FechaCanje"] == DBNull.Value && datos.Lector["IdArticulo"] == DBNull.Value)
                    {
                        return true;
                    }
                }

                return false;
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

        public void agregarVoucher(Prestamo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Vouchers SET IdCliente = @IdCliente, FechaCanje = @FechaCanje, IdArticulo = @IdArticulo " +
                                     "WHERE CodigoVoucher = @CodigoVoucher;");

                datos.setearParametro("@CodigoVoucher", nuevo.CodigoVoucher);
                datos.setearParametro("@IdCliente", nuevo.IdCliente);
                datos.setearParametro("@FechaCanje", nuevo.FechaCanje); 
                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);

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
