using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IOrderConfirmationRepository : IRepository<OrderConfirmation>
    {
        public Task<Invoice> OrderConfirmationToInvoice(OrderConfirmation orderConfirmation);
        public Task<DeliveryNote> OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation);
    }
}
