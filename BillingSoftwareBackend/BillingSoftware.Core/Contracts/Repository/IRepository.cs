using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T[]> GetAllAsync();
        Task AddAsync(T entity);
        Task AddAsync<E>(E entity);
        void Update(T entity);
        void Update<E>(E entity);
        Task Remove(int id);
    }
}
