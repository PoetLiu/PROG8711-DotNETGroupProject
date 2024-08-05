using System;
using System.Web.UI;

namespace FoodStore
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        // Property to get or set the heading text
        public string HeadingText
        {
            get { return lblHeading.Text; }
            set { lblHeading.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
            {
                // Show Logout and hide Login/Register links
                LogoutLink.Visible = true;
                LoginLink.Visible = false;
                RegisterLink.Visible = false;

                // Check user type and adjust the visibility of admin and cart links
                if (Session["UserType"] != null)
                {
                    int userType = (int)Session["UserType"];
                    if (userType == 2)
                    {
                        // Admin user - show Admin link, hide Cart link
                        AdminLink.Visible = true;
                        CartLink.Visible = false;
                        FoodsLink.Visible = false;
                    }
                    else
                    {
                        // Normal user - hide Admin link, show Cart link
                        AdminLink.Visible = false;
                        CartLink.Visible = true;
                    }
                }
                else
                {
                    // Default to non-admin settings if UserType is not set
                    AdminLink.Visible = false;
                    CartLink.Visible = true;
                }
            }
            else
            {
                // User is not logged in - show Login/Register links and hide Logout link
                LogoutLink.Visible = false;
                LoginLink.Visible = true;
                RegisterLink.Visible = true;
                CartLink.Visible = false; // Hide Cart link for users not logged in
                AdminLink.Visible = false; // Hide Admin link for users not logged in
            }
        }
    }
}
