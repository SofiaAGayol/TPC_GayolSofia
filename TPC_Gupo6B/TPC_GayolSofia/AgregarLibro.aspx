﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarLibro.aspx.cs" Inherits="TPC_GayolSofia.AgregarLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container mt-5 mb-5">
        <div class="card mx-auto" style="max-width: 500px;">
            <div class="card-body">
                <h5 class="card-title text-center mb-2">Agregar Libro</h5>
                
                <div class="form-group text-center mt-2">
                    <label for="txtTitulo">Título</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>
                <div class="form-group text-center">
                    <label for="ddlAutor">Autor</label>
                        <asp:DropDownList ID="ddlAutor" runat="server" CssClass="form-control w-75 mx-auto">
                        </asp:DropDownList>
                </div>
                <div class="form-group text-center">
                     <label for="ddlCategoria">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control w-75 mx-auto">
                    </asp:DropDownList>
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtFechaPublicacion">Fecha de Publicación</label>
                    <asp:TextBox ID="txtFechaPublicacion" runat="server" CssClass="form-control w-75 mx-auto" TextMode="Date" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtEjemplares">Ejemplares</label>
                    <asp:TextBox ID="txtEjemplares" runat="server" CssClass="form-control w-75 mx-auto" TextMode="Number" />
                </div>
                <div class="form-group text-center mt-2">
                    <label for="txtImagenURL">Imagen URL</label>
                    <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control w-75 mx-auto" TextMode="Url" />
                </div>


                <div class="text-center">
                    <asp:Button ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" Text="Agregar" CssClass="btn btn-success mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnModificar" OnClick="btnModificar_Click" runat="server" Text="Modificar" CssClass="btn btn-success mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnBaja" OnClick="btnBaja_Click" Text="Dar Baja" runat="server" CssClass="btn btn-danger mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnRestablecer" OnClick="btnRestablecer_Click" Text="Restablecer" runat="server" CssClass="btn btn-primary mt-2" />
                </div>
                 <div class="text-center">
                    <asp:CheckBox ID="CbBajaDef" runat="server" />
                </div>
                <div class="text-center">
                     <asp:Button ID="btnBajaDef" OnClick="btnBajaDef_Click" runat="server" Text="Confirmar Baja" CssClass="btn btn-danger mt-2" />
                </div>
            </div>
        </div>
    </div>
   

    <div class="mx-auto justify-content-center mt-5 alert alert-danger" id="divAlert" style="display: none;" runat="server">
    <span id="alertMessage" runat="server"></span>
</div>


    <asp:Panel ID="pnlMensaje" runat="server" CssClass="mensaje-exito" style="display:none;">
    <h2>Libro agregado correctamente !</h2>
    <asp:Button ID="btnCerrarMensaje" runat="server" Text="Cerrar" OnClick="btnCerrarMensaje_Click1"  CssClass="btn btn-primary mt-3" />
    </asp:Panel>

    <asp:Panel ID="pnlMensajeModificacion" runat="server" CssClass="mensaje-exito" style="display:none;">
    <h2>Libro modificado correctamente !</h2>
    <asp:Button ID="Button1" runat="server" Text="Cerrar" OnClick="btnCerrarMensaje_Click1" CssClass="btn btn-primary mt-3" />
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
