<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPC_GayolSofia.MainCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <%--Menu de navecgacion--%>
    <nav class="navbar navbar-dark bg-transparent fixed-top">
        <div class="container-fluid">
            <%--<a class="navbar-brand" href="#">Mi cuenta</a>--%>
            <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar" aria-controls="offcanvasNavbar" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="offcanvas offcanvas-start text-bg-dark" tabindex="-1" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
                <div class="offcanvas-header">
                    <h5 class="offcanvas-title" id="offcanvasNavbarLabel">Nombre Usuario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <ul class="navbar-nav justify-content-start flex-grow-1 pe-3">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="#">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Mi estanteria</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Mi Perfil
                            </a>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="#">Historial</a></li>
                                <li><a class="dropdown-item" href="#">Ajustes</a></li>
                                <li><a class="dropdown-item" href="#">Cuenta</a></li>
                                <li>
                                    <hr class="dropdown-divider">
                                </li>
                                <li><a class="dropdown-item" href="#">Mi Membresia</a></li>
                            </ul>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Ayuda</a>
                        </li>
                    </ul>
                    <form class="d-flex" role="search">
                        <input class="form-control my-2" type="search" placeholder="Search" aria-label="Search">
                        <button class="btn btn-outline-success" type="submit">Buscar</button>
                    </form>
                </div>
            </div>
        </div>
    </nav>

    <%-- Busqueda y filtros --%>
    <nav class="navbar bg-dark navbar-expand-lg bg-body-tertiary mb-2" data-bs-theme="dark">
        <div class="container-fluid">
            <button class="navbar-toggler btn-close-white" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Categorías</a>
                        <ul class="dropdown-menu">
                            <asp:Repeater ID="RepeaterCategorias" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="LinkButtonCategoria" runat="server" CommandArgument='<%# Eval("IdCategoria") %>' OnClick="LinkButtonCategoria_Click" CssClass="dropdown-item">
                                            <%# Eval("Descripcion") %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Autores</a>
                        <ul class="dropdown-menu">
                            <asp:Repeater ID="RepeaterAutores" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="LinkButtonAutor" runat="server" CommandArgument='<%# Eval("IdAutor") %>' OnClick="LinkButtonAutor_Click" CssClass="dropdown-item">
                                            <%# Eval("Nombre") + " " + Eval("Apellido") %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButtonTodos" runat="server" OnClick="LinkButtonTodos_Click" CssClass="nav-link text-white">
                            Ver Todos
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>

            <form class="d-flex" role="search">
                <asp:TextBox class="form-control me-2" type="search" placeholder="Buscar libro" aria-label="Buscar" ID="filtro" runat="server" AutoPostBack="true" OnTextChanged="filtro_TextChanged" TextMode="Search" />
                <button class="btn btn-outline-light" type="submit">Buscar</button>
            </form>
        </div>

    </nav>

    <asp:DropDownList ID="ddlSortBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" CssClass="form-select form-select-sm my-2">
        <asp:ListItem Text="Ordenar por Autor Asc" Value="AutorAsc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Autor Desc" Value="AutorDesc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Nombre Asc" Value="NombreAsc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Nombre Desc" Value="NombreDesc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Categoría Asc" Value="CategoriaAsc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Categoría Desc" Value="CategoriaDesc"></asp:ListItem>
        <%--<asp:ListItem Text="Ordenar por Relevancia Asc" Value="RelevanciaAsc"></asp:ListItem>
        <asp:ListItem Text="Ordenar por Relevancia Desc" Value="RelevanciaDesc"></asp:ListItem>--%>
    </asp:DropDownList>


    <%--No hay libros--%>
    <asp:Panel ID="PanelNoLibros" runat="server" Visible="false" CssClass="alert alert-info text-center mt-3">
        <i class="bi bi-info-circle-fill"></i>
        No hay libros para mostrar
   
    </asp:Panel>

    <%--Libros--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-4 g-3">

                <asp:Repeater ID="RepeaterArticulos" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card h-100">
                                <asp:Image ID="Image1" alt="Imagen del libro" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("Imagen") %>' />
                                <div class="card-body h-50  justify-content-end">
                                    <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                    <h6 class="card-text"><%# Eval("Autor.Nombre") + " " + Eval("Autor.Apellido")%></h6>
                                    <p class="card-title"><%# Eval("Categoria.Descripcion") %></p>
                                </div>
                                <div class="d-grid gap-1 d-flex justify-content-evenly mb-3 mx-3">
                                    <asp:Button ID="botonDetalles" class="btn btn-primary flex-fill" runat="server" Text="Detalles" CommandArgument='<%# Eval("IDLibro") %>' OnClick="botonDetalles_Click" />
                                    <asp:Button ID="botonModificar" class="btn btn-secondary flex-fill" runat="server" Text="Detalles" CommandArgument='<%# Eval("IDLibro") %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

