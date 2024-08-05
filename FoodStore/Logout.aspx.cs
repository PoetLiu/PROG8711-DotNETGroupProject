using System;
using System.Web;

namespace FoodStore
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            Session.Abandon();

            // Optionally clear authentication cookies if using Forms Authentication
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                HttpCookie authCookie = new HttpCookie("ASP.NET_AuthCookie", string.Empty);
                authCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(authCookie);

                HttpCookie sessionCookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
                sessionCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(sessionCookie);
            }

            // Redirect to the login page or home page
            Response.Redirect("~/Login.aspx");
        }
    }
}
