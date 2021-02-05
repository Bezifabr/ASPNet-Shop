using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class CartItem
    {
        [Key]
        public string CardItemId { get; set; }

        public string CartId { get; set; }
        
        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ShopItemId { get; set; }

        public virtual ShopItem ShopItem { get; set; }

    }
}
