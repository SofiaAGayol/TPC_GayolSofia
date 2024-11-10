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
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];
                if (usuarioActivo == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                CargarDatosUsuario(usuarioActivo);
            }
        }

        private void CargarDatosUsuario(Usuario usuarioActivo)
        {
            txtFirstName.Text = usuarioActivo.Nombre;
            txtLastName.Text = usuarioActivo.Apellido;
            txtPhone.Text = usuarioActivo.Telefono;
            txtEmail.Text = usuarioActivo.Email;

            DireccionNegocio direccionNegocio = new DireccionNegocio();
            List<Direccion> direcciones = direccionNegocio.ListarPorUsuario(usuarioActivo.IdUsuario);
            if (direcciones.Any(d => d.Predeterminada))
            {
                Direccion predeterminada = direcciones.First(d => d.Predeterminada);
                txtStreetAddress.Text = $"{predeterminada.Calle} {predeterminada.Altura}";
                ddlCountry.SelectedValue = "Argentina";
                txtCity.Text = predeterminada.Aclaracion;
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            //Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];
            //if (usuarioActivo == null)
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}

            //try
            //{
            //    Prestamo prestamo = new Prestamo
            //    {
            //        Usuario = usuarioActivo,
            //        FechaInicio = DateTime.Now,
            //        FechaFin = DateTime.Now.AddMonths(1),
            //        MetodoEnvio = ObtenerMetodoEnvioSeleccionado(),
            //        MetodoRetiro = ObtenerMetodoRetiroSeleccionado(),
            //        CostoEnvio = ObtenerCostoEnvio(),
            //        Estado = "Pendiente",
            //        Devuelto = false
            //    };

            //    PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
            //    prestamoNegocio.Crear(prestamo);

            //    // Si la dirección no existe, guardarla en la base de datos
            //    if (chkSaveForNextPayment.Checked)
            //    {
            //        GuardarDireccion(usuarioActivo);
            //    }

            //    Response.Redirect("OrderConfirmation.aspx");
            //}
            //catch (Exception ex)
            //{
            //    // Mostrar error
            //    lblTotal.Text = "Error al procesar el pedido: " + ex.Message;
            //}
        }

        private void ObtenerMetodoEnvioSeleccionado()
        {
            //int idMetodoEnvio = 0;

            //if (rdoFreeShipping.Checked) idMetodoEnvio = 2;
            //if (rdoLocalShipping.Checked) idMetodoEnvio = 1;
            //if (rdoFlatRate.Checked) idMetodoEnvio = 1;

            //MetodoDeEnvioNegocio metodoEnvioNegocio = new MetodoDeEnvioNegocio();
            //return metodoEnvioNegocio.ObtenerPorId(idMetodoEnvio);
        }

        private decimal ObtenerCostoEnvio()
        {
            if (rdoFreeShipping.Checked) return 0;
            if (rdoLocalShipping.Checked) return 5000;
            if (rdoFlatRate.Checked) return 10000;
            return 0;
        }

        private void GuardarDireccion(Usuario usuario)
        {
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            Direccion nuevaDireccion = new Direccion
            {
                Usuario = usuario,
                Calle = txtStreetAddress.Text,
                Altura = Convert.ToInt32(txtCity.Text), // Aquí sería más específico, se debe manejar de acuerdo a los datos que se tenga
                CodigoPostal = "AMBA", // Esto se tiene que obtener desde la dirección ingresada
                Aclaracion = "Direccion guardada en el checkout",
                Predeterminada = true
            };
            direccionNegocio.Agregar(nuevaDireccion);
        }
    }
}