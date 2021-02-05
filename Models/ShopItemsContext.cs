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
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartItem>()
                .HasKey(c => c.CardItemId);

            builder.Entity<CartItem>()
                .HasOne(s => s.ShopItem);
        }
    }
}
