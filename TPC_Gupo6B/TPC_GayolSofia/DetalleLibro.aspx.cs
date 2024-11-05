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
    public partial class DetalleLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idLibro;
                if (int.TryParse(Request.QueryString["id"], out idLibro))
                {
                    CargarDetallesLibro(idLibro);
                    CargarProductosSimilares(idLibro);
                    CargarMismoAutor(idLibro);
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
            // Lógica para prestamo inmediato
        }

        protected void AgregarCarrito_Click(object sender, EventArgs e)
        {
            // Lógica para agregar al carrito
        }

    }
}