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
<asp:GridView ID="Dgv_Usuarios" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover mx-auto">
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="IdUsuario" />
        <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
        <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
        <asp:BoundField HeaderText="DNI" DataField="DNI" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
<asp:TemplateField HeaderText="Rol">
    <ItemTemplate>
        <%# ObtenerNombreRol(Convert.ToInt32(Eval("IDRol"))) %>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
</div>
    


</asp:Content>
