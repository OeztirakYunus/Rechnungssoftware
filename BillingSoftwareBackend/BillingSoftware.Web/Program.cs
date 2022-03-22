using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSoftware.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var server = CreateHostBuilder(args).Build();
            try{
                using var scope = server.Services.CreateScope();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            
            server.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
