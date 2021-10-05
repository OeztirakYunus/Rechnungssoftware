using BillingSoftware.Core.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICompanyRepository CompanyRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
        Task CreateDatabaseAsync();
    }

}
