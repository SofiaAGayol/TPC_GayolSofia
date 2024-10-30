<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="TPC_GayolSofia.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 60px;">
        <div class="container">
            <div class="navbar-nav">
                <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
                <a class="nav-link" href="Libros.aspx">Libros</a>
            </div>
        </div>
    </nav>

    <div class="container mt-4">
        <!-- Resumen de informes -->
        <div class="row text-white">
            <div class="col-md-3 mb-4">
                <div class="card bg-primary p-3">
                    <h4>982</h4>
                    <p>Clientes activos</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card bg-danger p-3">
                    <h4>45</h4>
                    <p>Libros en lectura</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card bg-success p-3">
                    <h4>73</h4>
                    <p>Libros disponibles</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="card bg-warning p-3">
                    <h4>168</h4>
                    <p>Paquetes activos</p>
                </div>
            </div>
        </div>

        <!-- Balance de interes -->
        <div class="card bg-dark text-white mb-4 p-3">
            <h5>Balance mensual: total de libros leidos y devueltos</h5>
            <h3>$1,500.00</h3>
            <p>Tuesday, February 2nd 2024, 9:24 AM</p>
            <button class="btn btn-primary">Ver informes completos</button>
        </div>

        <!-- Libros en prestamo -->
        <h4>Libros en prestamo</h4>
        <div class="table-responsive">
            <table class="table table-dark table-bordered">
                <thead>
                    <tr>
                        <th>Libro</th>
                        <th>Autor</th>
                        <th>Fecha solicitud</th>
                        <th>Fecha vencimiento</th>
                        <th>Estado</th>
                        <th>Notificar</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Libro 1</td>
                        <td>Autor 1</td>
                        <td>Thu 1/7/2021</td>
                        <td>Thu 20/7/2021</td>
                        <td><span class="badge bg-success">Paid</span></td>
                        <td>
                            <button class="btn btn-outline-light btn-sm">Activo</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Busqueda -->
        <div class="card bg-dark text-white p-3">
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
    </div>



    <asp:Label ID="Saludo" runat="server" Text=""></asp:Label>
</asp:Content>
