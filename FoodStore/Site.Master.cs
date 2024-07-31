using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (Control ctl in navList.Controls)
            {
                if (!(ctl is HtmlGenericControl))
                    continue;

                var li = (HtmlGenericControl)ctl;
                var a = (HyperLink)li.Controls[1];

                if (Page.AppRelativeVirtualPath.Contains(a.NavigateUrl)) {
                    a.CssClass += " active";
                    break;
                }
            }

        }
    }
}