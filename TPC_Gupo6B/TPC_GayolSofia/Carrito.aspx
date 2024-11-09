<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPC_GayolSofia.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Carrito de Compras</h2>

        <asp:Panel ID="PanelNoLibros" runat="server" Visible="false" CssClass="alert alert-info text-center mt-3">
            <i class="bi bi-info-circle-fill"></i>
            No hay libros en el carrito.
       
        </asp:Panel>

        <asp:Repeater ID="RepeaterCarrito" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <div class="row g-0">

                            <!-- Columna para la imagen -->
                            <div class="col-md-4">
                                <asp:Image ID="ImageLibro" runat="server" CssClass="img-fluid rounded-start" ImageUrl='<%# Eval("Imagen") %>' />
                            </div>

                            <!-- Columna para la información del libro -->
                            <div class="col-md-8">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                    <p class="card-text text-muted"><%# Eval("Autor.Nombre") %> <%# Eval("Autor.Apellido") %></p>
                                    <div class="mt-auto">
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("IdLibro") %>' OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="mt-4 text-end">
            <h5>Resumen del Carrito</h5>
            <p>Total de libros: <asp:Label ID="lblTotalLibros" runat="server" Text="0"></asp:Label></p>
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-secondary" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnCheckout" runat="server" Text="Realizar pedido" CssClass="btn btn-success" OnClick="btnCheckout_Click" />
        </div>
    </div>
</asp:Content>
