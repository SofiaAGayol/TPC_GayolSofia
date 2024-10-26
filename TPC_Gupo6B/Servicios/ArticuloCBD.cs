using System;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Data.SqlTypes;
using Data_Management;
using System.Runtime.Remoting.Messaging;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Servicios
{
    public class ArticuloCBD
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio from ARTICULOS A left join MARCAS M on A.IdMarca = M.Id left join CATEGORIAS C on A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                int IDCategoria = 0;
                int IDMarca = 0;

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    IDCategoria = (int)datos.Lector["IdCategoria"];
                    IDMarca = (int)datos.Lector["IdMarca"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    ImagenCBD imagenes = new ImagenCBD();
                    foreach (Imagen img in imagenes.Listar())
                    {
                        if (img.IdArticulo == aux.Id)
                        {
                            aux.AgregarImagen(img);
                        }
                    }

                    CategoriaCBD categorias = new CategoriaCBD();
                    foreach (Categoria cat in categorias.Listar())
                    {
                        if (cat.Id == IDCategoria)
                        {
                            aux.Categoria = cat;
                        }
                    }

                    MarcaCBD marcas = new MarcaCBD();
                    foreach (Marca mar in marcas.Listar())
                    {
                        if (mar.Id == IDMarca)
                        {
                            aux.Marca = mar;
                        }
                    }

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
        public void ValidacionAgregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select count(*) from ARTICULOS where Codigo = @Codigo");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.ejecutarLectura();

                int count = 0;
                if (datos.Lector.Read())
                {
                    count = (int)datos.Lector[0];
                }

                if (count > 0)
                {
                    // Lanzar una excepción en lugar de mostrar MessageBox
                    throw new Exception("Ya existe un artículo con este código.");
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
        }
        public void Agregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) Values(@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marca.Id);
                datos.setearParametro("@IdCategoria", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);
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

        public void Modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Codigo = @Codigo ");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marca.Id);
                datos.setearParametro("@IdCategoria", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Eliminar(string codArticulo)
        {

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Codigo = @Codigo");
                datos.setearParametro("@Codigo", codArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Articulo Buscar(string CodArticulo)
        {
            ArticuloCBD articuloCBD = new ArticuloCBD();
            List<Articulo> ListaArticulos = new List<Articulo>();
            ListaArticulos = articuloCBD.Listar();

            try
            {
                foreach (Articulo art in ListaArticulos)
                {
                    if (art.Codigo == CodArticulo)
                    {
                        return art;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
