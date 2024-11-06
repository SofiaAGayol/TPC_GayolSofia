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
    public partial class AgregarLibro : System.Web.UI.Page
    {
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBaja.Visible = false;
            CbBajaDef.Visible = false;
            btnBajaDef.Visible = false;
            btnRestablecer.Visible = false;
            btnGuardar.Visible = false;
            btnModificar.Visible = false;

            //NORMALMENTE SERIA PARA AGREGAR
            if (!IsPostBack)
            {
                CargarAutores();
                CargarCategorias();
                btnGuardar.Visible = true;

                //SI SE LE PASA POR PARAMETRO UN ID VA A HABILITARSE PARA "DAR BAJA"
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    LibroNegocio libroNegocio = new LibroNegocio();
                    Libro libro = libroNegocio.LibroPorID(id);

                    // Precargar los valores en los controles del formulario
                    txtTitulo.Text = libro.Titulo;
                    ddlAutor.SelectedValue = libro.Autor.IdAutor.ToString();  // Asumiendo que Autor es un objeto con una propiedad IdAutor
                    ddlCategoria.SelectedValue = libro.Categoria.IdCategoria.ToString();  // Asumiendo que Categoria es un objeto con una propiedad IdCategoria
                    txtFechaPublicacion.Text = libro.FechaPublicacion.ToString("yyyy-MM-dd");  // Formateamos la fecha en formato compatible con un TextBox
                    txtEjemplares.Text = libro.Ejemplares.ToString();
                    txtImagenURL.Text = libro.Imagen;


                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnBaja.Visible = true;
                    CbBajaDef.Visible = false;
                    btnBajaDef.Visible = false;

                    //SI SE LE PASA POR PARAMETRO UN ID Y ADEMÁS ESTA DADO DE BAJA SE ACTIVA EL BOTON "RESTABLECER"
                    /*
                    if (!usuarioNegocio.estaBaja(id))
                    {
                        btnModificar.Visible = true;
                        btnBaja.Visible = false;
                        CbBajaDef.Visible = false;
                        btnBajaDef.Visible = false;
                        btnRestablecer.Visible = true;
                    }
                    */
                }
            }

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
        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.Listar(); 

            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Descripcion";  
            ddlCategoria.DataValueField = "IDCategoria"; 
            ddlCategoria.DataBind();

            if (ddlCategoria.Items.Count > 0)
            {
                ddlCategoria.SelectedIndex = 0;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            string titulo = txtTitulo.Text;
            string autor = ddlAutor.SelectedValue; 
            string categoria = ddlCategoria.SelectedValue; 
            string fechaPublicacion = txtFechaPublicacion.Text; 
            string ejemplares = txtEjemplares.Text; 
            string imagenURL = txtImagenURL.Text;  
            
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
            else if (string.IsNullOrEmpty(ejemplares) || !int.TryParse(ejemplares, out _))
            {
                mensajeError = "Número de ejemplares no válido.";
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
            }

            // Agregamos libro
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