using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }
      
        public DeliveryNote InvoiceToDeliveryNote(Invoice invoice)
        {
            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN " + invoice.Id;
            deliveryNote.DeliveryNoteInformations = invoice.InvoiceInformations;
            Update(deliveryNote);
            return deliveryNote;
        }

        override public async Task<Invoice[]> GetAllAsync()
        {
            return await _context.Invoices
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await _context.Invoices
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
