using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Data_Management;
using static System.Net.Mime.MediaTypeNames;

namespace Servicios
{
    public class ImagenCBD
    {
        public List<Imagen> Listar()
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select I.Id, I.IdArticulo, I.ImagenURL from IMAGENES I");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Url = (string)datos.Lector["ImagenURL"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];

                    lista.Add(aux);
                }

                return lista;
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

        public void Eliminar(int idArticulo)
        {

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from IMAGENES where IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Agregar(Imagen img)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into IMAGENES (IdArticulo, ImagenUrl) Values(@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", img.IdArticulo);
                datos.setearParametro("@ImagenUrl", img.Url);
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

        public void Modificar(Imagen art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update IMAGENES set IdArticulo = @IdArticulo where ImagenUrl = @ImagenUrl");
                datos.setearParametro("@IdArticulo", art.IdArticulo);
                datos.setearParametro("@ImagenUrl", art.Url);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
