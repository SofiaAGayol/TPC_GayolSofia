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
    public partial class ConfirmacionPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuarioActivo = (Usuario)Session["UsuarioActivo"];
                Prestamo prestamoActual = (Prestamo)Session["PrestamoActual"];

                if (usuarioActivo == null || prestamoActual == null)
                {
                    Response.Redirect("Home.aspx");
                    return;
                }

                CargarDatosPedido(usuarioActivo, prestamoActual);
            }
        }

        private void CargarDatosPedido(Usuario usuario, Prestamo prestamo)
        {
            lblIdPedido.Text = prestamo.IDPrestamo.ToString();
            lblNombreCliente.Text = $"{usuario.Nombre} {usuario.Apellido}";
            lblCorreoCliente.Text = usuario.Email;

            LibroNegocio libroNegocio = new LibroNegocio();
            List<Libro> librosPedido = libroNegocio.ListarLibrosPorPrestamo(prestamo.IDPrestamo);

            rptLibrosPedido.DataSource = librosPedido.Select(libro => new
            {
                Titulo = libro.Titulo,
                Autor = $"{libro.Autor.Nombre} {libro.Autor.Apellido}"
            });
            rptLibrosPedido.DataBind();

            lblFechaInicio.Text = prestamo.FechaInicio.ToString("dd/MM/yyyy");
            lblFechaFin.Text = prestamo.FechaFin.ToString("dd/MM/yyyy");
            lblEstado.Text = prestamo.Estado;

            lblMetodoEnvio.Text = prestamo.MetodoEnvio?.Descripcion ?? "No especificado";
            lblMetodoRetiro.Text = prestamo.MetodoRetiro?.Descripcion ?? "No especificado";

            if (prestamo.MetodoEnvio != null && prestamo.MetodoEnvio.IdMetodoEnvio != 0 && prestamo.Usuario != null)
            {
                DireccionNegocio direccionNegocio = new DireccionNegocio();
                Direccion direccion = direccionNegocio.ObtenerDireccionPorID(prestamo.Usuario.IdUsuario);
                if (direccion != null)
                {
                    lblDireccionEnvio.Text = $"{direccion.Calle} {direccion.Altura}, CP: {direccion.CodigoPostal}";
                }
                else
                {
                    lblDireccionEnvio.Text = "No especificada";
                }
            }

            lblCostoEnvio.Text = $"${prestamo.CostoEnvio:N2}";
            lblTotal.Text = $"${prestamo.CostoEnvio:N2}";
        }
    }
}