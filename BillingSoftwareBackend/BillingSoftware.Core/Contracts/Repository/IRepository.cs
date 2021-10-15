﻿using System;
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
        void Update(T entity);
        Task Remove(int id);
    }
}
