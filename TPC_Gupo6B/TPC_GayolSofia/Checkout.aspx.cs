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
                CargarLibrosCarrito(usuarioActivo);
                CargarMetodosDeEnvioyRetiro();
            }
        }

        private void CargarDatosUsuario(Usuario usuarioActivo)
        {
            txtNombre.Text = usuarioActivo.Nombre;
            txtApellido.Text = usuarioActivo.Apellido;
            txtTelefono.Text = usuarioActivo.Telefono;
            txtCorreoElectronico.Text = usuarioActivo.Email;

            DireccionNegocio direccionNegocio = new DireccionNegocio();
            List<Direccion> direcciones = direccionNegocio.ListarDireccionesPorUsuario(usuarioActivo.IdUsuario);
            if (direcciones.Any(d => d.Predeterminada))
            {
                Direccion predeterminada = direcciones.First(d => d.Predeterminada);
                txtDireccion.Text = predeterminada.Calle;
                ddlPais.SelectedValue = "Argentina";
                txtAltura.Text = predeterminada.Altura.ToString();
                txtCodigoPostal.Text = predeterminada.CodigoPostal;
                txtCiudad.Text = predeterminada.Aclaracion;
            }
        }
        private void CargarLibrosCarrito(Usuario usuarioActivo)
        {
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            List<Libro> librosCarrito = carritoNegocio.ListarLibrosEnCarrito(usuarioActivo.IdUsuario);

            if (librosCarrito != null && librosCarrito.Count > 0)
            {
                rptLibrosCarrito.DataSource = librosCarrito.Select(libro => new
                {
                    Titulo = libro.Titulo,
                    Autor = $"{libro.Autor.Nombre} {libro.Autor.Apellido}"
                });
                rptLibrosCarrito.DataBind();
            }

            lblTotalLibros.Text = librosCarrito.Count.ToString();
        }
        private void CargarMetodosDeEnvioyRetiro()
        {
            string codigoPostal = txtCodigoPostal.Text.Trim();
            MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();
            bool esAMBA = metodoDeEnvioNegocio.EsCodigoPostalAMBA(codigoPostal);

            CargarOpcionesEnvio(metodoDeEnvioNegocio, esAMBA);
            CargarOpcionesRetiro(metodoDeEnvioNegocio, esAMBA);
        }
        private void CargarOpcionesEnvio(MetodoDeEnvioNegocio metodoDeEnvioNegocio, bool esAMBA)
        {
            string metodoEnvioSeleccionado = rblOpcionesEnvio.SelectedValue;
            List<MetodoDeEnvio> metodosDeEnvio = metodoDeEnvioNegocio.ListarTodos();

            foreach (var metodo in metodosDeEnvio)
            {
                decimal costo = esAMBA ? metodo.CostoAMBA : metodo.CostoExterior;
                metodo.Descripcion += $" - <span style='color:green;'>${costo:N2}</span>";
            }

            rblOpcionesEnvio.DataSource = metodosDeEnvio;
            rblOpcionesEnvio.DataTextField = "Descripcion";
            rblOpcionesEnvio.DataValueField = "IdMetodoEnvio";
            rblOpcionesEnvio.DataBind();

            if (!string.IsNullOrEmpty(metodoEnvioSeleccionado) && rblOpcionesEnvio.Items.FindByValue(metodoEnvioSeleccionado) != null)
            {
                rblOpcionesEnvio.SelectedValue = metodoEnvioSeleccionado;
            }
            else
            {
                rblOpcionesEnvio.SelectedIndex = 0;
            }
        }
        private void CargarOpcionesRetiro(MetodoDeEnvioNegocio metodoDeEnvioNegocio, bool esAMBA)
        {
            string metodoRetiroSeleccionado = rblOpcionesRetiro.SelectedValue;
            List<MetodoDeRetiro> metodosDeRetiro = metodoDeEnvioNegocio.ListarTodosRetiro();

            foreach (var metodo in metodosDeRetiro)
            {
                decimal costo = esAMBA ? metodo.CostoAMBA : metodo.CostoExterior;
                metodo.Descripcion += $" - <span style='color:green;'>${costo:N2}</span>";
            }

            rblOpcionesRetiro.DataSource = metodosDeRetiro;
            rblOpcionesRetiro.DataTextField = "Descripcion";
            rblOpcionesRetiro.DataValueField = "IdMetodoRetiro";
            rblOpcionesRetiro.DataBind();

            if (!string.IsNullOrEmpty(metodoRetiroSeleccionado) && rblOpcionesRetiro.Items.FindByValue(metodoRetiroSeleccionado) != null)
            {
                rblOpcionesRetiro.SelectedValue = metodoRetiroSeleccionado;
            }
            else
            {
                rblOpcionesRetiro.SelectedIndex = 0;
            }
        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            CargarMetodosDeEnvioyRetiro();
            ActualizarTotal();
        }
        protected void rblOpcionesEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }
        protected void rblOpcionesRetiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        private decimal ObtenerCostoEnvio()
        {
            string codigoPostal = txtCodigoPostal.Text.Trim();
            MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();

            int idMetodoEnvioSeleccionado = Convert.ToInt32(rblOpcionesEnvio.SelectedValue);
            decimal totalEnvio = metodoDeEnvioNegocio.ObtenerCostoEnvio(codigoPostal, idMetodoEnvioSeleccionado);

            int idMetodoRetiroSeleccionado = Convert.ToInt32(rblOpcionesRetiro.SelectedValue);
            decimal totalRetiro = metodoDeEnvioNegocio.ObtenerCostoRetiro(codigoPostal, idMetodoRetiroSeleccionado);

            decimal total = totalRetiro + totalEnvio;
            return total;
        }
        private Direccion ObtenerDireccionDesdeFormulario()
        {
            return new Direccion
            {
                Usuario = (Usuario)Session["UsuarioActivo"],
                Calle = txtDireccion.Text,
                Altura = Convert.ToInt32(txtAltura.Text),
                CodigoPostal = txtCodigoPostal.Text.Trim(),
                Aclaracion = txtCiudad.Text,
                Predeterminada = chkGuardarPredeterminada.Checked
            };
        }
        protected void ActualizarTotal()
        {
            try
            {
                string codigoPostal = txtCodigoPostal.Text.Trim();
                MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();

                if (string.IsNullOrEmpty(codigoPostal) || codigoPostal.Length != 4 || !int.TryParse(codigoPostal, out _))
                {
                    lblTotal.Text = "0.00";
                    string script = "alert('Debe ingresar un código postal válido de 4 dígitos.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                }


                int idMetodoEnvioSeleccionado = Convert.ToInt32(rblOpcionesEnvio.SelectedValue);
                decimal totalEnvio = metodoDeEnvioNegocio.ObtenerCostoEnvio(codigoPostal, idMetodoEnvioSeleccionado);


                int idMetodoRetiroSeleccionado = Convert.ToInt32(rblOpcionesRetiro.SelectedValue);
                decimal totalRetiro = metodoDeEnvioNegocio.ObtenerCostoRetiro(codigoPostal, idMetodoRetiroSeleccionado);

                decimal total = totalRetiro + totalEnvio;

                lblTotal.Text = total.ToString("N2");

            }
            catch (Exception ex)
            {
                lblTotal.Text = "Error: " + ex.Message;
            }
        }

        private int GuardarDireccion(Direccion direccion)
        {
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            int idDireccion = direccionNegocio.ObtenerIdDireccion(direccion);
            if (idDireccion == 0)
            {
                idDireccion = direccionNegocio.AgregarYRetornarId(direccion);
            }
            return idDireccion;
        }
        private MetodoDeEnvio ObtenerMetodoDeEnvio()
        {
            int idMetodoEnvioSeleccionado = Convert.ToInt32(rblOpcionesEnvio.SelectedValue);
            MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();
            return metodoDeEnvioNegocio.ObtenerMetodoEnvioPorID(idMetodoEnvioSeleccionado);
        }
        private MetodoDeRetiro ObtenerMetodoDeRetiro()
        {
            int idMetodoRetiroSeleccionado = Convert.ToInt32(rblOpcionesRetiro.SelectedValue);
            MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();
            return metodoDeEnvioNegocio.ObtenerMetodoRetiroPorID(idMetodoRetiroSeleccionado);
        }
        private List<Libro> ObtenerLibrosDelCarrito(Usuario usuarioActivo)
        {
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            return carritoNegocio.ListarLibrosEnCarrito(usuarioActivo.IdUsuario);
        }
        //private void GuardarPrestamo(Usuario usuarioActivo, List<Libro> librosCarrito, MetodoDeEnvio metodoEnvio, MetodoDeRetiro metodoRetiro, int idDireccion)
        //{
        //    PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
        //    Prestamo prestamo = new Prestamo
        //    {
        //        Usuario = usuarioActivo,
        //        Libros = librosCarrito,
        //        FechaInicio = DateTime.Now,
        //        FechaFin = DateTime.Now.AddMonths(1),
        //        MetodoEnvio = metodoEnvio,
        //        MetodoRetiro = metodoRetiro,
        //        CostoEnvio = ObtenerCostoEnvio(),
        //        Estado = "Pendiente",
        //        Devuelto = false,
        //        Direccion = new Direccion { IdDireccion = idDireccion }
        //    };
        //    prestamoNegocio.GuardarPrestamo(prestamo);
        //    Session["PrestamoActual"] = prestamo;
        //    CarritoNegocio carritoNegocio = new CarritoNegocio();
        //    carritoNegocio.VaciarCarrito(usuarioActivo.IdUsuario);
        //    Redireccionar(prestamo);
        //}

        protected void btnRealizarPedido_Click(object sender, EventArgs e)
        {
            Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];

            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(rblMetodoPago.SelectedValue))
                {
                    string script = "alert('Debe seleccionar una forma de pago.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                }

                Direccion direccion = ObtenerDireccionDesdeFormulario();

                int idDireccion = GuardarDireccion(direccion);
                MetodoDeEnvio metodoEnvio = ObtenerMetodoDeEnvio();
                MetodoDeRetiro metodoRetiro = ObtenerMetodoDeRetiro();

                PrestamoNegocio prestamoNegocio = new PrestamoNegocio();

                Prestamo prestamo = new Prestamo
                {
                    Usuario = usuarioActivo,
                    FechaInicio = DateTime.Now,
                    FechaFin = DateTime.Now.AddMonths(1),
                    MetodoEnvio = metodoEnvio,
                    MetodoRetiro = metodoRetiro,
                    CostoEnvio = ObtenerCostoEnvio(),
                    Estado = "Pendiente",
                    Devuelto = false,
                    Direccion = new Direccion { IdDireccion = idDireccion }
                };

                int idPrestamo = prestamoNegocio.GuardarPrestamoYObtenerID(prestamo);

                List<Libro> librosCarrito = ObtenerLibrosDelCarrito(usuarioActivo);
                if (librosCarrito == null || librosCarrito.Count == 0)
                {
                    string script = "alert('El carrito está vacío. No se puede proceder con el pedido.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                }

                prestamoNegocio.GuardarLibrosDelPrestamo(idPrestamo, librosCarrito);
                Session["PrestamoActual"] = prestamo;

                Redireccionar(prestamo);
            }
            catch (Exception ex)
            {
                lblTotal.Text = "Error al procesar el pedido: " + ex.Message;
            }
        }

        private void Redireccionar(Prestamo prestamo)
        {
            if (rblMetodoPago.SelectedValue == "Tarjeta")
            {
                Response.Redirect("PagoPedido.aspx");
            }
            else
            {
                Response.Redirect("ConfirmacionPedido.aspx");
            }
        }
    }
}