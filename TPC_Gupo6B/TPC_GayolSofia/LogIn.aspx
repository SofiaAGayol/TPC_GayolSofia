<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TPC_GayolSofia.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form>
        <div class="row mb-3">
            <asp:Label ID="lblUsuario" class="col-sm-2 col-form-label" runat="server" Text="Usuario"></asp:Label>
            <div class="col-sm-10">
                <asp:TextBox ID="Tb_Usuario" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            <label for="inputPassword3" class="col-sm-2 col-form-label">Contraseña</label>
            <div class="col-sm-10">
                <asp:TextBox type="password" class="form-control" ID="Tb_Contrasenia" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-3">
            
        </div>
        <div class="row mb-3">
            <asp:Label ID="LblError" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="Btn_Ingreso" OnClick="Btn_Ingreso_Click" class="btn btn-primary" runat="server" Text="Ingresar" />
        
    </form>
</asp:Content>
