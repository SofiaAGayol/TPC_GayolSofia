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
            string Filtro = filtro.Text;

            if (!IsPostBack)
            {
                //Filtros                
                CargarCategorias();
                CargarAutores();

                //Libros
                CargarLibros();
            }
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.Listar();

            string sortBy = ddlSortBy.SelectedValue;

            switch (sortBy)
            {
                case "AutorAsc":
                    listaLibros = listaLibros.OrderBy(l => l.Autor.Nombre).ThenBy(l => l.Autor.Apellido).ToList();
                    break;
                case "AutorDesc":
                    listaLibros = listaLibros.OrderByDescending(l => l.Autor.Nombre).ThenByDescending(l => l.Autor.Apellido).ToList();
                    break;
                case "NombreAsc":
                    listaLibros = listaLibros.OrderBy(l => l.Titulo).ToList();
                    break;
                case "NombreDesc":
                    listaLibros = listaLibros.OrderByDescending(l => l.Titulo).ToList();
                    break;
                case "CategoriaAsc":
                    listaLibros = listaLibros.OrderBy(l => l.Categoria.Descripcion).ToList();
                    break;
                case "CategoriaDesc":
                    listaLibros = listaLibros.OrderByDescending(l => l.Categoria.Descripcion).ToList();
                    break;
            }

            if (listaLibros == null || listaLibros.Count == 0)
            {
                PanelNoLibros.Visible = true;
                RepeaterArticulos.DataSource = null;
                RepeaterArticulos.DataBind();
            }
            else
            {
                PanelNoLibros.Visible = false;
                RepeaterArticulos.DataSource = listaLibros;
                RepeaterArticulos.DataBind();
            }
        }

        protected void CargarLibros()
        {
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.Listar();

            Session.Add("listaLibros", negocio.Listar());

            RepeaterArticulos.DataSource = Session["listaLibros"];
            RepeaterArticulos.DataBind();
        }

        //Filtros
        protected void CargarCategorias()
        {
            CategoriaNegocio categorias = new CategoriaNegocio();
            List<Categoria> listaCategorias = categorias.Listar();
            RepeaterCategorias.DataSource = listaCategorias;
            RepeaterCategorias.DataBind();
        }
        protected void CargarAutores()
        {
            AutorNegocio autores = new AutorNegocio();
            List<Autor> listaAutores = autores.Listar();
            RepeaterAutores.DataSource = listaAutores;
            RepeaterAutores.DataBind();
        }
        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            if (filtro.Text != null)
            {
                List<Libro> listaLibros = (List<Libro>)Session["listaLibros"];
                List<Libro> listaFiltrada = listaLibros.FindAll(x => x.Titulo.ToUpper().Contains(filtro.Text.ToUpper()) || x.Autor.ToString().ToUpper().Contains(filtro.Text.ToUpper()) || x.Categoria.ToString().ToUpper().Contains(filtro.Text.ToUpper()));
                RepeaterArticulos.DataSource = listaFiltrada;
                RepeaterArticulos.DataBind();
            }
        }
        protected void LinkButtonTodos_Click(object sender, EventArgs e)
        {
            PanelNoLibros.Visible = false;
            CargarLibros();
        }
        protected void LinkButtonAutor_Click(object sender, EventArgs e)
        {
            LibroNegocio libros = new LibroNegocio();

            LinkButton btn = (LinkButton)sender;
            int idAutor = Convert.ToInt32(btn.CommandArgument);

            List<Libro> librosFiltrados = libros.LibrosPorAutor(idAutor);
            if (librosFiltrados.Count == 0)
            {
                PanelNoLibros.Visible = true;
                RepeaterArticulos.DataSource = null;
                RepeaterArticulos.DataBind();
            }
            else
            {
                RepeaterArticulos.DataSource = librosFiltrados;
                RepeaterArticulos.DataBind();
            }
        }

        protected void LinkButtonCategoria_Click(object sender, EventArgs e)
        {
            LibroNegocio libros = new LibroNegocio();

            LinkButton btn = (LinkButton)sender;
            int idCategoria = Convert.ToInt32(btn.CommandArgument);

            List<Libro> librosFiltrados = libros.LibrosPorCategoria(idCategoria);
            if (librosFiltrados.Count == 0)
            {
                PanelNoLibros.Visible = true;

                RepeaterArticulos.DataSource = null;
                RepeaterArticulos.DataBind();
            }
            else
            {
                PanelNoLibros.Visible = false;
                RepeaterArticulos.DataSource = librosFiltrados;
                RepeaterArticulos.DataBind();
            }
        }

        protected void botonElegir_Click(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            int idLibro = Convert.ToInt32(boton.CommandArgument);
        }
    }
}