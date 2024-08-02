using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FoodStore
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string HeadingText
        {
            set { lblHeading.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in by checking for the presence of the cookie
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            bool isLoggedIn = userCookie != null;

            // Clear existing navigation items
            navList.Controls.Clear();

            // Show only Logout button if the user is an admin
            if (isLoggedIn)
            {
                if (userCookie["Type"] == "admin")
                {
                    AddLogoutItem(); // Adds the Logout link
                }
                else
                {
                    // Add standard navigation items for regular users
                    AddNavItem("Foods.aspx", "Foods");
                    AddNavItem("Cart.aspx", "Cart");
                    AddLogoutItem(); // Add Logout link for non-admin users as well
                }
            }
            else
            {
                // Add navigation items for non-logged-in users
                AddNavItem("Home.aspx", "Home");
                AddNavItem("Login.aspx", "Login");
                AddNavItem("Register.aspx", "Register");
            }

            // Highlight the current page in the navigation
            HighlightCurrentPage();
        }

        private void AddNavItem(string url, string text)
        {
            // Create a new list item (li)
            var li = new HtmlGenericControl("li");
            li.Attributes["class"] = "nav-item";

            // Create a hyperlink (a) with the specified URL and text
            var a = new HyperLink
            {
                NavigateUrl = url,
                CssClass = "nav-link text",
                Text = text
            };

            // Add the hyperlink to the list item
            li.Controls.Add(a);

            // Add the list item to the navigation list
            navList.Controls.Add(li);
        }

        private void AddLogoutItem()
        {
            // Create a new list item (li) for logout
            var liLogout = new HtmlGenericControl("li");
            liLogout.Attributes["class"] = "nav-item";

            // Create a hyperlink (a) for logout
            var aLogout = new HyperLink
            {
                NavigateUrl = "Logout.aspx",
                CssClass = "nav-link text",
                Text = "Logout"
            };

            // Add the hyperlink to the list item
            liLogout.Controls.Add(aLogout);

            // Add the list item to the navigation list
            navList.Controls.Add(liLogout);
        }

        private void HighlightCurrentPage()
        {
            // Highlight the current page in the navigation
            foreach (Control ctl in navList.Controls)
            {
                if (ctl is HtmlGenericControl li && li.Controls[0] is HyperLink a)
                {
                    if (Page.AppRelativeVirtualPath.IndexOf(a.NavigateUrl, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        a.CssClass += " active";
                        break;
                    }
                }
            }
        }
    }
}
