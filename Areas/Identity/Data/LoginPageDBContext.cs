using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginPage.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginPage.Data
{
    public class LoginPageDBContext : IdentityDbContext<AppUser>
    {
        public LoginPageDBContext(DbContextOptions<LoginPageDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                .HasKey(e => e.Id);

            builder.Entity<AppUser>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users);
        }
    }
}
