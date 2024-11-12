﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_GayolSofia.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2>Detalles de Facturación</h2>
        <div class="row">

            <div class="col-md-8">
                <div id="formularioCheckout">
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server" CssClass="form-label" Text="Nombre*" AssociatedControlID="txtNombre"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblApellido" runat="server" CssClass="form-label" Text="Apellido*" AssociatedControlID="txtApellido"></asp:Label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblPais" runat="server" CssClass="form-label" Text="País*" AssociatedControlID="ddlPais"></asp:Label>
                        <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control" Required="True">
                            <asp:ListItem Text="Seleccione un País" Value="" />
                            <asp:ListItem Text="Argentina" Value="Argentina" />
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblDireccion" runat="server" CssClass="form-label" Text="Dirección*" AssociatedControlID="txtDireccion"></asp:Label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblCiudad" runat="server" CssClass="form-label" Text="Ciudad*" AssociatedControlID="txtCiudad"></asp:Label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblTelefono" runat="server" CssClass="form-label" Text="Teléfono*" AssociatedControlID="txtTelefono"></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Teléfono" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblCorreoElectronico" runat="server" CssClass="form-label" Text="Correo Electrónico*" AssociatedControlID="txtCorreoElectronico"></asp:Label>
                        <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="form-control" placeholder="Correo Electrónico" TextMode="Email" Required="True"></asp:TextBox>
                    </div>
                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkGuardarParaProximoPago" runat="server" CssClass="form-check-input" />
                        <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="chkGuardarParaProximoPago" Text="Guardar para mi próximo pago"></asp:Label>
                    </div>
                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkEnviarADireccionDiferente" runat="server" CssClass="form-check-input" />
                        <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="chkEnviarADireccionDiferente" Text="¿Enviar a una dirección diferente?"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <h4>Su Pedido</h4>
                <div class="card">
                    <div class="card-body">
                        <p><strong>Producto</strong> <span class="float-end">Subtotal:
                            <asp:Label ID="lblTotalLibros" runat="server" Text="0"></asp:Label>
                        </span></p>
                        <hr />
                        <asp:Repeater ID="rptLibrosCarrito" runat="server">
                            <ItemTemplate>
                                <p><%# Eval("Titulo") %> - <%# Eval("Autor") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                        <hr />
                        <p>Envío:</p>
                        <asp:RadioButtonList ID="rblOpcionesEnvio" runat="server" CssClass="form-check">
                        </asp:RadioButtonList>
                        <hr />
                        <p>Total: <strong>$<asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></strong></p>
                    </div>
                </div>
                <div class="mt-3">
                    <h5>Método de Pago</h5>
                    <asp:RadioButtonList ID="rblMetodoPago" runat="server" CssClass="form-check">
                        <asp:ListItem Text="Transferencia Bancaria Directa" Value="TransferenciaBancaria" />
                        <asp:ListItem Text="Contra Reembolso" Value="ContraReembolso" />
                        <asp:ListItem Text="Paypal" Value="Paypal" />
                    </asp:RadioButtonList>
                </div>
                <div class="mt-3">
                    <asp:Button ID="btnRealizarPedido" runat="server" Text="Realizar Pedido" CssClass="btn btn-primary w-100" OnClick="btnRealizarPedido_Click" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>