<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiEstanteria.aspx.cs" Inherits="TPC_GayolSofia.MiEstanteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--No hay libros--%>
    <asp:Panel ID="PanelNoLibros" runat="server" Visible="false" CssClass="alert alert-info text-center mt-3">
        <i class="bi bi-info-circle-fill"></i>
        No hay libros para mostrar 
    </asp:Panel>

    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success d-none" Text="" />


    <%--Libros--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-4 g-3">



                <asp:Repeater ID="RepeaterArticulos" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card h-100">
                                <asp:Image ID="Image1" alt="Imagen del libro" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("Imagen") %>' />
                                <div class="card-body h-50  justify-content-end">
                                    <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                    <h6 class="card-text"><%# Eval("Autor.Nombre") + " " + Eval("Autor.Apellido")%></h6>
                                    <p class="card-title"><%# Eval("Categoria.Descripcion") %></p>
                                </div>
                                <div class="d-grid gap-1 d-flex justify-content-evenly mb-3 mx-3">
                                    <asp:Button ID="btnDevolver" class="btn btn-primary flex-fill" runat="server" Text="Devolver" CommandArgument='<%# Eval("IDLibro") %>' OnClick="botonDevolver_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
