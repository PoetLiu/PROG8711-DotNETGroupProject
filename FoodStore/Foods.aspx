<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Foods.aspx.cs" Inherits="FoodStore.Foods" Title="Foods" MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div runat="server" class="row justify-content-md-center">
        <div class="col-10">
            <div class="mb-3 row justify-content-md-center">
                <div class="col-6 row">
                    <asp:Label ID="Label1" class="form-label col-3 fw-bold" runat="server" Text="Category"></asp:Label>
                    <div class="col-8">
                        <asp:DropDownList class="form-control form-select" ID="ddlCategories" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM Categories" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"></asp:SqlDataSource>
                </div>
                <div class="col-6 row">
                    <asp:Label ID="lblFoods" class="form-label col-3 fw-bold" runat="server" Text="Food"></asp:Label>
                    <div class="col-8">
                        <asp:DropDownList class="form-control form-select" ID="ddlFoods" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlFoods_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="SELECT Foods.Id, Foods.Name, Foods.Description, Foods.Stock, Foods.Price, Categories.Name AS Category, Foods.ImageUrl, Foods.CreatedAt FROM Foods INNER JOIN Categories ON Foods.CategoryId = Categories.Id WHERE (Foods.CategoryId = @CategoryId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlCategories" Name="CategoryId" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="card mb-3 rounded shadow">
                <div class="row g-0">
                    <div class="col-md-7">
                        <asp:Image ID="imgFood" runat="server" CssClass="img-fluid rounded-start" alt="the photo of food" />
                    </div>
                    <div class="col-md-5">
                        <div class="card-body row justify-content-center">
                            <h5 class="card-title">
                                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                            </h5>
                            <div>
                                <asp:Label ID="lblDescription" class="form-label" runat="server" Text="Description"></asp:Label>
                            </div>
                            <div>
                                <span class="fw-bold">Category: </span>
                                <asp:Label ID="lblCategory" class="form-label" runat="server" Text="Category"></asp:Label>
                            </div>
                            <div>
                                <span class="fw-bold">Stock: </span>
                                <asp:Label ID="lblStock" class="form-label" runat="server" Text="Stock"></asp:Label>
                            </div>
                            <div>
                                <span class="fw-bold">Price: </span>
                                <asp:Label class="form-label" ID="lblPrice" runat="server" Text="Price"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblQuantity" class="form-label fw-bold" runat="server" Text="Quantity"></asp:Label><br />
                                <asp:TextBox ID="tbQuantity" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbQuantity" CssClass="text-danger" ErrorMessage="Quantity is required."></asp:RequiredFieldValidator><br />
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbQuantity" CssClass="text-danger" ErrorMessage="Quantity must range from 1 to 10." MaximumValue="10" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                            </div>
                            <div>
                                <asp:Button ID="btnAddToCart" class="btn btn-primary" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" />
                                <asp:Button ID="btnGoToCart" class="btn btn-primary" runat="server" Text="Go To Cart" PostBackUrl="~/Cart" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

