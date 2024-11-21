<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="TPC_GayolSofia.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 40px;">
        <div class="container">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="Informes.aspx">Reportes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link " href="Usuarios.aspx">Usuarios</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link " href="Libros.aspx">Libros</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Autores.aspx">Autores</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="container mt-4">
        <!-- Resumen de informes -->
        <div class="row text-white heigh d-flex align-items-stretch">
            <div class="col-md-3 mb-4 h-100">
                <div class="card bg-primary h-100 p-3">
                    <h4>
                        <asp:Label ID="lblCantidadClientesActivos" runat="server" Text="0"></asp:Label>
                    </h4>
                    <p>
                        Clientes activos
                       
                        <br>
                    </p>
                </div>
            </div>
            <div class="col-md-3 mb-4 h-100">
                <div class="card bg-danger p-3">
                    <h4>
                        <asp:Label ID="lblCantidadLibrosPrestamo" runat="server" Text="0"></asp:Label>
                    </h4>
                    <p>Libros en lectura</p>
                </div>
            </div>
            <div class="col-md-3 mb-4 h-100">
                <div class="card bg-success p-3">
                    <h4>
                        <asp:Label ID="lblCantidadLibrosDisponibles" runat="server"></asp:Label>
                    </h4>
                    <p>Titulos disponibles</p>
                </div>
            </div>
            <div class="col-md-3 mb-4 h-100">
                <div class="card bg-warning p-3 h-100">
                    <h4>
                        <asp:Label ID="lblStock" runat="server"></asp:Label>
                    </h4>
                    <p>Libros en Stock</p>
                </div>
            </div>
        </div>

        <!-- Balance de interes -->
        <div class="card bg-dark text-white mb-4 p-3">
            <h5>Balance: libros leidos y devueltos</h5>
            <h3>$1,500.00</h3>
            <p>Fecha desde</p>
            <p>Fecha hasta</p>
        </div>
        <div class="card bg-dark text-white mb-4 p-3">
            <h5>Categorias principales</h5>
            <h3>$1,500.00</h3>
            <p>Total Reservas</p>
        </div>

        <!-- Libros en prestamo -->
        <h4>Libros en prestamo</h4>
        <asp:GridView ID="gvLibrosEnPrestamo" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Id Usuario">
                    <ItemTemplate>
                        <%# Eval("Prestamo.Usuario.IdUsuario") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Usuario">
                    <ItemTemplate>
                        <%# Eval("Prestamo.Usuario.Nombre") %> <%# Eval("Prestamo.Usuario.Apellido") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Usuario" />
                <asp:BoundField DataField="Titulo" HeaderText="Libro" />
                <asp:BoundField DataField="Autor" HeaderText="Autor" />
                <asp:TemplateField HeaderText="Devolución">
                    <ItemTemplate>
                        <asp:Button ID="btnGenerarDevolucion" runat="server" Text="Generar Devolución" CssClass="btn btn-outline-light btn-sm" CommandName="DevolverLibro" CommandArgument='<%# Eval("Prestamo.IDPrestamo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Busqueda -->
        <%--<div class="card bg-dark text-white p-3">
            <h5>Buscar Libros</h5>
            <div class="mb-3">
                <label for="recipient" class="form-label">Titulo</label>
                <select id="recipient" class="form-select">
                    <option>Libro 1</option>
                    <option>Ismael</option>
                    <option>Dinda</option>
                </select>
            </div>
            <div class="mb-3">
                <label for="service" class="form-label">Autor</label>
                <select id="service" class="form-select">
                    <option>Autor 1</option>
                </select>
            </div>
            <div class="mb-3">
                <label for="amount" class="form-label">Busqueda rapida</label>
                <input type="text" class="form-control" id="amount" placeholder="Ingrese su busqueda">
            </div>
            <button class="btn btn-primary w-100">Buscar</button>
        </div>
    </div>--%>



        <asp:Label ID="Saludo" runat="server" Text=""></asp:Label>
</asp:Content>
