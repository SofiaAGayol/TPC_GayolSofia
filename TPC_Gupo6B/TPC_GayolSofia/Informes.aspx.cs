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
            /*
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
            */

            
            if (!IsPostBack)
            {
                CargarClientesActivos();
                CargarLibrosDisponibles();
                CargarLibrosEnPrestamo();
                CargarBalance();
                CargarCategoriasPrincipales();
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

        private void CargarLibrosEnPrestamo()
        {
            LibroNegocio libroNegocio = new LibroNegocio();
            int librosEnPrestamo = libroNegocio.ContarLibrosEnPrestamo();

            if (librosEnPrestamo > 0)
            {
                gvLibrosEnPrestamo.DataSource = librosEnPrestamo; 
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


    }
}