using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            var total = _cartService.GetTotal();
            ViewBag.Total = total;
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price)
        {
            _cartService.AddToCart(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = 1,
                Price = price
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
