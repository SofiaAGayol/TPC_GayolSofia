<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarUsuario.aspx.cs" Inherits="TPC_GayolSofia.AgregarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container mt-5 mb-5">
        <div class="card mx-auto" style="max-width: 500px;">
            <div class="card-body">
                <h5 class="card-title text-center mb-2">Agregar Usuario</h5>
                <div class="form-group text-center">
                    <label for="ddlRol">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control w-75 mx-auto">
                        </asp:DropDownList>
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtUsuario">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="form-group text-center">
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
                    <asp:Button ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" />
                </div>
            </div>
        </div>
    </div>
    <div class="mx-auto justify-content-center mt-5 alert alert-danger" id="divAlert" style="display: none;" runat="server">
    <span id="alertMessage" runat="server"></span>
</div>


    <asp:Panel ID="pnlMensaje" runat="server" CssClass="mensaje-exito" style="display:none;">
    <h2>Usuario agregado correctamente !</h2>
    <asp:Button ID="btnCerrarMensaje" runat="server" Text="Cerrar" OnClick="btnCerrarMensaje_Click" CssClass="btn btn-primary mt-3" />
</asp:Panel>

<style>
    .mensaje-exito {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: white;
        padding: 50px;
        z-index: 1000;
        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
        text-align: center;
        width: 400px;
        border: 2px solid #c3e6cb;
        border-radius: 8px;
    }
</style>

</asp:Content>