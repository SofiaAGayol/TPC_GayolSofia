<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_GayolSofia.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2>Billing Details</h2>
        <div class="row">
            <div class="col-md-8">
                <div id="formCheckout">
                    <div class="mb-3">
                        <label for="txtFirstName" class="form-label">First Name*</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtLastName" class="form-label">Last Name*</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="ddlCountry" class="form-label">Country*</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" Required="True">
                            <asp:ListItem Text="Select A Country" Value="" />
                            <asp:ListItem Text="Argentina" Value="Argentina" />
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="txtStreetAddress" class="form-label">Street Address*</label>
                        <asp:TextBox ID="txtStreetAddress" runat="server" CssClass="form-control" placeholder="Street Address" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCity" class="form-label">Town/City*</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Town/City" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtPhone" class="form-label">Phone*</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone" Required="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email Address*</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" Required="True"></asp:TextBox>
                    </div>
                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkSaveForNextPayment" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkSaveForNextPayment">Save For My Next Payment</label>
                    </div>
                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkShipToDifferentAddress" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkShipToDifferentAddress">Ship To A Different Address?</label>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <h4>Our Order</h4>
                <div class="card">
                    <div class="card-body">
                        <p><strong>Product</strong> <span class="float-end">Subtotal</span></p>
                        <hr />
                        <p>Shipping:</p>
                        <div class="form-check">
                            <asp:RadioButton ID="rdoFreeShipping" runat="server" GroupName="Shipping" Text="Free Shipping" CssClass="form-check-input" />
                            <label class="form-check-label" for="rdoFreeShipping">Free Shipping</label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="rdoLocalShipping" runat="server" GroupName="Shipping" Text="Local: $5000" CssClass="form-check-input" />
                            <label class="form-check-label" for="rdoLocalShipping">Local: $5000</label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="rdoFlatRate" runat="server" GroupName="Shipping" Text="Flat Rate: $10000" CssClass="form-check-input" />
                            <label class="form-check-label" for="rdoFlatRate">Flat Rate: $10000</label>
                        </div>
                        <hr />
                        <p>Total: <strong>$<asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></strong></p>
                    </div>
                </div>
                <div class="mt-3">
                    <h5>Payment Method</h5>
                    <div class="form-check">
                        <asp:RadioButton ID="rdoBankTransfer" runat="server" GroupName="Payment" Text="Direct Bank Transfer" CssClass="form-check-input" />
                        <label class="form-check-label" for="rdoBankTransfer">Direct Bank Transfer</label>
                    </div>
                    <div class="form-check">
                        <asp:RadioButton ID="rdoCashOnDelivery" runat="server" GroupName="Payment" Text="Cash On Delivery" CssClass="form-check-input" />
                        <label class="form-check-label" for="rdoCashOnDelivery">Cash On Delivery</label>
                    </div>
                    <div class="form-check">
                        <asp:RadioButton ID="rdoPaypal" runat="server" GroupName="Payment" Text="Paypal" CssClass="form-check-input" />
                        <label class="form-check-label" for="rdoPaypal">Paypal</label>
                    </div>
                </div>
                <div class="mt-3">
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-primary w-100" OnClick="btnPlaceOrder_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
