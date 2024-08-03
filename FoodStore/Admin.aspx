<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="FoodStore.Admin" Title="Admin" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Admin Dashboard</h2>

        <!-- Add New Food -->
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
                <asp:Button ID="btnAddFood" runat="server" Text="Add Food" CssClass="btn btn-primary" OnClick="btnAddFood_Click" />
            </div>

            <!-- Manage Foods -->
            <div class="col-md-6">
                <h4>Manage Foods</h4>
                <asp:GridView ID="gvFoods" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceFoods"
                    OnRowEditing="gvFoods_RowEditing" OnRowUpdating="gvFoods_RowUpdating" OnRowCancelingEdit="gvFoods_RowCancelingEdit" OnRowDeleting="gvFoods_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:TemplateField HeaderText="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEditCategory" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceCategories" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("CategoryId") %>'></asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelCategory" runat="server" Text='<%# Bind("CategoryName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxPrice" runat="server" Text='<%# Bind("Price") %>' CssClass="form-control" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelPrice" runat="server" Text='<%# Bind("Price") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxStock" runat="server" Text='<%# Bind("Stock") %>' CssClass="form-control" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelStock" runat="server" Text='<%# Bind("Stock") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image URL">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxImageUrl" runat="server" Text='<%# Bind("ImageUrl") %>' CssClass="form-control" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelImageUrl" runat="server" Text='<%# Bind("ImageUrl") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceFoods" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="SELECT Foods.Id, Foods.Name, Foods.Description, Foods.CategoryId, Categories.Name AS CategoryName, Foods.Price, Foods.Stock, Foods.ImageUrl FROM Foods INNER JOIN Categories ON Foods.CategoryId = Categories.Id" 
                    UpdateCommand="UPDATE Foods SET Name = @Name, Description = @Description, CategoryId = @CategoryId, Price = @Price, Stock = @Stock, ImageUrl = @ImageUrl WHERE Id = @Id" 
                    DeleteCommand="DELETE FROM Foods WHERE Id = @Id">
                    <UpdateParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="CategoryId" Type="Int32" />
                        <asp:Parameter Name="Price" Type="Decimal" />
                        <asp:Parameter Name="Stock" Type="Int32" />
                        <asp:Parameter Name="ImageUrl" Type="String" />
                    </UpdateParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>

        
            <!-- Manage Orders -->
            <div class="row mt-4">
                <div class="col-md-12">
                    <h4>Manage Orders</h4>
                    <asp:GridView ID="gvOrders" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceOrders"
                        OnRowEditing="gvOrders_RowEditing" OnRowUpdating="gvOrders_RowUpdating" OnRowCancelingEdit="gvOrders_RowCancelingEdit" OnRowDeleting="gvOrders_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                            <asp:BoundField DataField="UserId" HeaderText="User ID" SortExpression="UserId" />
                            <asp:TemplateField HeaderText="Total Amount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxTotalAmount" runat="server" Text='<%# Bind("TotalAmount") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelTotalAmount" runat="server" Text='<%# Bind("TotalAmount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />
                            <asp:BoundField DataField="ShippingAddressId" HeaderText="Shipping Address ID" SortExpression="ShippingAddressId" />
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceOrders" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT * FROM Orders"
                        UpdateCommand="UPDATE Orders SET UserId = @UserId, TotalAmount = @TotalAmount, CreatedAt = @CreatedAt, ShippingAddressId = @ShippingAddressId WHERE Id = @Id"
                        DeleteCommand="DELETE FROM Orders WHERE Id = @Id">
                        <UpdateParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:Parameter Name="UserId" Type="Int32" />
                            <asp:Parameter Name="TotalAmount" Type="Decimal" />
                            <asp:Parameter Name="CreatedAt" Type="DateTime" />
                            <asp:Parameter Name="ShippingAddressId" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        <!-- Manage Shipping Addresses -->
            <div class="row mt-4">
                <div class="col-md-12">
                    <h4>Manage Shipping Addresses</h4>
                    <asp:GridView ID="gvShippingAddresses" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceShippingAddresses"
                        OnRowEditing="gvShippingAddresses_RowEditing" OnRowUpdating="gvShippingAddresses_RowUpdating" OnRowCancelingEdit="gvShippingAddresses_RowCancelingEdit" OnRowDeleting="gvShippingAddresses_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                            <asp:TemplateField HeaderText="User ID">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditUser" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceUsers" DataTextField="Username" DataValueField="Id" SelectedValue='<%# Bind("UserId") %>'></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelUserId" runat="server" Text='<%# Bind("UserId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Full Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxFullName" runat="server" Text='<%# Bind("FullName") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelFullName" runat="server" Text='<%# Bind("FullName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxPhone" runat="server" Text='<%# Bind("Phone") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelPhone" runat="server" Text='<%# Bind("Phone") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxAddress" runat="server" Text='<%# Bind("Address") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("Address") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxCity" runat="server" Text='<%# Bind("City") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelCity" runat="server" Text='<%# Bind("City") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Province">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxProvince" runat="server" Text='<%# Bind("Province") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelProvince" runat="server" Text='<%# Bind("Province") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Postcode">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxPostcode" runat="server" Text='<%# Bind("Postcode") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelPostcode" runat="server" Text='<%# Bind("Postcode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceShippingAddresses" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT * FROM Shipping_Address"
                        UpdateCommand="UPDATE Shipping_Address SET UserId = @UserId, FullName = @FullName, Phone = @Phone, Address = @Address, City = @City, Province = @Province, Postcode = @Postcode WHERE Id = @Id" 
                        DeleteCommand="DELETE FROM Shipping_Address WHERE Id = @Id">
                        <UpdateParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:Parameter Name="UserId" Type="Int32" />
                            <asp:Parameter Name="FullName" Type="String" />
                            <asp:Parameter Name="Phone" Type="String" />
                            <asp:Parameter Name="Address" Type="String" />
                            <asp:Parameter Name="City" Type="String" />
                            <asp:Parameter Name="Province" Type="String" />
                            <asp:Parameter Name="Postcode" Type="String" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
          <!-- Manage Users -->
            <div class="row mt-4">
                <div class="col-md-12">
                    <h4>Manage Users</h4>
                    <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceUsers"
                        OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                            <asp:TemplateField HeaderText="Username">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxUsername" runat="server" Text='<%# Bind("Username") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelUsername" runat="server" Text='<%# Bind("Username") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelEmail" runat="server" Text='<%# Bind("Email") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" 
                                        SelectedValue='<%# Bind("Type") %>'>
                                        <asp:ListItem Value="1">Admin</asp:ListItem>
                                        <asp:ListItem Value="2">User</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelType" runat="server" Text='<%# Bind("Type") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT * FROM Users"
                        UpdateCommand="UPDATE Users SET Username = @Username, Email = @Email, Type = @Type WHERE Id = @Id" 
                        DeleteCommand="DELETE FROM Users WHERE Id = @Id">
                        <UpdateParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:Parameter Name="Username" Type="String" />
                            <asp:Parameter Name="Email" Type="String" />
                            <asp:Parameter Name="Type" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
    </div>
</asp:Content>
