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
    public partial class AgregarLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarAutores();

        }


        private void CargarAutores()
        {
            AutorNegocio autorNegocio = new AutorNegocio();
            List<Autor> autores = autorNegocio.Listar();

            ddlAutor.DataSource = autores;
            ddlAutor.DataTextField = "NombreCompleto";
            ddlAutor.DataValueField = "IdAutor";
            ddlAutor.DataBind();

            if (ddlAutor.Items.Count > 0)
            {
                ddlAutor.SelectedIndex = 0;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            string titulo = txtTitulo.Text;
            string autor = ddlAutor.SelectedValue; 
            string categoria = txtIDCategoria.Text; 
            string fechaPublicacion = txtFechaPublicacion.Text; 
            string ejemplares = txtEjemplares.Text; 
            string imagenURL = txtImagenURL.Text;  
            /*
            string mensajeError = string.Empty;

            // Validaciones
            if (string.IsNullOrEmpty(titulo))
            {
                mensajeError = "Título no válido.";
            }
            else if (libroNegocio.ExisteTitulo(titulo))  // Verificar si el título ya está registrado
            {
                mensajeError = "Título ya registrado.";
            }
            else if (string.IsNullOrEmpty(autor))
            {
                mensajeError = "Autor no válido.";
            }
            else if (string.IsNullOrEmpty(categoria))
            {
                mensajeError = "Categoría no válida.";
            }
            else if (string.IsNullOrEmpty(fechaPublicacion) || !DateTime.TryParse(fechaPublicacion, out _))  // Validar fecha
            {
                mensajeError = "Fecha de publicación no válida.";
            }
            else if (string.IsNullOrEmpty(ejemplares) || !int.TryParse(ejemplares, out _))  // Validar número de ejemplares
            {
                mensajeError = "Número de ejemplares no válido.";
            }
            else if (string.IsNullOrEmpty(disponibles) || !int.TryParse(disponibles, out _))  // Validar número de ejemplares disponibles
            {
                mensajeError = "Número de ejemplares disponibles no válido.";
            }
            else if (string.IsNullOrEmpty(imagenURL))
            {
                mensajeError = "URL de la imagen no válida.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }*/

            // Agregar libro
            libroNegocio.Agregar(titulo, DateTime.Parse(fechaPublicacion), int.Parse(ejemplares),true, imagenURL, int.Parse(autor), int.Parse(categoria));

            pnlMensaje.Style["display"] = "block";
            divAlert.Style["display"] = "none";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {

        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {

        }

        protected void btnBajaDef_Click(object sender, EventArgs e)
        {

        }
    }
}