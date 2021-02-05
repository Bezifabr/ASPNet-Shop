using LoginPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.Logic;
using Shop.Models;
using Shop.Views.Shop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly ShopItemsContext _shopItemsContext;
        private readonly ILogger<ShopController> _logger;

        public ShopController(
            ILogger<ShopController> logger,
            ShopItemsContext shopItemsContext)
        {
            _logger = logger;
            _shopItemsContext = shopItemsContext;
        }

        [Route("Shop/Products/")]
        [Route("Shop/Products/cat={cat}")]
        public IActionResult Items(string cat)
        {
            ViewBag.Category = cat;
            ViewBag.ShopItems = _shopItemsContext.ShopItems;

            return View();
        }

        [Route("Shop/Products/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            foreach (var item in _shopItemsContext.ShopItems)
            {
                if (item.id == id)
                {
                    ViewBag.Item = item;
                    break;
                }
            }

            ViewBag.SuggestedItems = new List<ShopItem>();
            List<ShopItem> items = _shopItemsContext.ShopItems.ToList<ShopItem>();

            int i = 0;
            int j = id - 3;
            if (id < 3) j = 0;
            for (; j < items.Count; j++)
            {
                if (i >= 5) break;
                if (j == id - 1) j++;
                if (j >= items.Count - 1) j = 0;
                ViewBag.SuggestedItems.Add(items[j]);
                i++;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
