using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia.negocio
{
    public class RolNegocio
    {
        public List<Rol> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Rol> listaRoles = new List<Rol>();

            try
            {
                datos.setearConsulta("SELECT IDRol, Descripcion FROM Rol");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol rol = new Rol
                    {
                        IDRol = (int)datos.Lector["IDRol"],
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };
                    listaRoles.Add(rol);
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

            return listaRoles;
        }

        public string NombreRol(int IDRol)
        {
            AccesoDatos datos = new AccesoDatos();
            string nombreRol = "";

            try
            {
                datos.setearConsulta("SELECT Descripcion FROM Rol WHERE IDRol = @IDRol");
                datos.setearParametro("@IDRol", IDRol);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    nombreRol = datos.Lector["Descripcion"].ToString();
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

            return nombreRol; 
        }
    }
}