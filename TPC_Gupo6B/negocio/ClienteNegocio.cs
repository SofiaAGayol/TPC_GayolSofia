using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> buscarPorDNI(int Dni)
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM Clientes WHERE Documento = @DNI ");
                datos.setearParametro("@DNI", Dni);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                    Cliente cliente = new Cliente();

                    if (cliente != null)
                    {

                        cliente.Id = (int)datos.Lector["Id"];
                        cliente.Nombre = (string)datos.Lector["Nombre"];
                        cliente.Apellido = (string)datos.Lector["Apellido"];
                        cliente.Email = (string)datos.Lector["Email"];
                        cliente.Direccion = (string)datos.Lector["Direccion"];
                        cliente.Ciudad = (string)datos.Lector["Ciudad"];
                        cliente.CP = (int)datos.Lector["CP"];

                        lista.Add(cliente);
                    }
                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cliente buscarPorID(int ID)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM Clientes WHERE Id = @Id");
                datos.setearParametro("@Id", ID);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente
                    {
                        Documento = (int)datos.Lector["Documento"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Email = (string)datos.Lector["Email"],
                        Direccion = (string)datos.Lector["Direccion"],
                        Ciudad = (string)datos.Lector["Ciudad"],
                        CP = (int)datos.Lector["CP"]
                    };
                }
                return cliente;
            }
            catch (Exception ex)
            {
                return cliente;
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarCliente(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO CLIENTES (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                     "VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP);" +
                                     "SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@Documento", nuevo.Documento);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Direccion", nuevo.Direccion);
                datos.setearParametro("@Ciudad", nuevo.Ciudad);
                datos.setearParametro("@CP", nuevo.CP);

                int idCliente = Convert.ToInt32(datos.ejecutarAccion());
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

