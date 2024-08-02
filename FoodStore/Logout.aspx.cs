using System;
using System.Web;

namespace FoodStore
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove the UserInfo cookie
            HttpCookie userCookie = new HttpCookie("UserInfo");
            userCookie.Expires = DateTime.Now.AddDays(-1); // Set expiration date to a past date to delete the cookie
            Response.Cookies.Add(userCookie);

            // Redirect to the Home page
            Response.Redirect("Home.aspx");
        }
    }
}
