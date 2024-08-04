<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="FoodStore.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <h2>Checkout</h2>
    <div class="form-group">
        <asp:Label ID="lblFullName" runat="server" Text="Full Name:" AssociatedControlID="txtFullName"></asp:Label>
        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Full Name is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblAddress" runat="server" Text="Address:" AssociatedControlID="txtAddress"></asp:Label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblCity" runat="server" Text="City:" AssociatedControlID="txtCity"></asp:Label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblProvince" runat="server" Text="Province:" AssociatedControlID="txtProvince"></asp:Label>
        <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvProvince" runat="server" ControlToValidate="txtProvince" ErrorMessage="Province is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblPostcode" runat="server" Text="Postcode:" AssociatedControlID="txtPostcode"></asp:Label>
        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPostcode" runat="server" ControlToValidate="txtPostcode" ErrorMessage="Postcode is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
    </div>
    <div class="form-group">
        <asp:Label ID="lblNotes" runat="server" Text="Notes:" AssociatedControlID="txtNotes"></asp:Label>
        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
    </div>
    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" OnClick="btnPlaceOrder_Click" CssClass="btn btn-primary" />
    <br /><br />
    <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
</asp:Content>
