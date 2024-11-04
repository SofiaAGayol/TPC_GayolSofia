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
                CargarDetallesLibro();
                CargarProductosSimilares();
            }
        }

        private void CargarDetallesLibro()
        {
            // Cargar detalles del libro desde la base de datos o API
            lblTitulo.Text = "Palabras Semilla - Magela Demarco / Caru Grossi";
            lblPrecio.Text = "$9,300";
            lblCuotas.Text = "en 6 cuotas de $2,154.34";
            lblEnvio.Text = "Llega mañana";
            lblRetiro.Text = "Retirá a partir de mañana en correos y otros puntos";
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