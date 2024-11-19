<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisPrestamos.aspx.cs" Inherits="TPC_GayolSofia.MisPrestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mb-2 mt-5">
        <div class="col-12 text-center">
            <asp:GridView ID="Dgv_Prestamos" DataKeyNames="IdPrestamo" AllowPaging="True" PageSize="10"
                runat="server" AutoGenerateColumns="false" OnPageIndexChanging="Dgv_Prestamos_PageIndexChanging" OnRowDataBound="gvPrestamos_RowDataBound" CssClass="table table-striped table-bordered table-hover mx-auto">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="IdPrestamo" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaFin" HeaderText="Fecha de Fin" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Metodo Envio" DataField="MetodoEnvio" />
                    <asp:BoundField HeaderText="Metodo Retiro" DataField="MetodoRetiro" />
                    <asp:BoundField HeaderText="Costo Envio" DataField="CostoEnvio" />
                    <asp:BoundField HeaderText="Estado" DataField="Estado" />
                    <asp:TemplateField HeaderText="Libros">
                        <ItemTemplate>
                            <%# Eval("Libros") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
