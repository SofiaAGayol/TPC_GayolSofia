<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="TPC_GayolSofia.CrearUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="d-flex justify-content-center mt-5">
        <div class="card" style="width: 600px;">
            <div class="card-body">
                <asp:Panel ID="PanelRegistroCliente" runat="server" DefaultButton="Btn_Registro">
                    <form>
                        <!-- Usuario -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Usuario" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>
                        
                        <!-- Contraseña -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblClave" runat="server" Text="Clave" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Clave" TextMode="Password" type="password" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>

                        <!-- Nombre -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Nombre" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>

                        <!-- Apellido -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Apellido" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>

                        <!-- DNI -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblDNI" runat="server" Text="DNI" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_DNI" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>

                        <!-- Email -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Email" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>

                        <!-- Teléfono -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="Tb_Telefono" class="form-control mx-auto" Style="width: 100%;" runat="server" />
                        </div>


<div class="row mt-5">

    <!-- Suscripción Básica -->
    <div class="col-md-4 d-flex justify-content-center mb-4">
        <div class="card" style="width: 18rem;">
            <img src="../imagenes/MembresiaBronce.png" class="card-img-top" alt="Suscripción Básica">
            <div class="card-body text-center">
                <h5 class="card-title">Suscripción Básica</h5>
                <p class="card-text">Incluye acceso limitado a contenido y soporte básico.</p>
                <input type="radio" name="suscripcion" value="basica" class="form-check-input">
            </div>
        </div>
    </div>

    <!-- Suscripción Estándar -->
    <div class="col-md-4 d-flex justify-content-center mb-4">
        <div class="card" style="width: 18rem;">
            <img src="../imagenes/MembresiaPlateada.png" class="card-img-top" alt="Suscripción Estándar">
            <div class="card-body text-center">
                <h5 class="card-title">Suscripción Estándar</h5>
                <p class="card-text">Acceso a contenido completo y soporte estándar.</p>
                <input type="radio" name="suscripcion" value="estandar" class="form-check-input">
            </div>
        </div>
    </div>

    <!-- Suscripción Premium -->
    <div class="col-md-4 d-flex justify-content-center mb-4">
        <div class="card" style="width: 18rem;">
            <img src="../imagenes/MembresiaDorada.png" class="card-img-top" alt="Suscripción Premium">
            <div class="card-body text-center">
                <h5 class="card-title">Suscripción Premium</h5>
                <p class="card-text">Acceso total a contenido, soporte premium y beneficios exclusivos.</p>
                <input type="radio" name="suscripcion" value="premium" class="form-check-input" checked>
            </div>
        </div>
    </div>

</div>

                                                <!-- Botón de Registro -->
                        <div class="text-center">
                            <asp:Button ID="Btn_Registro" OnClick="Btn_Registro_Click" class="btn btn-primary" runat="server" Text="Registrar" />
                        </div>
                        <div class="d-flex justify-content-center mt-5">
                    </form>
                </asp:Panel>
                    <div class="mx-auto justify-content-center mt-5 alert alert-danger" id="divAlert" style="display: none;" runat="server">
                        <span id="alertMessage" runat="server"></span>
                    </div>
            </div>
        </div>
    </div>

        <div class="mx-auto justify-content-center mt-5 alert alert-danger" id="div1" style="display: none;" runat="server">
    <span id="Span1" runat="server"></span>
    </div>

        <asp:Panel ID="pnlMensaje" runat="server" CssClass="mensaje-exito" style="display:none;">
    <h2>Registrado Correctamente !</h2>
    <asp:Button ID="btnCerrarMensaje" runat="server" Text="Cerrar" OnClick="btnCerrarMensaje_Click" CssClass="btn btn-primary mt-3" />
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

