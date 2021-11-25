﻿using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<Product[]> GetAllAsync()
        {
            return _context.Products.ToArrayAsync();
        }
    }
}
