using System;

namespace FoodStore
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Example password
            string password = "Admin123";

            // Generate the hash
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Output or use the hash in your SQL insert query
            string script = $"console.log('Hashed Password: {hashedPassword}');";
            ClientScript.RegisterStartupScript(this.GetType(), "hashedPassword", script, true);
        }


    }
}