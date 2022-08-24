using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public Task<DeliveryNote> InvoiceToDeliveryNote(Invoice invoice);
        public Task UpdateWithDto(UpdateInvoiceDto dto);
    }
}
