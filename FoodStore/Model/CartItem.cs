namespace FoodStore.Model
{
    public class CartItem
    {
        public Food Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Food product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public string Display()
        {
            string displayString = string.Format("{0} ({1} at {2})",
                Product.Name,
                Quantity.ToString(),
                Product.Price.ToString("c"));
            return displayString;
        }

        public decimal Total
        {
            get { return Quantity * Product.Price; }
        }

        public string ProductName
        {
            get { return Product.Name; }
        }

        public int ProductID
        {
            get { return Product.Id; }
        }

        public decimal Price
        {
            get { return Product.Price; }
        }
    }
}
