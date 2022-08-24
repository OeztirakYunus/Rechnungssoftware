using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IOfferRepository : IRepository<Offer>
    {
        public Task<OrderConfirmation> OfferToOrderConfirmation(Offer offer);
        public Task UpdateWithDto(UpdateOfferDto dto);
    }
}
