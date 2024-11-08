using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_GayolSofia.dominio;

namespace negocio
{
    public class AutorNegocio
    {
        public List<Autor> Listar() //LISTA SOLO AUTORES ACTIVOS (ESTADO = 1)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Autor> autores = new List<Autor>();

            try
            {
                datos.setearConsulta("SELECT IDAutor, Nombre, Apellido, a.IdNacionalidad, n.Descripcion, BestSeller, Estado FROM Autores a JOIN Nacionalidad n ON a.IdNacionalidad = n.IdNacionalidad WHERE a.Estado = 1 ORDER BY Apellido ASC;\r\n");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Autor autor = new Autor
                    {
                        IdAutor = Convert.ToInt32(datos.Lector["IDAutor"]),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        BestSeller = datos.Lector["BestSeller"].ToString(),
                        Estado = (bool)datos.Lector["Estado"],
                        Nacionalidad = new Nacionalidad
                        {
                            IdNacionalidad = (int)datos.Lector["IdNacionalidad"],
                            Descripcion = datos.Lector["Descripcion"].ToString()
                        }
                    };

                    autores.Add(autor);
                }

                return autores;
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

        public Autor ObtenerAutorPorId(int autorId)
        {
            Autor autor = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdAutor, Nombre, Apellido, IdNacionalidad, BestSeller FROM Autores WHERE IdAutor = @IdAutor;");
                datos.setearParametro("@IdAutor", autorId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    autor = new Autor
                    {
                        IdAutor = (int)datos.Lector["IdAutor"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        BestSeller = datos.Lector["BestSeller"].ToString(),
                        Nacionalidad = new Nacionalidad
                        {
                            IdNacionalidad = Convert.ToInt32(datos.Lector["IdNacionalidad"]),
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener autor: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return autor;
        }
        public bool estaBaja(int idAutor)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;

            try
            {
                datos.setearConsulta("SELECT estado FROM Autores WHERE IdAutor = @idAutor;");
                datos.setearParametro("@idAutor", idAutor);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    if (!Convert.IsDBNull(datos.Lector["Estado"]))
                    {
                        bool estado = (bool)datos.Lector["Estado"];
                        resultado = estado;
                    }
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

            return resultado; // Retorna true si está activo y false si está inactivo
        }


        // ABM
        public bool Agregar(string nombre, string apellido, int idNacionalidad, string bestSeller)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Autores (Nombre, Apellido, IdNacionalidad, BestSeller, Estado) VALUES (@Nombre, @Apellido, @IdNacionalidad, @BestSeller, 1);");

                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@IdNacionalidad", idNacionalidad);
                datos.setearParametro("@BestSeller", bestSeller);

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

            return true;
        }
        public bool Modificar(int IDAutor, string nombre, string apellido, int idNacionalidad, string bestSeller)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Autores SET Nombre = @Nombre, Apellido = @Apellido, IdNacionalidad = @IdNacionalidad, BestSeller = @BestSeller WHERE IDAutor = @IDAutor;");

                datos.setearParametro("@IDAutor", IDAutor);
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@IdNacionalidad", idNacionalidad);
                datos.setearParametro("@BestSeller", bestSeller);

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

            return true;
        }
        public bool BajaLogica(int idAutor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Autores SET estado = 0 WHERE IDAutor = @idAutor;");

                datos.setearParametro("@idAutor", idAutor);

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

            return true;
        }
        public bool RestablecerLogica(int idAutor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Autores SET estado = 1 WHERE IDAutor = @idAutor;");

                datos.setearParametro("@idAutor", idAutor);

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

            return true;
        }
        public bool ExisteAutor(string nombre, string apellido)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Autores WHERE Nombre = @Nombre AND Apellido = @Apellido");
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0);
        }
        public bool ExisteAutorNuevo(string nombre, string apellido, int idAutor)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Autores WHERE Nombre = @Nombre AND Apellido = @Apellido AND IDAutor <> @IDAutor;");
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.setearParametro("@IDAutor", idAutor);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            return (contador > 0);
        }

        //Validacin
        public string ValidarCampos(string nombre, string apellido)
        {
            AccesoDatos datos = new AccesoDatos();
            int contador = 0;

            if (string.IsNullOrEmpty(nombre))
            {
                return "Nombre no válido.";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                return "Apellido no válido.";
            }
            return string.Empty;

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Contador FROM Autores WHERE Nombre = @Nombre AND Apellido = @Apellido");
                datos.setearParametro("@Nombre", nombre);
                datos.setearParametro("@Apellido", apellido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    contador = Convert.ToInt32(datos.Lector["Contador"]);
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

            if (contador > 0)
            {
                return "Ya existe un autor con el mismo nombre y apellido.";
            }

            return string.Empty;
        }

    }
    
}
