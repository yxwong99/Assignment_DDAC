using System;
using Assignment_DDAC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Assignment_DDAC.Areas.Identity.IdentityHostingStartup))]
namespace Assignment_DDAC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Assignment_DDACNewContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Assignment_DDACNewContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Assignment_DDACNewContext>();
            });
        }
    }
}