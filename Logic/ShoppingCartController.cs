using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Logic
{
    public class ShoppingCartController : Controller
    {
        public string ShoppingCartId { get; set; }

        private ShopItemsContext _shopItemContext;

        public const string CartSessionKey = "CardId";
        
        public ShoppingCartController(ShopItemsContext shopItemsContext)
        {
            this._shopItemContext = shopItemsContext;
        }

        [HttpPost]
        public void AddToCart(int id)
        {
            ShoppingCartId = GetCartId();

            var cartItem = _shopItemContext.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ShopItemId == id);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CardItemId = Guid.NewGuid().ToString(),
                    ShopItemId = id,
                    CartId = ShoppingCartId,
                    ShopItem = _shopItemContext.ShopItems.SingleOrDefault(
                        p => p.id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _shopItemContext.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _shopItemContext.SaveChanges();
        }

        public string GetCartId()
        {
            var sessionCartId = HttpContext.Session.GetString(CartSessionKey);

            if (sessionCartId == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    HttpContext.Session.SetString(CartSessionKey, HttpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            sessionCartId = HttpContext.Session.GetString(CartSessionKey);
            return sessionCartId;
        }

        [HttpGet]
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _shopItemContext.CartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }

       [Route("Shop/Cart")]
        public IActionResult Cart()
        {
            ViewBag.CartItems = _shopItemContext.CartItems.ToList();
            ViewBag.CartId = GetCartId();

            return View();
        }
    }
}
