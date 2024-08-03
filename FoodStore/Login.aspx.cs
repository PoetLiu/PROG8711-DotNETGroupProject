using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace FoodStore
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any page load logic can be added here
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
                    string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        try
                        {
                            conn.Open();
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                string storedPasswordHash = result.ToString();
                                if (VerifyPassword(password, storedPasswordHash))
                                {
                                    lblMessage.Text = "Login successful!";
                                    lblMessage.ForeColor = System.Drawing.Color.Green;
                                    // Redirect to a different page after successful login
                                    Response.Redirect("~/Home.aspx");
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
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: " + ex.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
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

        private bool VerifyPassword(string password, string storedPasswordHash)
        {
            // Implement your password verification logic here
            // For example, using a library like BCrypt.Net or SHA256
            return password == storedPasswordHash; // Placeholder, replace with actual verification implementation
        }
    }
}
