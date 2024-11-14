<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionPedido.aspx.cs" Inherits="TPC_GayolSofia.ConfirmacionPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
            <h2>Confirmación de Pedido</h2>
            <div class="card mt-3">
                <div class="card-body">
                    <h4>Detalles del Pedido</h4>
                    <hr />

                    <p><strong>ID de Pedido:</strong> <asp:Label ID="lblIdPedido" runat="server"></asp:Label></p>
                    <p><strong>Nombre del Cliente:</strong> <asp:Label ID="lblNombreCliente" runat="server"></asp:Label></p>
                    <p><strong>Correo Electrónico:</strong> <asp:Label ID="lblCorreoCliente" runat="server"></asp:Label></p>

                    <h4>Detalles del Préstamo</h4>
                    <p><strong>Libros Solicitados:</strong></p>
                    <ul>
                        <asp:Repeater ID="rptLibrosPedido" runat="server">
                            <ItemTemplate>
                                <li>
                                    <%# Eval("Titulo") %> - <%# Eval("Autor") %>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                    <p><strong>Fecha de Inicio del Préstamo:</strong> <asp:Label ID="lblFechaInicio" runat="server"></asp:Label></p>
                    <p><strong>Fecha de Devolución:</strong> <asp:Label ID="lblFechaFin" runat="server"></asp:Label></p>
                    <p><strong>Estado del Pedido:</strong> <asp:Label ID="lblEstado" runat="server"></asp:Label></p>

                    <h4>Método de Envío y Retiro</h4>
                    <p><strong>Método de Envío:</strong> <asp:Label ID="lblMetodoEnvio" runat="server"></asp:Label></p>
                    <p><strong>Método de Retiro:</strong> <asp:Label ID="lblMetodoRetiro" runat="server"></asp:Label></p>
                    <p><strong>Dirección de Envío:</strong> <asp:Label ID="lblDireccionEnvio" runat="server"></asp:Label></p>

                    <h4>Resumen del Costo</h4>
                    <p><strong>Costo de Envío:</strong> <asp:Label ID="lblCostoEnvio" runat="server"></asp:Label></p>
                    <p><strong>Total:</strong> <asp:Label ID="lblTotal" runat="server"></asp:Label></p>

                    <div class="mt-4">
                        <a href="SeguimientoPedido.aspx?pedidoId=<%# lblIdPedido.Text %>" class="btn btn-primary">Seguir Pedido</a>
                        <a href="Home.aspx" class="btn btn-secondary">Volver a la Tienda</a>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
