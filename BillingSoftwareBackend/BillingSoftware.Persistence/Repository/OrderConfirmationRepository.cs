using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(orderConfirmation.CompanyId)).SingleOrDefault();

            Invoice invoice = new Invoice();
            invoice.InvoiceDate = System.DateTime.Now;
            invoice.InvoiceNumber = "I" + DateTime.Now.ToString("yy") + cDocumentCounter.InvoiceCounter.ToString().PadLeft(5, '0');
            invoice.Subject = "Rechnung " + invoice.InvoiceNumber;
            invoice.HeaderText = "Vielen Dank für Ihren Auftrag. Wir berechnen Ihnen folgende Leistung:";
            invoice.FlowText = "Zahlbar sofort ohne Abzug. Für Rückfragen zu dieser Rechnung stehen wir gerne jederzeit zur Verfügung.";
            invoice.DocumentInformationId = orderConfirmation.DocumentInformationId;
            invoice.CompanyId = orderConfirmation.CompanyId;
            invoice.PaymentTerm = System.DateTime.Now.AddDays(14);

            cDocumentCounter.InvoiceCounter++;
            orderConfirmation.Status = Status.CLOSED;

            await AddAsync(invoice);
            await Update(orderConfirmation);
            await Update(cDocumentCounter);
            return invoice;
        }

        public async Task<DeliveryNote> OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation)
        {
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(orderConfirmation.CompanyId)).SingleOrDefault();

            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN" + DateTime.Now.ToString("yy") + cDocumentCounter.DeliveryNoteCounter.ToString().PadLeft(5, '0');
            deliveryNote.Subject = "Lieferschein " + deliveryNote.DeliveryNoteNumber;
            deliveryNote.HeaderText = "Vielen Dank für die Zusammenarbeit. Vereinbarungsgemäß liefern wir Ihnen folgende Waren:";
            deliveryNote.FlowText = "Die gelieferte Ware bleibt bis zu vollständiger Bezahlung unser Eigentum.";
            deliveryNote.DocumentInformationsId = orderConfirmation.DocumentInformationId;
            deliveryNote.CompanyId = orderConfirmation.CompanyId;

            cDocumentCounter.DeliveryNoteCounter++;

            await AddAsync(deliveryNote);
            await Update(cDocumentCounter);
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

        public override async Task Update(OrderConfirmation entity)
        {
            var docInfoRep = new Repository<DocumentInformations>(_context);
            var positionRepo = new Repository<Position>(_context);

            var docInfo = await docInfoRep.GetByIdAsync(entity.DocumentInformationId);
            if (docInfo != null)
            {
                entity.DocumentInformation.CopyProperties(docInfo);

                var positions = new List<Position>();
                foreach (var item in docInfo.Positions)
                {
                    var position = await positionRepo.GetByIdAsync(item.Id);
                    if (position != null)
                    {
                        item.CopyProperties(position);
                        positions.Add(position);
                        await positionRepo.Update(position);
                        await _context.SaveChangesAsync();
                    }
                }
                docInfo.Positions = positions;

                await docInfoRep.Update(docInfo);
                await _context.SaveChangesAsync();

                entity.DocumentInformation = docInfo;
            }

            await base.Update(entity);
        }
    }
}
