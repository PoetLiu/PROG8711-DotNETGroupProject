<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="FoodStore.Admin" Title="Admin" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Admin Dashboard</h2>
        <div class="row">
            <div class="col-md-6">
                <h4>Add New Food</h4>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
                <div class="form-group">
                    <asp:Label ID="lblName" runat="server" Text="Name" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceCategories" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Id, Name FROM Categories"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPrice" runat="server" Text="Price" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblStock" runat="server" Text="Stock" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbStock" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblImageUrl" runat="server" Text="Image URL" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbImageUrl" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblIsOnSale" runat="server" Text="On Sale" CssClass="form-label"></asp:Label>
                    <asp:CheckBox ID="cbIsOnSale" runat="server" CssClass="form-check-input" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSalePrice" runat="server" Text="Sale Price" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="tbSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="btnAddFood" runat="server" Text="Add Food" CssClass="btn btn-primary" OnClick="btnAddFood_Click" />
            </div>
            <div class="col-md-6">
                <h4>Manage Foods</h4>
                <asp:GridView ID="gvFoods" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceFoods">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                        <asp:BoundField DataField="ImageUrl" HeaderText="Image URL" SortExpression="ImageUrl" />
                        <asp:BoundField DataField="IsOnSale" HeaderText="On Sale" SortExpression="IsOnSale" />
                        <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" SortExpression="SalePrice" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-warning btn-sm" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceFoods" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Foods.Id, Foods.Name, Foods.Description, Categories.Name AS Category, Foods.Price, Foods.Stock, Foods.ImageUrl, Foods.IsOnSale, Foods.SalePrice FROM Foods INNER JOIN Categories ON Foods.CategoryId = Categories.Id" DeleteCommand="DELETE FROM Foods WHERE Id = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Manage Orders</h4>
                <asp:GridView ID="gvOrders" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceOrders">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="UserId" HeaderText="User ID" SortExpression="UserId" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount" />
                        <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />
                        <asp:BoundField DataField="ShippingAddressId" HeaderText="Shipping Address ID" SortExpression="ShippingAddressId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnViewOrder" runat="server" Text="View" CommandName="View" CssClass="btn btn-info btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceOrders" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Id, UserId, TotalAmount, CreatedAt, ShippingAddressId FROM Orders"></asp:SqlDataSource>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Manage Users</h4>
                <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceUsers">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditUser" runat="server" Text="Edit" CommandName="EditUser" CssClass="btn btn-warning btn-sm" />
                                <asp:Button ID="btnDeleteUser" runat="server" Text="Delete" CommandName="DeleteUser" CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Id, Username, Email, Type FROM Users" DeleteCommand="DELETE FROM Users WHERE Id = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Manage Shipping Addresses</h4>
                <asp:GridView ID="gvShippingAddresses" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceShippingAddresses">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="UserId" HeaderText="User ID" SortExpression="UserId" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                        <asp:BoundField DataField="Province" HeaderText="Province" SortExpression="Province" />
                        <asp:BoundField DataField="Postcode" HeaderText="Postcode" SortExpression="Postcode" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditAddress" runat="server" Text="Edit" CommandName="EditAddress" CssClass="btn btn-warning btn-sm" />
                                <asp:Button ID="btnDeleteAddress" runat="server" Text="Delete" CommandName="DeleteAddress" CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceShippingAddresses" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Id, UserId, FullName, Phone, Address, City, Province, Postcode FROM Shipping_Address" DeleteCommand="DELETE FROM Shipping_Address WHERE Id = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
