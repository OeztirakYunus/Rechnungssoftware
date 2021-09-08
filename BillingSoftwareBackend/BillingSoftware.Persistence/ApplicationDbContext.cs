using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BillingSoftware.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OrderConfirmation> OrderConfirmations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrganisationContact> OrganisationContacts { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Environment.CurrentDirectory)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //var configuration = builder.Build();
            //var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLOCALDB;Initial Catalog=BillingSoftwareDb;Integrated Security=True");
        }
    }
}
