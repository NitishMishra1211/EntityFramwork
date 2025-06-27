using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartItems";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartItemsJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartItemsJson);
        }

        public void AddToCart(CartItem item)
        {
            var cartItems = GetCartItems() as List<CartItem>;
            var existingItem = cartItems.Find(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cartItems.Add(item);
            }
            SaveCartItems(cartItems);
        }

        public void RemoveFromCart(int productId)
        {
            var cartItems = GetCartItems() as List<CartItem>;
            var item = cartItems.Find(i => i.ProductId == productId);
            if (item != null)
            {
                cartItems.Remove(item);
            }
            SaveCartItems(cartItems);
        }

        public void ClearCart()
        {
            SaveCartItems(new List<CartItem>());
        }

        public decimal GetTotal()
        {
            return GetCartItems().Sum(i => i.Total);
        }

        private void SaveCartItems(List<CartItem> cartItems)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            session.SetString(CartSessionKey, cartItemsJson);
        }
    }
}
