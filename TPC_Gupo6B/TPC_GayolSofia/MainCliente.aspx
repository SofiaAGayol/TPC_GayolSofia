﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainCliente.aspx.cs" Inherits="TPC_GayolSofia.MainCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Busqueda y filtros --%>
    <nav class="navbar bg-dark navbar-expand-lg bg-body-tertiary my-2" data-bs-theme="dark">
        <div class="container-fluid">
            <button class="navbar-toggler btn-close-white" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Categorías</a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Ficción</a></li>
                            <li><a class="dropdown-item" href="#">No Ficción</a></li>
                            <li><a class="dropdown-item" href="#">Infantil</a></li>
                            <li><a class="dropdown-item" href="#">Ciencia</a></li>
                        </ul>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Autores</a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Ficción</a></li>
                            <li><a class="dropdown-item" href="#">No Ficción</a></li>
                            <li><a class="dropdown-item" href="#">Infantil</a></li>
                            <li><a class="dropdown-item" href="#">Ciencia</a></li>
                        </ul>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Año</a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Ficción</a></li>
                            <li><a class="dropdown-item" href="#">No Ficción</a></li>
                            <li><a class="dropdown-item" href="#">Infantil</a></li>
                            <li><a class="dropdown-item" href="#">Ciencia</a></li>
                        </ul>
                    </li>
                </ul>
            </div>

            <form class="d-flex" role="search">
                <input class="form-control me-2" type="search" placeholder="Buscar libro" aria-label="Buscar">
                <button class="btn btn-outline-light" type="submit">Buscar</button>
            </form>
        </div>
    </nav>


    <%--Grid Libros--%>
    <div class="row row-cols-1 row-cols-md-4 g-4">

        <asp:Repeater ID="RepeaterArticulos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100">
                        <asp:Image ID="Image1" alt="Imagen del libro" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("Imagen") %>' />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text"><%# Eval("Autor.Nombre") + " " + Eval("Autor.Apellido")%></p>
                            <asp:Button ID="botonElegir" class="btn btn-primary" runat="server" Text="¡Ver Mas!" CommandArgument='<%# Eval("IDLibro") %>' OnClick="botonElegir_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>

