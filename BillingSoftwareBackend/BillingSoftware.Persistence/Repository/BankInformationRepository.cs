using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class BankInformationRepository : Repository<BankInformation>, IBankInformationRepository
    {
        public BankInformationRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<BankInformation[]> GetAllAsync()
        {
            return _context.BankInformations.ToArrayAsync();
        }
    }
}
