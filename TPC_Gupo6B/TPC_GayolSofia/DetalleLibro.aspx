<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleLibro.aspx.cs" Inherits="TPC_GayolSofia.DetalleLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container m-6">
        <!-- Imagen Principal -->
        <div class="row">
            <div class="col-md-6">
                <asp:Image ID="imgPrincipal" alt="Imagen del libro" runat="server" CssClass="img-fluid mb-3" ImageUrl='<%# Eval("Imagen") %>' />
            </div>

            <!-- Info del Libro -->
            <div class="col-md-6">
                <h3>
                    <asp:Label ID="lblTitulo" runat="server" Text="Palabras Semilla - Magela Demarco / Caru Grossi" CssClass="fw-bold" /></h3>
                <p>
                    <asp:Label ID="lblAutor" runat="server" Text='<%# Eval("Autor")%>' CssClass="h4 text-success" />
                </p>
                <p>
                    <asp:Label ID="lblCat" runat="server" Text='<%# Eval("Categoria")%>' CssClass="text-muted" />
                </p>
                <div>
                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>' CssClass="text-success fw-bold" />
                    <br />
                </div>

                <!-- Botones de Compra -->
                <div class="mt-3">
                    <asp:Button ID="btnComprarAhora" runat="server" Text="Comprar ahora" CssClass="btn btn-primary btn-lg mb-2" OnClick="ComprarAhora_Click" />
                    <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al carrito" CssClass="btn btn-outline-primary btn-lg" OnClick="AgregarCarrito_Click" />
                </div>

                <!-- Condiciones de Envío Gratis -->
                <div class="mt-3">
                    <asp:Label ID="lblEnvioGratis" runat="server" Text="Envío gratis en compras a partir de $30,000" CssClass="text-muted" />
                </div>
            </div>
        </div>

        <!-- Otros Productos Similares -->
        <div class="row mt-5">
            <h4>Otros productos similares</h4>
            <asp:Repeater ID="rptProductosSimilares" runat="server">
                <ItemTemplate>
                    <div class="col-md-3">
                        <div class="card">
                            <asp:Image ID="imgProductoSimilar" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("ImagenUrl") %>' />
                            <div class="card-body">
                                <h6 class="card-title">
                                    <asp:Label ID="lblTituloSimilar" runat="server" Text='<%# Eval("Titulo") %>' /></h6>
                                <p class="card-text text-success">
                                    <asp:Label ID="lblPrecioSimilar" runat="server" Text='<%# Eval("Precio") %>' />
                                </p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>



</asp:Content>
