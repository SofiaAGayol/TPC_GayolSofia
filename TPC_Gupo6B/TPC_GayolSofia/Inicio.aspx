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


    <asp:Label ID="Saludo" runat="server" Text=""></asp:Label>
</asp:Content>
