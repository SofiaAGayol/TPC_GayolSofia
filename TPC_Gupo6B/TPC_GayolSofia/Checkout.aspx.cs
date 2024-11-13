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
            List<Direccion> direcciones = direccionNegocio.ListarPorUsuario(usuarioActivo.IdUsuario);
            if (direcciones.Any(d => d.Predeterminada))
            {
                Direccion predeterminada = direcciones.First(d => d.Predeterminada);
                txtDireccion.Text = $"{predeterminada.Calle} {predeterminada.Altura}";
                ddlPais.SelectedValue = "Argentina";
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
            MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();

            List<MetodoDeEnvio> metodosDeEnvio = metodoDeEnvioNegocio.ListarTodos();

            rblOpcionesEnvio.DataSource = metodosDeEnvio;
            rblOpcionesEnvio.DataTextField = "Descripcion";
            rblOpcionesEnvio.DataValueField = "IdMetodoEnvio";
            rblOpcionesEnvio.DataBind();

            List<MetodoDeRetiro> metodosDeRetiro = metodoDeEnvioNegocio.ListarTodosRetiro();

            rblOpcionesRetiro.DataSource = metodosDeRetiro;
            rblOpcionesRetiro.DataTextField = "Descripcion";
            rblOpcionesRetiro.DataValueField = "IdMetodoRetiro";
            rblOpcionesRetiro.DataBind();
        }

        protected void rblOpcionesEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        protected void rblOpcionesRetiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        protected void ActualizarTotal()
        {
            try
            {
                string codigoPostal = txtCodigoPostal.Text.Trim();

                if (string.IsNullOrEmpty(codigoPostal) || codigoPostal.Length != 4 || !int.TryParse(codigoPostal, out _))
                {
                    lblTotal.Text = "0.00";
                    string script = "alert('Debe ingresar un código postal válido de 4 dígitos.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    return;
                }

                MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();

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
                int idMetodoEnvioSeleccionado = Convert.ToInt32(rblOpcionesEnvio.SelectedValue);
                MetodoDeEnvioNegocio metodoDeEnvioNegocio = new MetodoDeEnvioNegocio();
                MetodoDeEnvio metodoEnvio = metodoDeEnvioNegocio.ObtenerMetodoEnvioPorID(idMetodoEnvioSeleccionado);

                Prestamo prestamo = new Prestamo
                {
                    Usuario = usuarioActivo,
                    FechaInicio = DateTime.Now,
                    FechaFin = DateTime.Now.AddMonths(1),
                    MetodoEnvio = ObtenerMetodoEnvioSeleccionado(),
                    //MetodoRetiro = ObtenerMetodoRetiroSeleccionado(),
                    CostoEnvio = ObtenerCostoEnvio(),
                    Estado = "Pendiente",
                    Devuelto = false
                };

                PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
                //prestamoNegocio.Crear(prestamo);

                // Si la dirección no existe, guardarla en la base de datos
                if (chkGuardarParaProximoPago.Checked)
                {
                    GuardarDireccion(usuarioActivo);
                }

                Response.Redirect("ConfirmacionPedido.aspx");
            }
            catch (Exception ex)
            {
                lblTotal.Text = "Error al procesar el pedido: " + ex.Message;
            }
        }

        private MetodoDeEnvio ObtenerMetodoEnvioSeleccionado()
        {
            int idMetodoEnvio = Convert.ToInt32(rblOpcionesEnvio.SelectedValue);
            MetodoDeEnvioNegocio metodoEnvioNegocio = new MetodoDeEnvioNegocio();
            return metodoEnvioNegocio.ObtenerMetodoEnvioPorID(idMetodoEnvio);
        }


        private decimal ObtenerCostoEnvio()
        {
            return Convert.ToDecimal(rblOpcionesEnvio.SelectedValue);
        }

        private void GuardarDireccion(Usuario usuario)
        {
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            Direccion nuevaDireccion = new Direccion
            {
                Usuario = usuario,
                Calle = txtDireccion.Text,
                Altura = Convert.ToInt32(txtCiudad.Text), // Aquí sería más específico, se debe manejar de acuerdo a los datos que se tenga
                CodigoPostal = "AMBA", // Esto se tiene que obtener desde la dirección ingresada
                Aclaracion = "Direccion guardada en el checkout",
                Predeterminada = true
            };
            direccionNegocio.Agregar(nuevaDireccion);
        }
    }
}