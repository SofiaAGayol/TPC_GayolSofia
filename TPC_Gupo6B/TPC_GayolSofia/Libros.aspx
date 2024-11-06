<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Libros.aspx.cs" Inherits="TPC_GayolSofia.Libros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 40px;">
        <div class="container">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link " href="Usuarios.aspx">Usuarios</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active"  aria-current="page" href="Libros.aspx">Libros</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Informes.aspx">Reportes</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="row justify-content-center mb-2 mt-5">
        <div class="col-12 text-center">
            <asp:GridView ID="Dgv_Libros" DataKeyNames="IdLibro" AllowPaging="True" PageSize="5"
                runat="server" AutoGenerateColumns="false" OnPageIndexChanging="Dgv_Libros_PageIndexChanging"
                OnSelectedIndexChanged="Dgv_Libros_SelectedIndexChanged" CssClass="table table-striped table-bordered table-hover mx-auto">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="IdLibro" />
                    <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
                    <asp:BoundField HeaderText="Autor" DataField="Autor" />
                    <asp:BoundField HeaderText="Categoria" DataField="Categoria" />
                    <asp:BoundField HeaderText="Fecha Publicacion" DataField="FechaPublicacion" />
                    <asp:BoundField HeaderText="Ejemplares" DataField="Ejemplares" />
                    <asp:BoundField HeaderText="Disponibles" DataField="Disponibles" />
                    <asp:BoundField HeaderText="Imagen" DataField="Imagen" />

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





    <asp:Button ID="BtnAgregarLibro" runat="server" Text="Agregar Libro" OnClick="BtnAgregarLibro_Click" CssClass="btn btn-success mb-5" />





</asp:Content>
