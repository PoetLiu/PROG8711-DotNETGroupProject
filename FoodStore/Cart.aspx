<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FoodStore.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <h2>Your Shopping Cart</h2>
    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnRemove" runat="server" CommandArgument='<%# Eval("ProductID") %>' Text="Remove" CssClass="btn btn-danger" OnClick="btnRemove_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblTotal" runat="server" Text="Total: $" CssClass="h4"></asp:Label>
    <br />
    <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-primary" OnClick="btnCheckout_Click" />
</asp:Content>
