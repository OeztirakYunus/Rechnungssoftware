using BillingSoftware.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSoftware.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var server = CreateHostBuilder(args).Build();
            using var scope = server.Services.CreateScope();
            try
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var deleteDatabase = Environment.GetEnvironmentVariable("DELETE_DATABASE");
            if (string.IsNullOrEmpty(deleteDatabase))
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                Debug.Write(configuration.ToString());
                deleteDatabase = configuration["DeleteDatabase"];
            }

            if (deleteDatabase.ToLower().Equals("true"))
            {
                await context.Database.EnsureDeletedAsync();
            }
            await context.Database.EnsureCreatedAsync();

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
