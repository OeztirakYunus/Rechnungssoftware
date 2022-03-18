using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class DeliveryNoteRepository : Repository<DeliveryNote>, IDeliveryNoteRepository
    {
        public DeliveryNoteRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public async Task<DeliveryNote[]> GetAllAsync()
        {
            return await _context.DeliveryNotes
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<DeliveryNote> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryNotes
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
