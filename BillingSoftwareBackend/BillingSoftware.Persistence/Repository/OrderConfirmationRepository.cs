﻿using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using BillingSoftware.Core.Enums;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class OrderConfirmationRepository : Repository<OrderConfirmation>, IOrderConfirmationRepository
    {
        public OrderConfirmationRepository(ApplicationDbContext context) : base(context)
        {
        }
     
        public Invoice OrderConfirmationToInvoice(OrderConfirmation orderConfirmation)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceDate = System.DateTime.Now;
            invoice.InvoiceNumber = "I " + orderConfirmation.Id;
            invoice.InvoiceInformations = invoice.InvoiceInformations;
            invoice.Status = Status.CLOSED;
            Update(invoice);
            Update(orderConfirmation);
            return invoice;
        }

        public DeliveryNote OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation)
        {
            DeliveryNote deliveryNote = new DeliveryNote();
            deliveryNote.DeliveryNoteDate = System.DateTime.Now;
            deliveryNote.DeliveryNoteNumber = "DN " + orderConfirmation.Id;
            deliveryNote.DeliveryNoteInformations = deliveryNote.DeliveryNoteInformations;
            deliveryNote.Status = Status.CLOSED;
            Update(deliveryNote);
            Update(orderConfirmation);
            return deliveryNote;
        }

        override public async Task<OrderConfirmation[]> GetAllAsync()
        {
            return await _context.OrderConfirmations
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<OrderConfirmation> GetByIdAsync(int id)
        {
            return await _context.OrderConfirmations
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
