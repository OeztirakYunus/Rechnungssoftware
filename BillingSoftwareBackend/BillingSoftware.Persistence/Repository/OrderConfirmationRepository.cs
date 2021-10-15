using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class OrderConfirmationRepository : Repository<OrderConfirmation>, IOrderConfirmationRepository
    {
        public OrderConfirmationRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<OrderConfirmation[]> GetAllAsync()
        {
            return _context.OrderConfirmations.ToArrayAsync();
        }
    }
}
