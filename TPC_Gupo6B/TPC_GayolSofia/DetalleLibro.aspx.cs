using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class DetalleLibro : System.Web.UI.Page
    {
        Usuario usuarioActivo;
        Libro libroActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();

            if (Session["UsuarioActivo"] == null)
            {
                usuarioActivo = (Usuario)Session["UsuarioActivo"];
            }

            if (!IsPostBack)
            {
                int idLibro;

                if (int.TryParse(Request.QueryString["id"], out idLibro))
                {
                    CargarDetallesLibro(idLibro);
                    CargarProductosSimilares(idLibro);
                    CargarMismoAutor(idLibro);
                    libroActivo = negocio.LibroPorID(idLibro);
                    Session.Add("LibroActivo", libroActivo);
                }
                else
                {
                    string script = "alert('ID de libro no válido.');" +
                         "window.location.href='Home.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
        }

        private void CargarDetallesLibro(int idLibro)
        {
            LibroNegocio negocio = new LibroNegocio();
            Libro libroSeleccionado = negocio.LibroPorID(idLibro);

            if (libroSeleccionado != null)
            {
                Session["libroSeleccionado"] = libroSeleccionado;

                lblTitulo.Text = libroSeleccionado.Titulo;
                lblAutor.Text = libroSeleccionado.Autor.ToString();
                lblCat.Text = libroSeleccionado.Categoria.ToString();
                lblFecha.Text = libroSeleccionado.FechaPublicacion.ToString("yyyy");
                imgPrincipal.ImageUrl = libroSeleccionado.Imagen;
            }
            else
            {
                string script = "alert('El libro seleccionado no existe.');" +
                         "window.location.href='Home.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }

        private void CargarProductosSimilares(int idLibro)
        {
            LibroNegocio negocio = new LibroNegocio();
            Libro libro = negocio.LibroPorID(idLibro);
            List<Libro> todosLosLibrosDeLaCategoria = negocio.LibrosPorCategoria(libro.Categoria.IdCategoria);

            todosLosLibrosDeLaCategoria = todosLosLibrosDeLaCategoria
                .Where(l => l.IdLibro != idLibro)
                .ToList();

            var random = new Random();
            todosLosLibrosDeLaCategoria = todosLosLibrosDeLaCategoria
                .OrderBy(x => random.Next())
                .ToList();

            var librosSimilares = todosLosLibrosDeLaCategoria.Take(4).ToList();

            rptProductosSimilares.DataSource = librosSimilares;
            rptProductosSimilares.DataBind();
        }

        private void CargarMismoAutor(int idLibro)
        {
            LibroNegocio negocio = new LibroNegocio();
            Libro libro = negocio.LibroPorID(idLibro);
            List<Libro> todosLosLibrosDelAutor = negocio.LibrosPorAutor(libro.Autor.IdAutor);

            todosLosLibrosDelAutor = todosLosLibrosDelAutor
                .Where(l => l.IdLibro != idLibro)
                .ToList();

            if (todosLosLibrosDelAutor != null && todosLosLibrosDelAutor.Count > 0)
            {
                var random = new Random();
                todosLosLibrosDelAutor = todosLosLibrosDelAutor
                    .OrderBy(x => random.Next())
                    .ToList();

                var librosAutor = todosLosLibrosDelAutor.Take(4).ToList();

                rptMismoAutor.DataSource = librosAutor;
                rptMismoAutor.DataBind();
            }
            else
            {

                lblMismoAutor.Text = "";
            }
        }

        protected void Solicitar_Click(object sender, EventArgs e)
        {
            CarritoNegocio negocio = new CarritoNegocio();

            usuarioActivo = (Usuario)Session["UsuarioActivo"];
            libroActivo = (Libro)Session["LibroActivo"];

            if (Session["LibroActivo"] != null && libroActivo.Estado)
            {
                negocio.AgregarLibroAlCarrito(usuarioActivo.IdUsuario, libroActivo.IdLibro);
                Response.Redirect("Carrito.aspx");
            }
            else
            {
                alertMessage.InnerText = "El libro seleccionado no está disponible para agregar al carrito.";
                divAlert.Style["display"] = "block";
            }


        }

        protected void AgregarCarrito_Click(object sender, EventArgs e)
        {
            CarritoNegocio negocio = new CarritoNegocio();

            usuarioActivo = (Usuario)Session["UsuarioActivo"];
            libroActivo = (Libro)Session["LibroActivo"];

            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["LibroActivo"] != null && libroActivo.Estado)
            {
                try
                {
                    negocio.AgregarLibroAlCarrito(usuarioActivo.IdUsuario, libroActivo.IdLibro);
                    alertMessage.InnerText = "Libro agregado al carrito con éxito.";
                    divAlert.Style["display"] = "block";
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("El libro ya está en el carrito"))
                    {
                        alertMessage.InnerText = "Este libro ya se encuentra en el carrito.";
                        divAlert.Style["display"] = "block";
                    }
                    else
                    {
                        alertMessage.InnerText = "Ocurrió un error al intentar agregar el libro al carrito.";
                        divAlert.Style["display"] = "block";
                    }
                }
            }
            else
            {
                alertMessage.InnerText = "El libro seleccionado no está disponible para agregar al carrito.";
                divAlert.Style["display"] = "block";
            }


        }

    }
}