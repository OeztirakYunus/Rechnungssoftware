using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using CommonBase.Exceptions;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(offer.CompanyId)).SingleOrDefault();

            OrderConfirmation orderConfirmation = new OrderConfirmation();
            orderConfirmation.OrderConfirmationDate = System.DateTime.Now;
            orderConfirmation.OrderConfirmationNumber = "OC" + DateTime.Now.ToString("yy") + cDocumentCounter.OrderConfirmationCounter.ToString().PadLeft(5, '0');
            orderConfirmation.Subject = "Auftragsbestätigung " + orderConfirmation.OrderConfirmationNumber;
            orderConfirmation.HeaderText = "Vielen Dank für Ihr Vertrauen und den Auftrag. Gemäß unserem Angebot erbringen wir folgende Leistungen:";
            orderConfirmation.FlowText = "Bei Rückfragen stehen wir selbstverständlich jeder Zeit gerne zur Verfügung.";
            orderConfirmation.DocumentInformationId = offer.DocumentInformationId;
            orderConfirmation.CompanyId = offer.CompanyId;
            
            offer.Status = Status.CLOSED;
            cDocumentCounter.OrderConfirmationCounter++;
            
            await AddAsync(orderConfirmation);
            await Update(offer);
            await Update(cDocumentCounter);
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

        public async Task UpdateWithDto(UpdateOfferDto dto)
        {
            var entity = await GetByIdAsync(dto.Id);
            if (entity != null)
            {
                DocumentInformationsRepository documentInformationsRepository = new DocumentInformationsRepository(_context);
                dto.DocumentInformation.Id = dto.DocumentInformationId;
                await documentInformationsRepository.UpdateWithDto(dto.DocumentInformation);
                dto.CopyProperties(entity);
                await Update(entity);
            }
            else
            {
                throw new EntityNotFoundException("DeliveryNote not found");
            }
        }
    }
}
