using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class DeliveryNoteRepository : Repository<DeliveryNote>, IDeliveryNoteRepository
    {
        public DeliveryNoteRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<DeliveryNote[]> GetAllAsync()
        {
            return _context.DeliveryNotes.ToArrayAsync();
        }
    }
}
