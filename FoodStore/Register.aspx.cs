using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace FoodStore
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optionally, you can add any page load logic here
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (IsValidInput(username, email, password))
            {
                string passwordHash = HashPassword(password); // Assuming you have a method to hash the password

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (Type, Username, Email, PasswordHash, CreatedAt) VALUES (@Type, @Username, @Email, @PasswordHash, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", 1); // Set the Type value as needed
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            lblMessage.Text = "Registration successful!";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
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
                lblMessage.Text = "Please fill in all fields correctly.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool IsValidInput(string username, string email, string password)
        {
            // Implement your validation logic here, e.g., check if fields are not empty
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            // For example, using a library like BCrypt.Net or SHA256
            return password; // Placeholder, replace with actual hash implementation
        }
    }
}
