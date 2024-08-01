using System;
using System.Data.SqlClient;

namespace FoodStore
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string description = tbDescription.Text;
            int categoryId = int.Parse(ddlCategory.SelectedValue);
            decimal price = decimal.Parse(tbPrice.Text);
            int stock = int.Parse(tbStock.Text);
            string imageUrl = tbImageUrl.Text;
            bool isOnSale = cbIsOnSale.Checked;
            decimal? salePrice = isOnSale ? (decimal?)decimal.Parse(tbSalePrice.Text) : null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Foods (Name, Description, CategoryId, Price, Stock, ImageUrl, IsOnSale, SalePrice) VALUES (@Name, @Description, @CategoryId, @Price, @Stock, @ImageUrl, @IsOnSale, @SalePrice)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Stock", stock);
                    command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    command.Parameters.AddWithValue("@IsOnSale", isOnSale);
                    command.Parameters.AddWithValue("@SalePrice", salePrice.HasValue ? (object)salePrice.Value : DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            lblMessage.Text = "Food added successfully!";
            gvFoods.DataBind();
        }

        protected void gvFoods_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvFoods.EditIndex = e.NewEditIndex;
            gvFoods.DataBind();
        }

        protected void gvFoods_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = (int)e.Keys["Id"];
            string name = (string)e.NewValues["Name"];
            string description = (string)e.NewValues["Description"];
            string category = (string)e.NewValues["Category"];
            decimal price = (decimal)e.NewValues["Price"];
            int stock = (int)e.NewValues["Stock"];
            string imageUrl = (string)e.NewValues["ImageUrl"];
            bool isOnSale = (bool)e.NewValues["IsOnSale"];
            decimal? salePrice = isOnSale ? (decimal?)e.NewValues["SalePrice"] : null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Foods SET Name = @Name, Description = @Description, CategoryId = (SELECT Id FROM Categories WHERE Name = @Category), Price = @Price, Stock = @Stock, ImageUrl = @ImageUrl, IsOnSale = @IsOnSale, SalePrice = @SalePrice WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Stock", stock);
                    command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    command.Parameters.AddWithValue("@IsOnSale", isOnSale);
                    command.Parameters.AddWithValue("@SalePrice", salePrice.HasValue ? (object)salePrice.Value : DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            gvFoods.EditIndex = -1;
            gvFoods.DataBind();
        }

        protected void gvFoods_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvFoods.EditIndex = -1;
            gvFoods.DataBind();
        }

        protected void gvFoods_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = (int)e.Keys["Id"];

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Foods WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            gvFoods.DataBind();
        }
    }
}
