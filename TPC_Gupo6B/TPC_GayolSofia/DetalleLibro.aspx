<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleLibro.aspx.cs" Inherits="TPC_GayolSofia.DetalleLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container m-2">

        <div class="row">
            <div class="col-6 px-5">
                <asp:Image ID="imgPrincipal" alt="Imagen del libro" runat="server" CssClass="img-large w-100" ImageUrl='<%# Eval("Imagen") %>' />
            </div>

            <div class="card col-md-6 h-75">
                <div class="card-body">
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

                    <div class="mt-3">
                        <asp:Button ID="btnPedirAhora" runat="server" Text="Solicitar libro" CssClass="btn btn-primary btn-lg" OnClick="Solicitar_Click" />
                        <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al carrito" CssClass="btn btn-outline-primary btn-lg" OnClick="AgregarCarrito_Click" />
                    </div>
                    <div id="divAlert" runat="server" style="display: none; border: 1px solid #d1e7dd; background-color: #d4edda; color: #155724; padding: 10px; margin-top: 20px; border-radius: 5px;">
                        <span id="alertMessage" runat="server"></span>
                    </div>

                    <div class="mt-3">
                        <asp:Label ID="lblEnvioGratis" runat="server" Text="Envío gratis reservando 3 libros o mas" CssClass="text-muted" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-5">
            <h4>Otros productos similares</h4>
            <asp:Repeater ID="rptProductosSimilares" runat="server">
                <ItemTemplate>
                    <div class="col-md-3">
                        <div class="card">
                            <asp:Image ID="imgProductoSimilar" runat="server" CssClass="card-img-top h-100" ImageUrl='<%# Eval("Imagen") %>' />
                            <div class="card-body">
                                <h6 class="card-title">
                                    <asp:Label ID="lblTituloSimilar" runat="server" Text='<%# Eval("Titulo") %>' /></h6>
                                <p class="card-text text-success">
                                    <asp:Label ID="lblAutorSimilar" runat="server" Text='<%# Eval("Autor") %>' />
                                </p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <!-- Mismo autor -->
        <div class="row mt-5">
            <h4>
                <asp:Label ID="lblMismoAutor" runat="server" Text="Otros titulos del autor" CssClass="fw-bold" /></h4>
            <asp:Repeater ID="rptMismoAutor" runat="server">
                <ItemTemplate>
                    <div class="col-md-3">
                        <div class="card">
                            <asp:Image ID="imgMismoAutor" runat="server" CssClass="card-img-top h-100" ImageUrl='<%# Eval("Imagen") %>' />
                            <div class="card-body">
                                <h6 class="card-title">
                                    <asp:Label ID="lblTituloAutor" runat="server" Text='<%# Eval("Titulo") %>' /></h6>
                                <p class="card-text text-success">
                                    <asp:Label ID="lblCategoriaAutor" runat="server" Text='<%# Eval("Categoria") %>' />
                                </p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>



</asp:Content>
