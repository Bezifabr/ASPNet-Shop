using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Views.Shop
{
    [AllowAnonymous]
    public class ItemsModel : PageModel
    {
        private readonly ShopItemsContext _shopItemsContext;

        ItemsModel(ShopItemsContext shopItemsContext)
        {
            _shopItemsContext = shopItemsContext;
        }

        [BindProperty]
        public OutputModel Output { get; set; }
        public string ReturnUrl { get; set; }


        public class OutputModel
        {
            public ICollection<ShopItem> shopItems;
            
        }

        public async Task OnGetAsync(string returnUrl = null)
        {

            foreach (var item in _shopItemsContext.ShopItems)
            {
                Output.shopItems.Add(item);
            }


            ReturnUrl = returnUrl;
        }
    }
}
