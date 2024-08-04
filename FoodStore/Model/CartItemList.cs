using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Model
{
    public class CartItemList
    {
        private List<CartItem> cartItems;

        public CartItemList()
        {
            cartItems = new List<CartItem>();
        }

        public int Count
        {
            get { return cartItems.Count; }
        }

        public CartItem this[int id]
        {
            get
            {
                return cartItems.FirstOrDefault(c => c.Product.Id == id);
            }
            set
            {
                CartItem item = cartItems.FirstOrDefault(c => c.Product.Id == id);
                if (item != null)
                {
                    item = value;
                }
                else
                {
                    cartItems.Add(value);
                }
            }
        }

        public static CartItemList GetCart()
        {
            CartItemList cart = (CartItemList)HttpContext.Current.Session["Cart"];
            if (cart == null)
            {
                HttpContext.Current.Session["Cart"] = new CartItemList();
            }
            return (CartItemList)HttpContext.Current.Session["Cart"];
        }

        public void AddItem(Food product, int quantity)
        {
            CartItem cartItem = this[product.Id];
            if (cartItem == null)
            {
                cartItems.Add(new CartItem(product, quantity));
            }
            else
            {
                cartItem.AddQuantity(quantity);
            }
        }

        public void RemoveAt(int index)
        {
            cartItems.RemoveAt(index);
        }

        public void Clear()
        {
            cartItems.Clear();
        }

        public List<CartItem> GetCartItems()
        {
            return cartItems;
        }

        public void RemoveItem(int productId)
        {
            CartItem item = cartItems.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
            {
                cartItems.Remove(item);
            }
        }

        public decimal GetTotalAmount()
        {
            return cartItems.Sum(item => item.Total);
        }
    }
}
