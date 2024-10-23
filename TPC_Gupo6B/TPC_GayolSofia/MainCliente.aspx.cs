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
    public partial class MainCliente : System.Web.UI.Page
    {
        /*
        public List<Libro> ListaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Mostrar los art en las tarjetas
            LibroNegocio articuloNegocio = new LibroNegocio();
            ListaArticulos = articuloNegocio.listarConSP();

            if (!IsPostBack)
            {
                RepeaterArticulos.DataSource = ListaArticulos;
                RepeaterArticulos.DataBind();
            }

            //Obtengo el vocher ingresado antes
            Prestamo voucher = (Prestamo)Session["voucherActual"];

            if (voucher == null)
            {
                //redirijo si no hay nada en la sesion
                string script = "alert('No hay ningun voucher ingresado.');" +
                         "window.location.href='Default.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

        }

        protected void botonElegir_Click(object sender, EventArgs e)
        {
            Prestamo voucher = (Prestamo)Session["voucherActual"];

            Button button = (Button)sender;
            int idArticuloSeleccionado = Convert.ToInt32(button.CommandArgument);

            if (voucher != null)
            {
                voucher.IdArticulo = idArticuloSeleccionado;
                Session["voucherActual"] = voucher;

                Libro articuloSeleccionado = ListaArticulos.FirstOrDefault(a => a.Id == idArticuloSeleccionado);

                if (articuloSeleccionado != null)
                {
                    //confirmacion para redirigir
                    string script = $"alert('Usted eligió el artículo: {articuloSeleccionado.Nombre}.');" +
                                "window.location.href='Login.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

            }


        }
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLibros();
            }
        }

        private void CargarLibros()
        {
            LibroNegocio negocio = new LibroNegocio();
            List<Libro> listaLibros = negocio.listar();
            RepeaterArticulos.DataSource = listaLibros;
            RepeaterArticulos.DataBind();
        }

        protected void botonElegir_Click(object sender, EventArgs e)
        {
            // Código para manejar la lógica al elegir un libro (por ejemplo, agregarlo a la cesta)
            var boton = (Button)sender;
            int idLibro = Convert.ToInt32(boton.CommandArgument);
            // Aquí puedes realizar las acciones necesarias con el id del libro
        }
    }
}