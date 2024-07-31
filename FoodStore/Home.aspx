<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FoodStore.Default" Title="Home" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>


<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div class="text-center">
        <asp:Image ID="imgHome" runat="server" ImageUrl="~/Images/home.png" CssClass="rounded shadow m-2 col-8" /><br />
        <asp:Button ID="btnGoShopping" PostBackUrl="~/foods" runat="server" Text="Go Shopping" CssClass="btn btn-primary" />
    </div>
</asp:Content>