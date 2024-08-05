using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace FoodStore
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserEmail"] != null)
            {
                string email = Session["UserEmail"].ToString();
                int userId = (int)Session["UserId"];
                int userType = (int)Session["UserType"];

                HttpCookie userCookie = new HttpCookie("UserInfo");
                userCookie["UserId"] = userId.ToString();
                userCookie["Email"] = email;
                userCookie["Type"] = userType.ToString();
                userCookie.Expires = DateTime.Now.AddHours(1); // Set cookie expiration to 1 hour
                Response.Cookies.Add(userCookie);

                // Clear session after setting cookie to prevent redundant operations
                Session.Remove("UserEmail");
                Session.Remove("UserId");
                Session.Remove("UserType");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (IsValidInput(email, password))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Type, PasswordHash FROM Users WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        try
                        {
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int userId = (int)reader["Id"];
                                    int userType = (int)reader["Type"];
                                    string storedPasswordHash = reader["PasswordHash"].ToString();

                                    if (VerifyPassword(password, storedPasswordHash))
                                    {
                                        // Set session variables
                                        Session["UserId"] = userId;
                                        Session["UserType"] = userType;
                                        Session["IsLoggedIn"] = true;
                                        Session["UserEmail"] = email;

                                        HttpCookie userCookie = new HttpCookie("UserInfo");
                                        userCookie["UserId"] = userId.ToString();
                                        userCookie["Email"] = email;
                                        userCookie["Type"] = userType.ToString();
                                        userCookie.Expires = DateTime.Now.AddHours(1); // Set cookie expiration to 1 hour
                                        Response.Cookies.Add(userCookie);

                                        lblMessage.Text = "Login successful!";
                                        lblMessage.ForeColor = System.Drawing.Color.Green;

                                        // Redirect based on user type
                                        if (userType == 2)
                                        {
                                            // Redirect to admin dashboard or specific admin page
                                            Response.Redirect("~/Admin.aspx");
                                        }
                                        else
                                        {
                                            // Redirect to home or user-specific page
                                            Response.Redirect("~/Home.aspx");
                                        }
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Invalid email or password.";
                                        lblMessage.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Invalid email or password.";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: An unexpected error occurred.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            // Log the exception (e.g., using a logging framework)
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool IsValidInput(string email, string password)
        {
            return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Direct comparison for plain text passwords (for testing purposes)
            return enteredPassword == storedPasswordHash;
        }
    }
}
