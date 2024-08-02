using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoodStore
{
    public partial class Foods : System.Web.UI.Page
    {

        private Food selectedFood;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.HeadingText = "Foods";
                ddlCategories.DataBind();
                ddlFoods.DataBind();
                RenderSelectedFood();
            }

        }

        protected void RenderSelectedFood()
        {
            selectedFood = GetFood(ddlFoods.SelectedValue);
            RenderFood(selectedFood);
        }

        private void RenderFood(Food p)
        {
            lblName.Text = p.Name;
            lblDescription.Text = p.Description;
            lblCategory.Text = p.Category;
            lblStock.Text = p.Stock.ToString();
            lblPrice.Text = p.Price.ToString("C") + " Each";
            imgFood.ImageUrl = "Images/Foods/" + p.ImageUrl;
        }

        private Food GetFood(String Id)
        {
            DataView foodsTable = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            foodsTable.RowFilter = "Id= " + Id + "";
            DataRowView row = foodsTable[0];

            Food p = new Food
            {
                Id = Int32.Parse(row["Id"].ToString()),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Stock = Int32.Parse(row["Stock"].ToString()),
                Price = (decimal)row["Price"],
                Category = row["Category"].ToString(),
                ImageUrl = row["ImageUrl"].ToString()
            };
            return p;
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            // TODO add to cart
            Response.Redirect("~/Cart");
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFoods.DataBind();
            RenderSelectedFood();
        }

        protected void ddlFoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderSelectedFood();
        }
    }
}