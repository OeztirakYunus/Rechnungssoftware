using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<Invoice[]> GetAllAsync()
        {
            return _context.Invoices.ToArrayAsync();
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
    }
}
