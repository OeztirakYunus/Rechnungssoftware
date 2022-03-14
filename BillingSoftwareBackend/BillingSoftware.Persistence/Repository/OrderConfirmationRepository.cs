using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class OrderConfirmationRepository : Repository<OrderConfirmation>, IOrderConfirmationRepository
    {
        public OrderConfirmationRepository(ApplicationDbContext context) : base(context)
        {
        }
     
        public async Task<Invoice> OrderConfirmationToInvoice(OrderConfirmation orderConfirmation)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceDate = System.DateTime.Now;
            invoice.InvoiceNumber = "I " + orderConfirmation.Id;
            invoice.DocumentInformationId = invoice.DocumentInformationId;
            invoice.Status = Status.CLOSED;
            await Update(invoice);
            await Update(orderConfirmation);
            return invoice;
        }

        public async Task<DeliveryNote> OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation)
        {
            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN " + orderConfirmation.Id;
            deliveryNote.DocumentInformationsId = orderConfirmation.DocumentInformationId;
            deliveryNote.Status = Status.CLOSED;
            await Update(deliveryNote);
            await Update(orderConfirmation);
            return deliveryNote;
        }

        override public async Task<OrderConfirmation[]> GetAllAsync()
        {
            return await _context.OrderConfirmations
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<OrderConfirmation> GetByIdAsync(Guid id)
        {
            return await _context.OrderConfirmations
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
