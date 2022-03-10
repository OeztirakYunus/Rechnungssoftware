using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<Contact[]> GetAllAsync()
        {
            return await _context.Contacts
                .IncludeAllRecursively()
                .ToArrayAsync();
        }
        public override async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
