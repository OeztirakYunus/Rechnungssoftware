using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class Repository<T> : IRepository<T>
    {
        protected ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual Task<T[]> GetAllAsync()
        {
            return null;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.FindAsync(typeof(T), id);
            return (T)result;
        }

        public async Task Remove(int id)
        {
            var result = await GetByIdAsync(id);
            _context.Remove(result);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
