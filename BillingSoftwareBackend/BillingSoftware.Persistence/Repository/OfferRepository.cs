using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<Offer[]> GetAllAsync()
        {
            return _context.Offers.ToArrayAsync();
        }

        public OrderConfirmation OfferToOrderConfirmation(Offer offer)
        {
            OrderConfirmation orderConfirmation = new OrderConfirmation();
            orderConfirmation.OrderConfirmationDate = System.DateTime.Now;
            orderConfirmation.OrderConfirmationNumber = "OC " + offer.Id;
            orderConfirmation.OrderConfirmationInformations = offer.OfferInformations;
            offer.Status = Status.CLOSED;
            Update(orderConfirmation);
            Update(offer);
            return orderConfirmation;
        }
    }
}
