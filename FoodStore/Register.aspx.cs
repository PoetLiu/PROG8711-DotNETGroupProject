using System;
using System.Data.SqlClient;
using System.Web.UI;
using BCrypt.Net;

namespace FoodStore
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Perform basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "All fields are required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Hash the password
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                // Set default type as 1 for regular user
                int type = 1;

                // Define your connection string (replace with your actual connection string)
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\swapnil\\Source\\Repos\\PROG8711-DotNETGroupProject\\FoodStore\\App_Data\\FoodStore.mdf;Integrated Security=True";

                // Insert the user into the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Users (Type, Username, Email, PasswordHash) VALUES (@Type, @Username, @Email, @PasswordHash)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            lblMessage.Text = "Registration successful!";
                        }
                        catch (SqlException sqlEx)
                        {
                            lblMessage.Text = "Database error: " + sqlEx.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "An error occurred: " + ex.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
