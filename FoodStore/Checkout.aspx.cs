using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using FoodStore.Model;

namespace FoodStore
{
    public partial class Checkout : System.Web.UI.Page
    {
        private CartItemList cart;

        protected void Page_Load(object sender, EventArgs e)
        {
            cart = CartItemList.GetCart(); 
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                string fullName = txtFullName.Text;
                string address = txtAddress.Text;
                string city = txtCity.Text;
                string province = txtProvince.Text;
                string postcode = txtPostcode.Text;
                string phone = txtPhone.Text;
                string notes = txtNotes.Text;

               
                HttpCookie userCookie = Request.Cookies["UserInfo"];
                if (userCookie == null || string.IsNullOrEmpty(userCookie["Email"]))
                {
                    lblMessage.Text = "User not logged in.";
                    return;
                }
                string email = userCookie["Email"];
                int userId = GetUserIdFromDatabase(email);

               
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                       
                        string insertAddressQuery = @"
                            INSERT INTO ShippingAddress (UserId, FullName, Phone, Address, City, Province, Postcode)
                            OUTPUT INSERTED.Id
                            VALUES (@UserId, @FullName, @Phone, @Address, @City, @Province, @Postcode)";

                        int shippingAddressId;
                        using (SqlCommand cmd = new SqlCommand(insertAddressQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@FullName", fullName);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Address", address);
                            cmd.Parameters.AddWithValue("@City", city);
                            cmd.Parameters.AddWithValue("@Province", province);
                            cmd.Parameters.AddWithValue("@Postcode", postcode);
                            shippingAddressId = (int)cmd.ExecuteScalar();
                        }

                      
                        string insertOrderQuery = @"
                            INSERT INTO Orders (UserId, TotalAmount, CreatedAt, ShippingAddressId)
                            OUTPUT INSERTED.Id
                            VALUES (@UserId, @TotalAmount, @CreatedAt, @ShippingAddressId)";

                        int orderId;
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@TotalAmount", cart.GetTotalAmount());
                            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ShippingAddressId", shippingAddressId);
                            orderId = (int)cmd.ExecuteScalar();
                        }

                        foreach (var item in cart.GetCartItems())
                        {
                            string insertOrderItemQuery = @"
                                INSERT INTO OrderItems (OrderId, FoodId, Quantity, Price)
                                VALUES (@OrderId, @FoodId, @Quantity, @Price)";

                            using (SqlCommand cmd = new SqlCommand(insertOrderItemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@OrderId", orderId);
                                cmd.Parameters.AddWithValue("@FoodId", item.ProductID);
                                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@Price", item.Price);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        cart.Clear();

                        lblMessage.Text = "Thank you for your order!";

                        
                        Response.Redirect("Foods.aspx");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblMessage.Text = "An error occurred while processing your order. Please try again.";
                    }
                }
            }
        }

      
        private int GetUserIdFromDatabase(string email)
        {
            int userId = -1;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    userId = (int)cmd.ExecuteScalar();
                }
            }

            return userId;
        }
    }
}
