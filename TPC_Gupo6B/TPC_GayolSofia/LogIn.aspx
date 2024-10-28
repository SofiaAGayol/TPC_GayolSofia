<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_GayolSofia.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="d-flex justify-content-center mt-5">
    <div class="card" style="width: 600px;">
        <div class="card-body">
            <form>
                <div class="mb-3 text-center">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="Tb_Usuario" class="form-control mx-auto" style="width: 100%;" runat="server" />
                </div>
                <div class="mb-3 text-center">
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="Tb_Contrasenia" type="password" class="form-control mx-auto" style="width: 100%;" runat="server" />
                </div>
                <div class="text-center">
                </div>
                <div class="text-center">
                    <asp:Button ID="Btn_Ingreso" OnClick="Btn_Ingreso_Click" class="btn btn-primary" runat="server" Text="Ingresar" />
                </div>
            </form>
        </div>
    </div>
</div>

<div class="mx-auto justify-content-center mt-5 alert alert-danger" id="divAlert" style="display: none;" runat="server">
    <span id="alertMessage" runat="server"></span>
</div>
</asp:Content>
