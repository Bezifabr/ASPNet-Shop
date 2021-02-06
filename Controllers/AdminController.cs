using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Models;
using Shop.Views.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ShopItemsContext _shopItemsContext;

        public AdminController(ShopItemsContext shopItemsContext)
        {
            _shopItemsContext = shopItemsContext;
        }

        [Route("AdminPanel")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("AdminPanel/ShopItemsManager")]
        public ActionResult ShopItems()
        {
            ViewBag.ShopItems = _shopItemsContext.ShopItems;
            return View();
        }

        [Route("AdminPanel/ShopItemsManager/Add")]
        public ActionResult ShopItemCreator()
        {
            return View();
        }

        ShopItemDetailsModel shopItemDetailsModel = new ShopItemDetailsModel();

        [Route("AdminPanel/Details/{id:int}")]
        public ActionResult ShopItemDetails(int id)
        {
            foreach (var item in _shopItemsContext.ShopItems)
            {
                if (item.id == id)
                {
                    ViewBag.Item = item;
                    break;
                }
            }



            return View(shopItemDetailsModel);
        }

        [Route("AdminPanel/ShopItemsCategoriesManager")]
        public ActionResult ShopItemsCategories()
        {
            return View();
        }


        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public void ChangeShopItem(ShopItem shopItem)
        {
            var item = _shopItemsContext.ShopItems.FirstOrDefault(s => s.id == shopItem.id);
            item.name = shopItem.name;
            item.description = shopItem.description;
            item.pictureUrl = shopItem.pictureUrl;
            item.price = shopItem.price;

            _shopItemsContext.SaveChanges();
        }

        [HttpPost]
        public void AddShopItem(ShopItem shopItem)
        {
            _shopItemsContext.ShopItems.Add(shopItem);
            _shopItemsContext.SaveChanges();
        }

        [HttpDelete]
        public void RemoveShopItem(int id)
        {
            var shopItem = _shopItemsContext.ShopItems.SingleOrDefault(s => s.id == id);
            if (shopItem != null) _shopItemsContext.Remove(shopItem);
            _shopItemsContext.SaveChanges();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
