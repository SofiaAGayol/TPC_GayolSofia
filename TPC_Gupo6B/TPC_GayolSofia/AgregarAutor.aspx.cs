using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_GayolSofia.dominio;
using TPC_GayolSofia.negocio;

namespace TPC_GayolSofia
{
    public partial class AgregarAutor : System.Web.UI.Page
    {
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarNacionalidad();

            btnBaja.Visible = false;
            CbBajaDef.Visible = false;
            btnBajaDef.Visible = false;
            btnRestablecer.Visible = false;
            btnGuardar.Visible = false;
            btnModificar.Visible = false;

            if (!IsPostBack)
            {
                btnGuardar.Visible = true;

                if (Request.QueryString["id"] != null)
                {
                    id = int.Parse(Request.QueryString["id"]);

                    AutorNegocio autorNegocio = new AutorNegocio();
                    Autor autor = autorNegocio.ObtenerAutorPorId(id);

                    txtNombreAutor.Text = autor.Nombre;
                    txtApellidoAutor.Text = autor.Apellido;
                    ddlNacionalidadAutor.SelectedValue = autor.Nacionalidad.ToString();
                    txtBestSellerAutor.Text = autor.BestSeller;

                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnBaja.Visible = true;
                    CbBajaDef.Visible = false;
                    btnBajaDef.Visible = false;

                    if (!autorNegocio.estaBaja(id))
                    {
                        btnModificar.Visible = true;
                        btnBaja.Visible = false;
                        CbBajaDef.Visible = false;
                        btnBajaDef.Visible = false;
                        btnRestablecer.Visible = true;
                    }
                }

            }


        }
        private void CargarNacionalidad()
        {
            NacionalidadNegocio nacionalidadNegocio = new NacionalidadNegocio();
            List<Nacionalidad> nacionalidades = nacionalidadNegocio.Listar();

            ddlNacionalidadAutor.DataSource = nacionalidades;
            ddlNacionalidadAutor.DataTextField = "Descripcion";
            ddlNacionalidadAutor.DataValueField = "IdNacionalidad";
            ddlNacionalidadAutor.DataBind();

            if (ddlNacionalidadAutor.Items.Count > 0)
            {
                ddlNacionalidadAutor.SelectedIndex = 0;
            }
        }

        protected void btnGuardarAutor_Click(object sender, EventArgs e)
        {
            AutorNegocio autorNegocio = new AutorNegocio();
            string nombre = txtNombreAutor.Text;
            string apellido = txtApellidoAutor.Text;
            int idNacionalidad = Convert.ToInt32(ddlNacionalidadAutor.SelectedValue);
            string bestSeller = txtBestSellerAutor.Text;

            string mensajeError = autorNegocio.ValidarCampos(nombre, apellido);

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

            autorNegocio.Agregar(nombre,apellido,idNacionalidad,bestSeller);

            pnlMensaje.Style["display"] = "block";
            divAlert.Style["display"] = "none";

        }

        
        protected void btnCerrarMensajeAutor_Click(object sender, EventArgs e)
        {
            pnlMensaje.Style["display"] = "none";
            Response.Redirect("Autores.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AutorNegocio autorNegocio = new AutorNegocio();
            string nombre = txtNombreAutor.Text;
            string apellido = txtApellidoAutor.Text;
            int idNacionalidad = Convert.ToInt32(ddlNacionalidadAutor.SelectedValue);
            string bestSeller = txtBestSellerAutor.Text;

            string mensajeError = string.Empty;

            // Validaciones
            if (string.IsNullOrEmpty(nombre))
            {
                mensajeError = "Nombre no válido.";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                mensajeError = "Apellido no válido.";
            }
            else if (idNacionalidad <= 0)
            {
                mensajeError = "Nacionalidad no válida.";
            }
            else if (string.IsNullOrEmpty(bestSeller))
            {
                mensajeError = "Best seller no válido.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

            autorNegocio.Agregar(nombre, apellido, idNacionalidad, bestSeller);

            pnlMensaje.Style["display"] = "block";
            divAlert.Style["display"] = "none";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idAutor = int.Parse(Request.QueryString["id"]);
            btnModificar.Visible = true;

            AutorNegocio autorNegocio = new AutorNegocio();
            string nombre = txtNombreAutor.Text;
            string apellido = txtApellidoAutor.Text;
            int idNacionalidad = Convert.ToInt32(ddlNacionalidadAutor.SelectedValue);
            string bestSeller = txtBestSellerAutor.Text;

            string mensajeError = string.Empty;

            // Validaciones
            if (string.IsNullOrEmpty(nombre))
            {
                mensajeError = "Nombre no válido.";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                mensajeError = "Apellido no válido.";
            }
            else if (idNacionalidad <= 0)
            {
                mensajeError = "Nacionalidad no válida.";
            }
            else if (string.IsNullOrEmpty(bestSeller))
            {
                mensajeError = "Best seller no válido.";
            }

            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

            // Mostrar mensaje de error si hay
            if (!string.IsNullOrEmpty(mensajeError))
            {
                alertMessage.InnerText = mensajeError;
                divAlert.Style["display"] = "block";
                return;
            }

            autorNegocio.Modificar(idAutor, nombre, apellido, idNacionalidad, bestSeller);

            pnlMensajeModificacion.Style["display"] = "block";
            divAlert.Style["display"] = "none";
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            btnBaja.Visible = false;
            CbBajaDef.Visible = true;
            btnBajaDef.Visible = true;
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            AutorNegocio autor = new AutorNegocio();

            autor.RestablecerLogica(id);
            Response.Redirect("Autores.aspx");
        }

        protected void btnBajaDef_Click1(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            AutorNegocio autor = new AutorNegocio();

            if (CbBajaDef.Checked)
            {
                autor.BajaLogica(id);
                Response.Redirect("Autores.aspx");
            }
        }
    }
}