using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public UnitOfWork() : this(new ApplicationDbContext())
        {
        }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CompanyRepository = new CompanyRepository(_context);
        }

        public ICompanyRepository CompanyRepository { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task DeleteDatabaseAsync() => await _context.Database.EnsureDeletedAsync();
        public async Task MigrateDatabaseAsync() => await _context.Database.MigrateAsync();
        public async Task CreateDatabaseAsync() => await _context.Database.EnsureCreatedAsync();

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
