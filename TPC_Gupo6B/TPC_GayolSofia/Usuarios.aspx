<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_GayolSofia.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 40px;">
    <div class="container">
        <div class="navbar-nav">
            <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
            <a class="nav-link" href="Libros.aspx">Libros</a>
        </div>
    </div>
</nav>  

<div class="row justify-content-center mb-5 mt-5">
    <div class="col-12 text-center">
        <asp:GridView ID="Dgv_Usuarios" runat="server" CssClass="mx-auto"></asp:GridView>
    </div>
</div>
    


</asp:Content>
