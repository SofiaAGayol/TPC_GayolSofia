using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class ClienteNegocio
    {
        //Listar
        public List<Usuario> ListarClientes()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> clientes = new List<Usuario>();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol FROM Usuarios WHERE IDRol = 4");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario cliente = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]),
                        NombreUsuario = datos.Lector["Usuario"].ToString(),
                        Clave = datos.Lector["Clave"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        DNI = datos.Lector["DNI"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        Rol = new Rol
                        {
                            IDRol = Convert.ToInt32(datos.Lector["IDRol"]),
                            Descripcion = datos.Lector["DescripcionRol"].ToString()
                        }
                    };

                    clientes.Add(cliente);
                }

                return clientes;
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

        //Informes

        public List<Usuario> ClientesActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> clientesActivos = new List<Usuario>();

            try
            {
                // (estado = 1) | IdRol = 4
                datos.setearConsulta("SELECT IDUsuario, Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol " +
                                     "FROM Usuarios WHERE IDRol = 4 AND estado = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario cliente = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(datos.Lector["IDUsuario"]),
                        NombreUsuario = datos.Lector["Usuario"].ToString(),
                        Clave = datos.Lector["Clave"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        DNI = datos.Lector["DNI"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        Rol = new Rol
                        {
                            IDRol = Convert.ToInt32(datos.Lector["IDRol"]),
                            Descripcion = datos.Lector["DescripcionRol"]?.ToString()
                        }
                    };

                    clientesActivos.Add(cliente);
                }

                return clientesActivos;
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

        public int ContarClientesActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            int cantidadClientes = 0;

            try
            {
                // IdRol = 4
                datos.setearConsulta("SELECT COUNT(*) FROM Usuarios WHERE IDRol = 4");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cantidadClientes = Convert.ToInt32(datos.Lector[0]);
                }

                return cantidadClientes;
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

