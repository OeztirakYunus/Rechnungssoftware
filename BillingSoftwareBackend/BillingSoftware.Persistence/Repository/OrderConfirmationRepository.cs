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
            var company = await _context.Companies.FindAsync(orderConfirmation.CompanyId);

            Invoice invoice = new Invoice();
            invoice.InvoiceDate = System.DateTime.Now;
            invoice.InvoiceNumber = "I" + DateTime.Now.ToString("yy") + company.InvoiceCounter.ToString().PadLeft(5, '0');
            invoice.DocumentInformationId = invoice.DocumentInformationId;
            invoice.CompanyId = orderConfirmation.CompanyId;
            invoice.PaymentTerm = System.DateTime.Now.AddDays(14);

            company.InvoiceCounter++;
            orderConfirmation.Status = Status.CLOSED;

            await AddAsync(invoice);
            await Update(orderConfirmation);
            await Update(company);
            return invoice;
        }

        public async Task<DeliveryNote> OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation)
        {
            var company = await _context.Companies.FindAsync(orderConfirmation.CompanyId);

            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN" + DateTime.Now.ToString("yy") + company.DeliveryNoteCounter.ToString().PadLeft(5, '0');
            deliveryNote.DocumentInformationsId = orderConfirmation.DocumentInformationId;
            deliveryNote.CompanyId = orderConfirmation.CompanyId;

            company.DeliveryNoteCounter++;

            await AddAsync(deliveryNote);
            await Update(company);
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
