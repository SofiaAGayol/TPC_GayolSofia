<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DevolverLibro.aspx.cs" Inherits="TPC_GayolSofia.DevolverLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header bg-primary text-white">
                        <h3>Devolución de Libro</h3>
                    </div>
                    <div class="card-body">
                        <p class="card-text">
                            Para devolver el libro prestado, por favor comuníquese con la Biblioteca Borges.
                       
                        </p>
                        <h4>Detalles de la Biblioteca:</h4>
                        <p><strong>Nombre:</strong> Biblioteca Borges</p>
                        <p><strong>Dirección:</strong> Av. Siempre Viva 123, Buenos Aires, Argentina</p>
                        <p><strong>Teléfono:</strong> +54 11 1234 5678</p>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-secondary" Text="Volver a Mi Estantería" OnClick="btnVolver_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
