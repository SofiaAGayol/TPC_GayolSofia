using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GayolSofia
{
    public partial class MainCliente : System.Web.UI.Page
    {
        /* VOY A USARLO PARA PASAR LOS ID DE LOS LIBROS Y VER EL DETALLE DE CADA UNO
        protected void botonElegir_Click(object sender, EventArgs e)
        {
            Prestamo voucher = (Prestamo)Session["voucherActual"];

            Button button = (Button)sender;
            int idArticuloSeleccionado = Convert.ToInt32(button.CommandArgument);

            if (voucher != null)
            {
                voucher.IdArticulo = idArticuloSeleccionado;
                Session["voucherActual"] = voucher;

                Libro articuloSeleccionado = ListaArticulos.FirstOrDefault(a => a.Id == idArticuloSeleccionado);

                if (articuloSeleccionado != null)
                {
                    //confirmacion para redirigir
                    string script = $"alert('Usted eligió el artículo: {articuloSeleccionado.Nombre}.');" +
                                "window.location.href='Login.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

            }


        }
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLibros();
                CargarCategorias();
                CargarAutores();
            }
        }

        private void CargarLibros()
        {
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.Listar();
            RepeaterArticulos.DataSource = listaLibros;
            RepeaterArticulos.DataBind();
        }
        private void CargarCategorias()
        {
            CategoriaNegocio categorias = new CategoriaNegocio();
            List<Categoria> listaCategorias = categorias.Listar();
            RepeaterCategorias.DataSource = listaCategorias;
            RepeaterCategorias.DataBind();
        }
        private void CargarAutores()
        {
            AutorNegocio autores = new AutorNegocio();
            List<Autor> listaAutores = autores.Listar();
            RepeaterAutores.DataSource = listaAutores;
            RepeaterAutores.DataBind();
        }

        protected void botonElegir_Click(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            int idLibro = Convert.ToInt32(boton.CommandArgument);
        }
    }
}