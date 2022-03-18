using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }
      
        public async Task<DeliveryNote> InvoiceToDeliveryNote(Invoice invoice)
        {
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(invoice.CompanyId)).SingleOrDefault();
            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN" + DateTime.Now.ToString("yy") + cDocumentCounter.DeliveryNoteCounter.ToString().PadLeft(5, '0');
            deliveryNote.Subject = "Lieferschein " + deliveryNote.DeliveryNoteNumber;
            deliveryNote.HeaderText = "Vielen Dank für die Zusammenarbeit. Vereinbarungsgemäß liefern wir Ihnen folgende Waren:";
            deliveryNote.FlowText = "Die gelieferte Ware bleibt bis zu vollständiger Bezahlung unser Eigentum.";
            deliveryNote.DocumentInformationsId = invoice.DocumentInformationId;
            deliveryNote.CompanyId = invoice.CompanyId;
            cDocumentCounter.DeliveryNoteCounter++;

            await AddAsync(deliveryNote);
            await Update(cDocumentCounter);

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
