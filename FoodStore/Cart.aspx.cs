using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodStore.Model;

namespace FoodStore
{
    public partial class Cart : System.Web.UI.Page
    {
        private CartItemList cart;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            cart = CartItemList.GetCart();

            if (cart == null)
            {
                cart = new CartItemList();
                Session["Cart"] = cart;
            }

            if (!IsPostBack)
            {
                DisplayCart();
            }
        }

        private void DisplayCart()
        {
            gvCart.DataSource = cart.GetCartItems().Select(item => new
            {
                ProductID = item.ProductID,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                Total = item.Total
            }).ToList();
            gvCart.DataBind();
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = cart.GetCartItems().Sum(x => x.Total);
            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);
            cart.RemoveItem(productId);
            DisplayCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}
