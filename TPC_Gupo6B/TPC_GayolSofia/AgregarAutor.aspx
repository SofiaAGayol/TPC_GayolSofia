<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarAutor.aspx.cs" Inherits="TPC_GayolSofia.AgregarAutor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5 mb-5">
        <div class="card mx-auto" style="max-width: 500px;">
            <div class="card-body">
                <h5 class="card-title text-center mb-2">Agregar Autor</h5>

                <div class="form-group text-center mt-2">
                    <label for="txtNombreAutor">Nombre</label>
                    <asp:TextBox ID="txtNombreAutor" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>

                <div class="form-group text-center mt-2">
                    <label for="txtApellidoAutor">Apellido</label>
                    <asp:TextBox ID="txtApellidoAutor" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>

                <div class="form-group text-center mt-2">
                    <label for="ddlNacionalidadAutor">Nacionalidad</label>
                    <asp:DropDownList ID="ddlNacionalidadAutor" runat="server" CssClass="form-control w-75 mx-auto">
                    </asp:DropDownList>
                </div>

                <div class="form-group text-center mt-2">
                    <label for="txtBestSellerAutor">Best Seller</label>
                    <asp:TextBox ID="txtBestSellerAutor" runat="server" CssClass="form-control w-75 mx-auto" />
                </div>

                <div class="text-center">
                    <asp:Button ID="btnGuardarAutor" OnClick="btnGuardarAutor_Click" runat="server" Text="Agregar" CssClass="btn btn-success mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnModificarAutor" OnClick="btnModificarAutor_Click" runat="server" Text="Modificar" CssClass="btn btn-success mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnEliminarAutor" OnClick="btnEliminarAutor_Click" Text="Eliminar" runat="server" CssClass="btn btn-danger mt-2" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnRestablecerAutor" OnClick="btnRestablecerAutor_Click" Text="Restablecer" runat="server" CssClass="btn btn-primary mt-2" />
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


    <asp:Panel ID="pnlMensajeAutor" runat="server" CssClass="mensaje-exito" Style="display: none;">
        <h2>Autor agregado correctamente!</h2>
        <asp:Button ID="btnCerrarMensajeAutor" runat="server" Text="Cerrar" OnClick="btnCerrarMensajeAutor_Click" CssClass="btn btn-primary mt-3" />
    </asp:Panel>

    <asp:Panel ID="pnlMensajeModificacionAutor" runat="server" CssClass="mensaje-exito" Style="display: none;">
        <h2>Autor modificado correctamente!</h2>
        <asp:Button ID="btnCerrarMensajeModAutor" runat="server" Text="Cerrar" OnClick="btnCerrarMensajeAutor_Click" CssClass="btn btn-primary mt-3" />
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
