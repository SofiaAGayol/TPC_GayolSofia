﻿using dominio;
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
    public partial class PagoPedido : Page
    {
        private Usuario usuarioActivo;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioActivo = (Usuario)Session["UsuarioActivo"];
            if (usuarioActivo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDetallesPedido();
                CargarMetodosPagoExistentes();
            }
        }

        private void CargarDetallesPedido()
        {
            Prestamo prestamo = (Prestamo)Session["PrestamoActual"];
            if (prestamo != null)
            {
                string detallesLibros = string.Empty;
                foreach (var libro in prestamo.Libros)
                {
                    detallesLibros += $"{libro.Titulo} - {libro.Autor.Nombre} {libro.Autor.Apellido}<br/>";
                }
                lblProductos.Text = detallesLibros;
                lblImporteTotal.Text = prestamo.CostoEnvio.ToString("N2");
            }
        }

        private void CargarMetodosPagoExistentes()
        {
            MetodoDePagoNegocio metodoDePagoNegocio = new MetodoDePagoNegocio();
            List<MetodoPago> metodosPago = metodoDePagoNegocio.ListarPorUsuario(usuarioActivo.IdUsuario);

            if (metodosPago != null && metodosPago.Count > 0)
            {
                rptMetodosPago.DataSource = metodosPago;
                rptMetodosPago.DataBind();
            }
        }

        protected void chkNuevoMetodoPago_CheckedChanged(object sender, EventArgs e)
        {
            panelNuevoMetodoPago.Visible = chkNuevoMetodoPago.Checked;
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                Prestamo prestamo = (Prestamo)Session["PrestamoActual"];
                int idPrestamo = prestamo.IDPrestamo;

                if (chkNuevoMetodoPago.Checked)
                {
                    MetodoPago nuevoMetodoPago = new MetodoPago
                    {
                        Usuario = usuarioActivo,
                        TipoTarjeta = ddlTipoTarjeta.SelectedValue,
                        NroTarjeta = txtNroTarjeta.Text,
                        Vencimiento = DateTime.ParseExact(txtVencimiento.Text, "MM/yy", null),
                        Cod = txtCodSeguridad.Text
                    };

                    MetodoDePagoNegocio metodoDePagoNegocio = new MetodoDePagoNegocio();
                    metodoDePagoNegocio.Guardar(nuevoMetodoPago);
                }

                PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
                string nuevoEstado = "Pagado";
                prestamoNegocio.ActualizarEstadoPrestamo(idPrestamo, false, nuevoEstado);

                Response.Redirect("ConfirmacionPedido.aspx");
            }
            catch (Exception ex)
            {
                lblImporteTotal.Text = "Error al procesar el pago: " + ex.Message;
            }
        }
    }
}