<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Autores.aspx.cs" Inherits="TPC_GayolSofia.Autores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 40px;">
        <div class="container">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link" href="Informes.aspx">Reportes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Usuarios.aspx">Usuarios</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Libros.aspx">Libros</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="Autores.aspx">Autores</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="row justify-content-center mb-2 mt-5">
        <div class="col-12 text-center">
            <asp:GridView ID="Dgv_Autores" DataKeyNames="IdAutor" AllowPaging="True" PageSize="5"
                runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover mx-auto"
                OnPageIndexChanging="Dgv_Autores_PageIndexChanging" OnSelectedIndexChanged="Dgv_Autores_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="IdAutor" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                    <asp:TemplateField HeaderText="Nacionalidad">
                        <ItemTemplate>
                            <%# Eval("Nacionalidad.Descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Best Seller" DataField="BestSeller" />
                    <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Estado")) ? "✓" : "✖" %>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:CommandField HeaderText="Modificar" ShowSelectButton="true" SelectText=" ✍" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Button ID="BtnAgregarAutor" runat="server" Text="Agregar Autor" OnClick="BtnAgregarAutor_Click" CssClass="btn btn-success mb-5" />


</asp:Content>
