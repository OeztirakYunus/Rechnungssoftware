using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task AddAddress(Guid companyId, Address address);
        Task AddContact(Guid companyId, Contact contact);
        Task AddDeliveryNote(Guid companyId, DeliveryNote deliveryNote);
        Task AddInvoice(Guid companyId, Invoice invoice);
        Task AddOffer(Guid companyId, Offer offer);
        Task AddOrderConfirmation(Guid companyId, OrderConfirmation orderConfirmation);
        Task AddProduct(Guid companyId, Product product);
        Task AddUser(Guid companyId, UserAddDto user);
        Task DeleteAddress(Guid companyId, Guid addressId);
        Task DeleteContact(Guid companyId, Guid contactId);
        Task DeleteDeliveryNote(Guid companyId, Guid deliveryNoteId);
        Task DeleteInvoice(Guid companyId, Guid invoiceId);
        Task DeleteOffer(Guid companyId, Guid offerId);
        Task DeleteOrderConfirmation(Guid companyId, Guid orderConfirmationId);
        Task DeleteProduct(Guid companyId, Guid productId);
        Task DeleteUser(Guid companyId, string userId);
    }
}
