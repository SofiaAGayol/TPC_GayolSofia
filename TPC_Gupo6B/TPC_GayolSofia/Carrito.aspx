<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPC_GayolSofia.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Carrito de Compras</h2>
            <asp:Repeater ID="RepeaterCarrito" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Subtotal</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="ImageProducto" runat="server" Width="52px" Height="68px" ImageUrl='<%# Eval("Imagen") %>' />
                            <span><%# Eval("Titulo") %></span>
                        </td>
                        <td>$<%# Eval("Precio") %></td>
                        <td>
                            <div class="d-flex align-items-center">
                                <asp:Button ID="btnMenos" runat="server" Text="-" CommandArgument='<%# Eval("IDProducto") %>' OnClick="btnMenos_Click" />
                                <span class="mx-2"><%# Eval("Cantidad") %></span>
                                <asp:Button ID="btnMas" runat="server" Text="+" CommandArgument='<%# Eval("IDProducto") %>' OnClick="btnMas_Click" />
                            </div>
                        </td>
                        <td>$<%# Eval("Subtotal") %></td>
                        <td>
                            <asp:Button ID="btnEliminar" runat="server" Text="X" CommandArgument='<%# Eval("IDProducto") %>' OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
               
                </FooterTemplate>
            </asp:Repeater>

            <div class="mt-4 text-end">
                <h5>Cart Total</h5>
                <p>Subtotal: $<asp:Label ID="lblSubtotal" runat="server" Text="0.00"></asp:Label></p>
                <p>Shipping: $<asp:Label ID="lblShipping" runat="server" Text="5.00"></asp:Label></p>
                <h4>Total: $<asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label></h4>
                <%--<asp:Button ID="btnCheckout" runat="server" Text="Proceed To Checkout" CssClass="btn btn-success" OnClick="btnCheckout_Click" />--%>
            </div>
        </div>
    </form>
</asp:Content>
