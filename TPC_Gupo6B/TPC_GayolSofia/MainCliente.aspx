<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainCliente.aspx.cs" Inherits="TPC_GayolSofia.MainCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row row-cols-1 row-cols-md-3">

        <asp:Repeater ID="RepeaterArticulos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100">
                        <asp:Image ID="Image1" alt="Imagen del libro" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("Imagen") %>' />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text"><%# Eval("Autor.Nombre") + " " + Eval("Autor.Apellido")%></p>
                            <asp:Button ID="botonElegir" class="btn btn-primary" runat="server" Text="¡Ver Mas!" CommandArgument='<%# Eval("IDLibro") %>' OnClick="botonElegir_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>

