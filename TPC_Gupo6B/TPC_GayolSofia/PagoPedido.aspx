<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagoPedido.aspx.cs" Inherits="TPC_GayolSofia.PagoPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
            <h2>Pagar Pedido</h2>
            <asp:Panel ID="panelDetallesPedido" runat="server" CssClass="mt-3">
                <h4>Detalles del Pedido</h4>
                <p><strong>Producto(s): </strong><asp:Label ID="lblProductos" runat="server" /></p>
                <p><strong>Importe Total: </strong>$<asp:Label ID="lblImporteTotal" runat="server" /></p>
            </asp:Panel>

            <asp:Panel ID="panelMetodosPago" runat="server" CssClass="mt-3">
                <h4>Método de Pago</h4>
                <asp:Repeater ID="rptMetodosPago" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="card-body">
                                <asp:RadioButton ID="rdoMetodoPago" runat="server" GroupName="MetodoPago"
                                    Text='<%# Eval("TipoTarjeta") + " - " + Eval("NroTarjeta") %>' CssClass="form-check-input" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div class="form-check mb-3">
                    <asp:CheckBox ID="chkNuevoMetodoPago" runat="server" CssClass="form-check-input" OnCheckedChanged="chkNuevoMetodoPago_CheckedChanged" AutoPostBack="true" />
                    <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="chkNuevoMetodoPago" Text="Ingresar nuevo método de pago"></asp:Label>
                </div>
            </asp:Panel>

            <asp:Panel ID="panelNuevoMetodoPago" runat="server" CssClass="mt-3" Visible="false">
                <h4>Nuevo Método de Pago</h4>
                <div class="mb-3">
                    <asp:Label ID="lblTipoTarjeta" runat="server" Text="Tipo de Tarjeta" AssociatedControlID="ddlTipoTarjeta" />
                    <asp:DropDownList ID="ddlTipoTarjeta" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Visa" Value="Visa" />
                        <asp:ListItem Text="MasterCard" Value="MasterCard" />
                        <asp:ListItem Text="Amex" Value="Amex" />
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblNroTarjeta" runat="server" Text="Número de Tarjeta" AssociatedControlID="txtNroTarjeta" />
                    <asp:TextBox ID="txtNroTarjeta" runat="server" CssClass="form-control" MaxLength="16" />
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblVencimiento" runat="server" Text="Fecha de Vencimiento (MM/AA)" AssociatedControlID="txtVencimiento" />
                    <asp:TextBox ID="txtVencimiento" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblCodSeguridad" runat="server" Text="Código de Seguridad" AssociatedControlID="txtCodSeguridad" />
                    <asp:TextBox ID="txtCodSeguridad" runat="server" CssClass="form-control" MaxLength="3" />
                </div>
            </asp:Panel>

            <asp:Button ID="btnPagar" runat="server" Text="Pagar" CssClass="btn btn-primary mt-3" OnClick="btnPagar_Click" />
        </div>
</asp:Content>
