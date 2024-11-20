using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class MainCliente : System.Web.UI.Page
    {
        Usuario usuarioActivo;
        Libro libroActivo;
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

        protected void CargarLibros()
        {
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.ListarDisponibles();

            Session.Add("listaLibros", negocio.ListarDisponibles());

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

        //Detalle libros

        protected void botonDetalles_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();

            Button button = (Button)sender;
            int idLibroSeleccionado = Convert.ToInt32(button.CommandArgument);

            List<Libro> listaLibros = negocio.Listar();

            Libro libroSeleccionado = listaLibros.FirstOrDefault(l => l.IdLibro == idLibroSeleccionado);

            if (libroSeleccionado != null)
            {
                Response.Redirect("DetalleLibro.aspx?id=" + idLibroSeleccionado, false);
            }
            else
            {
                string script = $"alert('El libro seleccionado no existe.');";
            }

        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            LibroNegocio libroNegocio = new LibroNegocio();
            Button button = (Button)sender;
            string script;

            usuarioActivo = (Usuario)Session["UsuarioActivo"];

            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int idLibroSeleccionado = Convert.ToInt32(button.CommandArgument);

            List<Libro> librosEnPrestamo = libroNegocio.ListarLibrosEnPrestamoPorUsuario(usuarioActivo.IdUsuario);
            bool libroYaEnPrestamo = librosEnPrestamo.Any(libro => libro.IdLibro == idLibroSeleccionado);

            List<Libro> listaLibros = libroNegocio.Listar();
            Libro libroSeleccionado = listaLibros.FirstOrDefault(l => l.IdLibro == idLibroSeleccionado);

            if (libroYaEnPrestamo)
            {
                script = "alert('El libro ya está en préstamo activo y no puede ser agregado al carrito.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            else if (libroSeleccionado != null && libroSeleccionado.Estado)
            {
                
                try
                {
                    if (!carritoNegocio.ExisteEnCarrito(usuarioActivo.IdUsuario, libroSeleccionado.IdLibro))
                    {
                        carritoNegocio.AgregarLibroAlCarrito(usuarioActivo.IdUsuario, libroSeleccionado.IdLibro);
                        script = "alert('Libro agregado al carrito con éxito.');";
                    }
                    else
                    {
                        script = "alert('Este libro ya se encuentra en el carrito.');";
                    }
                }
                catch (Exception ex)
                {
                    script = "alert('El libro ya está en préstamo activo y no puede ser agregado al carrito.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            else
            {
                script = "alert('El libro seleccionado no está disponible.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
        }

    }
}