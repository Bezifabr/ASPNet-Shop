using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ShopItemsContext : DbContext
    {
        public ShopItemsContext(DbContextOptions<ShopItemsContext> options)
               : base(options)
        {
        }
        public DbSet<ShopItem> ShopItems { get; set; }
    }
}
