<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_GayolSofia.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-center mt-5">
        <div class="card" style="width: 600px;">
            <div class="card-body">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Btn_Ingreso">
                    <form>
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Usuario" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Contrasenia" TextMode="Password" type="password" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>
                        <div class="text-center">
                            <asp:Button ID="Btn_Ingreso" OnClick="Btn_Ingreso_Click" class="btn btn-primary" runat="server" Text="Ingresar" />
                        </div>
                        <div class="text-center mt-3">
                            <a href="#" class="text-decoration-none">¿Has olvidado tu contraseña?</a>
                        </div>
                        <div class="text-center mt-2">
                            <span>¿No eres miembro todavía? <br /> Elige un plan <a href="CrearUsuario.aspx" class="text-decoration-none">¡Y empieza ahora!</a></span>
                        </div>
                    </form>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="mx-auto justify-content-center mt-5 alert alert-danger" id="divAlert" style="display: none;" runat="server">
        <span id="alertMessage" runat="server"></span>
    </div>
</asp:Content>
