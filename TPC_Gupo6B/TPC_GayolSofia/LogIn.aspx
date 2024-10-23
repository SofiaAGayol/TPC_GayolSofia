<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TPC_GayolSofia.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form>
        <div class="row mb-3">
            <label for="inputEmail3" class="col-sm-2 col-form-label">Usuario</label>
            <div class="col-sm-10">
                <input type="email" class="form-control" id="inputEmail3">
            </div>
        </div>
        <div class="row mb-3">
            <label for="inputPassword3" class="col-sm-2 col-form-label">Contraseña</label>
            <div class="col-sm-10">
                <input type="password" class="form-control" id="inputPassword3">
            </div>
        </div>
        <div class="row mb-3">
            <label for="inputMasterKey" class="col-sm-2 col-form-label">Clave Maestra</label>
            <div class="col-sm-10">
                <input type="password" class="form-control" id="inputMasterKey">
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-10 offset-sm-2">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="gridCheck1">
                    <label class="form-check-label" for="gridCheck1">
                        Soy Administrador
                    </label>
                </div>
            </div>
        </div>
        <button type="submit" class="btn btn-primary">Sign in</button>
    </form>
</asp:Content>
