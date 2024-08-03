using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace FoodStore
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optionally, you can load or initialize data here
            }
        }

        // Add New Food
        protected void btnAddFood_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Foods (Name, Description, CategoryId, Price, Stock, ImageUrl) VALUES (@Name, @Description, @CategoryId, @Price, @Stock, @ImageUrl)", con);
                    cmd.Parameters.AddWithValue("@Name", tbName.Text);
                    cmd.Parameters.AddWithValue("@Description", tbDescription.Text);
                    cmd.Parameters.AddWithValue("@CategoryId", ddlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(tbPrice.Text));
                    cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(tbStock.Text));
                    cmd.Parameters.AddWithValue("@ImageUrl", tbImageUrl.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                lblMessage.Text = "Food item added successfully.";
                gvFoods.DataBind(); // Refresh GridView
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Manage Foods - Editing
        protected void gvFoods_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFoods.EditIndex = e.NewEditIndex;
            gvFoods.DataBind();
        }

        protected void gvFoods_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvFoods.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvFoods.Rows[e.RowIndex];

                TextBox tbName = row.FindControl("TextBoxName") as TextBox;
                TextBox tbDescription = row.FindControl("TextBoxDescription") as TextBox;
                DropDownList ddlCategory = row.FindControl("ddlEditCategory") as DropDownList;
                TextBox tbPrice = row.FindControl("TextBoxPrice") as TextBox;
                TextBox tbStock = row.FindControl("TextBoxStock") as TextBox;
                TextBox tbImageUrl = row.FindControl("TextBoxImageUrl") as TextBox;

                if (tbName == null || tbDescription == null || ddlCategory == null || tbPrice == null || tbStock == null || tbImageUrl == null)
                {
                    lblMessage.Text = "Error: One or more controls were not found.";
                    return;
                }

                string name = tbName.Text;
                string description = tbDescription.Text;
                int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                decimal price = Convert.ToDecimal(tbPrice.Text);
                int stock = Convert.ToInt32(tbStock.Text);
                string imageUrl = tbImageUrl.Text;

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Foods SET Name = @Name, Description = @Description, CategoryId = @CategoryId, Price = @Price, Stock = @Stock, ImageUrl = @ImageUrl WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvFoods.EditIndex = -1;
                gvFoods.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvFoods_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFoods.EditIndex = -1;
            gvFoods.DataBind();
        }

        protected void gvFoods_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvFoods.DataKeys[e.RowIndex].Value);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Foods WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvFoods.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Manage Orders - Editing
        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrders.EditIndex = e.NewEditIndex;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvOrders.Rows[e.RowIndex];

                TextBox tbUserId = row.FindControl("TextBoxUserId") as TextBox;
                TextBox tbTotalAmount = row.FindControl("TextBoxTotalAmount") as TextBox;
                TextBox tbCreatedAt = row.FindControl("TextBoxCreatedAt") as TextBox;
                TextBox tbShippingAddressId = row.FindControl("TextBoxShippingAddressId") as TextBox;

                if (tbUserId == null || tbTotalAmount == null || tbCreatedAt == null || tbShippingAddressId == null)
                {
                    lblMessage.Text = "Error: One or more controls were not found.";
                    return;
                }

                int userId = Convert.ToInt32(tbUserId.Text);
                decimal totalAmount = Convert.ToDecimal(tbTotalAmount.Text);
                DateTime createdAt = Convert.ToDateTime(tbCreatedAt.Text);
                int shippingAddressId = Convert.ToInt32(tbShippingAddressId.Text);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Orders SET UserId = @UserId, TotalAmount = @TotalAmount, CreatedAt = @CreatedAt, ShippingAddressId = @ShippingAddressId WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);
                    cmd.Parameters.AddWithValue("@ShippingAddressId", shippingAddressId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvOrders.EditIndex = -1;
                gvOrders.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrders.EditIndex = -1;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvOrders.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Manage Shipping Addresses - Editing
        protected void gvShippingAddresses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvShippingAddresses.EditIndex = e.NewEditIndex;
            gvShippingAddresses.DataBind();
        }

        protected void gvShippingAddresses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvShippingAddresses.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvShippingAddresses.Rows[e.RowIndex];

                DropDownList ddlUser = row.FindControl("ddlEditUser") as DropDownList;
                TextBox tbFullName = row.FindControl("TextBoxFullName") as TextBox;
                TextBox tbPhone = row.FindControl("TextBoxPhone") as TextBox;
                TextBox tbAddress = row.FindControl("TextBoxAddress") as TextBox;
                TextBox tbCity = row.FindControl("TextBoxCity") as TextBox;
                TextBox tbProvince = row.FindControl("TextBoxProvince") as TextBox;
                TextBox tbPostcode = row.FindControl("TextBoxPostcode") as TextBox;

                if (ddlUser == null || tbFullName == null || tbPhone == null || tbAddress == null || tbCity == null || tbProvince == null || tbPostcode == null)
                {
                    lblMessage.Text = "Error: One or more controls were not found.";
                    return;
                }

                int userId = Convert.ToInt32(ddlUser.SelectedValue);
                string fullName = tbFullName.Text;
                string phone = tbPhone.Text;
                string address = tbAddress.Text;
                string city = tbCity.Text;
                string province = tbProvince.Text;
                string postcode = tbPostcode.Text;

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Shipping_Address SET UserId = @UserId, FullName = @FullName, Phone = @Phone, Address = @Address, City = @City, Province = @Province, Postcode = @Postcode WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Province", province);
                    cmd.Parameters.AddWithValue("@Postcode", postcode);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvShippingAddresses.EditIndex = -1;
                gvShippingAddresses.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvShippingAddresses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvShippingAddresses.EditIndex = -1;
            gvShippingAddresses.DataBind();
        }

        protected void gvShippingAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvShippingAddresses.DataKeys[e.RowIndex].Value);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Shipping_Address WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvShippingAddresses.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Manage Users - Editing
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            gvUsers.DataBind();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
                GridViewRow row = gvUsers.Rows[e.RowIndex];

                TextBox tbUsername = row.FindControl("TextBoxUsername") as TextBox;
                TextBox tbEmail = row.FindControl("TextBoxEmail") as TextBox;
                TextBox tbPasswordHash = row.FindControl("TextBoxPasswordHash") as TextBox;
                DropDownList ddlType = row.FindControl("ddlType") as DropDownList;

                if (tbUsername == null || tbEmail == null || tbPasswordHash == null || ddlType == null)
                {
                    lblMessage.Text = "Error: One or more controls were not found.";
                    return;
                }

                string username = tbUsername.Text;
                string email = tbEmail.Text;
                string passwordHash = tbPasswordHash.Text;
                int type = Convert.ToInt32(ddlType.SelectedValue);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Type = @Type, Username = @Username, Email = @Email, PasswordHash = @PasswordHash WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvUsers.EditIndex = -1;
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            gvUsers.DataBind();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
