using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class NacionalidadNegocio
    {
        public List<Nacionalidad> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Nacionalidad> listaNacionalidades = new List<Nacionalidad>();

            try
            {
                datos.setearConsulta("SELECT IdNacionalidad, Descripcion FROM Nacionalidad");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Nacionalidad nacionalidad = new Nacionalidad
                    {
                        IdNacionalidad = (int)datos.Lector["IdNacionalidad"],
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };

                    listaNacionalidades.Add(nacionalidad);
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

            return listaNacionalidades;
        }
    }
}
