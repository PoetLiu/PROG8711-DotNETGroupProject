using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using BCrypt.Net;

namespace FoodStore
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Perform basic validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter your email and password.";
                return;
            }

            // Replace with your actual connection string
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\swapnil\\Source\\Repos\\PROG8711-DotNETGroupProject\\FoodStore\\App_Data\\FoodStore.mdf;Integrated Security=True";
            string storedPasswordHash = null;
            int userType = -1; // Initialize to an invalid value

            // Check the email and password
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT PasswordHash, Type FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedPasswordHash = reader["PasswordHash"].ToString();
                            userType = Convert.ToInt32(reader["Type"]);
                        }
                    }
                }
            }

            // If the user exists and password is correct
            if (!string.IsNullOrEmpty(storedPasswordHash) && BCrypt.Net.BCrypt.Verify(password, storedPasswordHash))
            {
                // Create a cookie with the appropriate value
                HttpCookie userCookie = new HttpCookie("UserInfo");
                if (userType == 1)
                {
                    userCookie["Type"] = "user";
                }
                else if (userType == 2)
                {
                    userCookie["Type"] = "admin";
                }
                userCookie.Expires = DateTime.Now.AddHours(1); // Set the cookie to expire in 1 hour
                Response.Cookies.Add(userCookie);

                // Redirect based on user type
                if (userType == 1)
                {
                    Response.Redirect("Foods.aspx");
                }
                else if (userType == 2)
                {
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid user type.";
                }
            }
            else
            {
                lblMessage.Text = "Invalid email or password.";
            }
        }


    }
}
