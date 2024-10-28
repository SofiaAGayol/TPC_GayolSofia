<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarUsuario.aspx.cs" Inherits="TPC_GayolSofia.AgregarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container mt-5 mb-5">
        <div class="card mx-auto" style="max-width: 500px;">
            <div class="card-body">
                <h5 class="card-title text-center mb-2">Agregar Usuario</h5>
                <div class="form-group text-center">
                    <label for="ddlRol">Rol</label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control w-75 mx-auto">
                        <asp:ListItem Text="Seleccione un rol" Value="" />
                        <asp:ListItem Text="Administrador" Value="1" />
                        <asp:ListItem Text="Usuario" Value="2" />
                        <asp:ListItem Text="Moderador" Value="3" />
                    </asp:DropDownList>
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtUsuario">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtClave">Clave</label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control w-75 mx-auto" TextMode="Password" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtNombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtApellido">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtDNI">DNI</label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control w-75 mx-auto" />
                </div> 
                <div class="form-group text-center mt-2">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control w-75 mx-auto" TextMode="Email" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtTelefono">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>