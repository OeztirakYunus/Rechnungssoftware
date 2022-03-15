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

        public async Task<OrderConfirmation> OfferToOrderConfirmation(Offer offer)
        {
            var company = await _context.Companies.FindAsync(offer.CompanyId);
            
            OrderConfirmation orderConfirmation = new OrderConfirmation();
            orderConfirmation.OrderConfirmationDate = System.DateTime.Now;
            orderConfirmation.OrderConfirmationNumber = "OC" + DateTime.Now.ToString("yy") + company.OrderConfirmationCounter.ToString().PadLeft(5, '0');
            orderConfirmation.DocumentInformationId = offer.DocumentInformationId;
            orderConfirmation.CompanyId = offer.CompanyId;
            
            offer.Status = Status.CLOSED;
            company.OrderConfirmationCounter++;
            
            await AddAsync(orderConfirmation);
            await Update(offer);
            await Update(company);
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
