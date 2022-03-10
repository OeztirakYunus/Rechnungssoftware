using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext context) : base(context)
        {
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

        override public async Task<Offer[]> GetAllAsync()
        {
            return await _context.Offers
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<Offer> GetByIdAsync(Guid id)
        {
            return await _context.Offers
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
