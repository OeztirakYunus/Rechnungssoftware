using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace BillingSoftware.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        internal DbSet<Address> Addresses { get; set; }
        internal DbSet<DeliveryNote> DeliveryNotes { get; set; }
        internal DbSet<Invoice> Invoices { get; set; }
        internal DbSet<Offer> Offers { get; set; }
        internal DbSet<OrderConfirmation> OrderConfirmations { get; set; }
        internal DbSet<Position> Positions { get; set; }
        internal DbSet<Product> Products { get; set; }
        internal DbSet<Company> Companies { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<DocumentInformations> DocumentInformations { get; set; }
        internal DbSet<CompanyDocumentCounter> CompanyDocumentCounters { get; set; }
        internal DbSet<BSFile> BSFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                Debug.Write(configuration.ToString());
                connectionString = configuration["ConnectionStrings:DefaultConnection"];
            }
            Console.WriteLine($"!!!!Connecting with {connectionString}");
            optionsBuilder
                .UseSqlServer(connectionString, builder =>{
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
