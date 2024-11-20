<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_GayolSofia.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-expand-sm navbar-dark bg-dark" style="height: 40px;">
        <div class="container">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link" href="Informes.aspx">Reportes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="Usuarios.aspx">Usuarios</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Libros.aspx">Libros</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Autores.aspx">Autores</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="row justify-content-center mb-2 mt-5">
        <div class="col-12 text-center">
            <asp:GridView ID="Dgv_Usuarios" DataKeyNames="IdUsuario" AllowPaging="True" PageSize="3"
                runat="server" AutoGenerateColumns="false" OnPageIndexChanging="Dgv_Usuarios_PageIndexChanging"
                OnSelectedIndexChanged="Dgv_Usuarios_SelectedIndexChanged" CssClass="table table-striped table-bordered table-hover mx-auto">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="IdUsuario" />
                    <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                    <asp:BoundField HeaderText="DNI" DataField="DNI" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <%# Eval("Rol.Descripcion")%>
                        </ItemTemplate>
                    </asp:TemplateField>
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





    <asp:Button ID="BtnAgregarUsuario" runat="server" Text="Agregar Usuario" OnClick="BtnAgregarUsuario_Click" CssClass="btn btn-success mb-5" />





</asp:Content>
