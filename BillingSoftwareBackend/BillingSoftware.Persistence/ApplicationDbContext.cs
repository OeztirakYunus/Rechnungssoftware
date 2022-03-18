using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OrderConfirmation> OrderConfirmations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DocumentInformations> DocumentInformations { get; set; }
        public DbSet<CompanyDocumentCounter> CompanyDocumentCounters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Environment.CurrentDirectory)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //var configuration = builder.Build();
            //var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder
               // .UseLazyLoadingProxies(true)
                .UseSqlServer("Data Source=(localdb)\\MSSQLLOCALDB;Initial Catalog=BillingSoftwareDb;Integrated Security=True");
        }
    }
}
