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

            btnEliminarAutor.Visible = false;
            btnRestablecerAutor.Visible = false;
            btnGuardarAutor.Visible = false;
            btnModificarAutor.Visible = false;

            if (!IsPostBack)
            {
                btnGuardarAutor.Visible = true;

                if (Request.QueryString["id"] != null)
                {
                    id = int.Parse(Request.QueryString["id"]);

                    AutorNegocio autorNegocio = new AutorNegocio();
                    Autor autor = autorNegocio.ObtenerAutorPorId(id);

                    txtNombreAutor.Text = autor.Nombre;
                    txtApellidoAutor.Text = autor.Apellido;
                    ddlNacionalidadAutor.SelectedValue = autor.Nacionalidad.ToString();
                    txtBestSellerAutor.Text = autor.BestSeller;

                    btnModificarAutor.Visible = true;
                    btnGuardarAutor.Visible = false;
                    btnEliminarAutor.Visible = true;

                    if (autorNegocio.estaBaja(id))
                    {
                        btnModificarAutor.Visible = false;
                        btnEliminarAutor.Visible = false;
                        btnRestablecerAutor.Visible = true;
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

            pnlMensajeAutor.Style["display"] = "block";
            divAlert.Style["display"] = "none";

        }


        protected void btnCerrarMensajeAutor_Click(object sender, EventArgs e)
        {
            pnlMensajeAutor.Style["display"] = "none";
            Response.Redirect("Usuarios.aspx");
        }


        protected void btnEliminarAutor_Click(object sender, EventArgs e)
        {
            btnEliminarAutor.Visible = false;
            CbBajaDef.Visible = true;
            btnBajaDef.Visible = true;
        }

        protected void btnBajaDef_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            AutorNegocio autorNegocio = new AutorNegocio();

            if (CbBajaDef.Checked)
            {
                autorNegocio.BajaLogica(id);
                Response.Redirect("Autores.aspx");
            }
        }

        protected void btnRestablecerAutor_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            AutorNegocio autorNegocio = new AutorNegocio();

            autorNegocio.RestablecerLogica(id);
            Response.Redirect("Autores.aspx");
        }


        protected void btnModificarAutor_Click(object sender, EventArgs e)
        {
            id = int.Parse(Request.QueryString["id"]);
            btnModificarAutor.Visible = true;

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

            autorNegocio.Modificar(nombre, apellido, idNacionalidad, bestSeller);

            pnlMensajeAutor.Style["display"] = "block";
            divAlert.Style["display"] = "none";

        }
    }
}