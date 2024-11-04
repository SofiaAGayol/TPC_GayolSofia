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
                }
                else
                {
                    string script = "alert('ID de libro no válido.');";
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
                // Muestra alerta si el libro no existe en la base de datos
                string script = "alert('El libro seleccionado no existe.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }



        private void CargarProductosSimilares()
        {
            // Llenar el Repeater con productos similares
            var productosSimilares = new List<dynamic>
    {
        new { ImagenUrl = "~/images/producto1.jpg", Titulo = "Oficina Para Cambiar El Mundo", Precio = "$17,500" },
        new { ImagenUrl = "~/images/producto2.jpg", Titulo = "El Poder De Las Palabras", Precio = "$30,499" }
    };
            rptProductosSimilares.DataSource = productosSimilares;
            rptProductosSimilares.DataBind();
        }

        // Manejo de eventos de los botones
        protected void ComprarAhora_Click(object sender, EventArgs e)
        {
            // Lógica para compra inmediata
        }

        protected void AgregarCarrito_Click(object sender, EventArgs e)
        {
            // Lógica para agregar al carrito
        }

        protected void Thumbnail_Click(object sender, ImageClickEventArgs e)
        {
            // Cambia la imagen principal según la miniatura seleccionada
            ImageButton thumbnail = (ImageButton)sender;
            imgPrincipal.ImageUrl = thumbnail.ImageUrl;
        }

    }
}