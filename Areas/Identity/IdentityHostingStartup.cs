using System;
using LoginPage.Areas.Identity.Data;
using LoginPage.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LoginPage.Areas.Identity.IdentityHostingStartup))]
namespace LoginPage.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                var builder = new SqlConnectionStringBuilder(context.Configuration.GetConnectionString("LoginPageDBContextConnection"));
                services.AddDbContext<LoginPageDBContext>(options => options.UseSqlServer(builder.ConnectionString));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginPageDBContext>();
            });
        }
    }
}
