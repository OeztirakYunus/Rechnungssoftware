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
        public Invoice OrderConfirmationToInvoice(OrderConfirmation orderConfirmation);
        public DeliveryNote OrderConfirmationToDeliveryNote(OrderConfirmation orderConfirmation);
    }
}
