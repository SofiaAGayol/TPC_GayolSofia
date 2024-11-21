using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using TPC_GayolSofia.dominio;

namespace TPC_GayolSofia
{
    public partial class Inicio : System.Web.UI.Page
    {

        int IDUsuarioActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            //HABILITAR ESTO LUEGO (SI NO ESTAS CON LA SESION INICIADA TE REDIRIGE AL LOGIN)
            
            if (Session["UsuarioActivo"] != null)
            {
                Usuario usuarioActivo = Session["UsuarioActivo"] as Usuario;

                if (usuarioActivo != null)
                {
                    Saludo.Text = "Hola, " + usuarioActivo.Nombre;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            

            
            if (!IsPostBack)
            {
                CargarClientesActivos();
                CargarLibrosDisponibles();
                CargarLibrosEnPrestamo();
                CargarBalance();
                CargarCategoriasPrincipales();
                CargarLibrosEnstock();
            }
        }

        private void CargarClientesActivos()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            int cantidadClientesActivos = clienteNegocio.ContarClientesActivos();
            lblCantidadClientesActivos.Text = cantidadClientesActivos.ToString();
        }
        private void CargarLibrosDisponibles()
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            int cantidadLibrosDisponibles = libroNegocio.ContarLibrosDisponibles();

            if (lblCantidadLibrosDisponibles != null)
            {
                lblCantidadLibrosDisponibles.Text = cantidadLibrosDisponibles.ToString();
            }
            else
            {
                throw new Exception("lblCantidadLibrosDisponibles es null.");
            }
        }
        private void CargarLibrosEnstock()
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            int cantidadLibrosEnStock = libroNegocio.ContarLibrosStock();

            if (lblStock != null)
            {
                lblStock.Text = cantidadLibrosEnStock.ToString();
            }
            else
            {
                throw new Exception("lblStock es null.");
            }

        } 
        private void CargarLibrosEnPrestamo()
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            List<Libro> listaLibrosEnPrestamo = libroNegocio.ObtenerLibrosEnPrestamo();
            int cantidadLibrosEnPrestamo = libroNegocio.ContarLibrosEnPrestamo();

            if (lblCantidadLibrosPrestamo != null)
            {
                lblCantidadLibrosPrestamo.Text = cantidadLibrosEnPrestamo.ToString();
            }
            else
            {
                throw new Exception("lblCantidadLibrosDisponibles es null.");
            }

            if (listaLibrosEnPrestamo != null && listaLibrosEnPrestamo.Count > 0)
            {
                gvLibrosEnPrestamo.DataSource = listaLibrosEnPrestamo;
                gvLibrosEnPrestamo.DataBind();
            }
            else
            {
                gvLibrosEnPrestamo.DataSource = null;
                gvLibrosEnPrestamo.DataBind();
            }
        }
        private void CargarBalance()
        {
        }
        private void CalcularBalance()
        {
        }
        private void CargarCategoriasPrincipales()
        {
        }

        protected void gvLibrosEnPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DevolverLibro")
            {
                int idPrestamo = Convert.ToInt32(e.CommandArgument);

                try
                {
                    PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
                    prestamoNegocio.MarcarPrestamoComoDevuelto(idPrestamo);

                    LibroNegocio libroNegocio = new LibroNegocio();
                    libroNegocio.IncrementarStockLibro(idPrestamo);

                    CargarLibrosEnPrestamo();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El libro ha sido devuelto exitosamente.');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al procesar la devolución: {ex.Message}');", true);
                }
            }
        }
    }
}