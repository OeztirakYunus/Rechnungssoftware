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
        Task AddAddress(int companyId, Address address);
        Task AddContact(int companyId, Contact contact);
        Task AddDeliveryNote(int companyId, DeliveryNote deliveryNote);
        Task AddInvoice(int companyId, Invoice invoice);
        Task AddOffer(int companyId, Offer offer);
        Task AddOrderConfirmation(int companyId, OrderConfirmation orderConfirmation);
        Task AddProduct(int companyId, Product product);
        Task AddUser(int companyId, User user);
        Task DeleteAddress(int companyId, int addressId);
        Task DeleteContact(int companyId, int contactId);
        Task DeleteDeliveryNote(int companyId, int deliveryNoteId);
        Task DeleteInvoice(int companyId, int invoiceId);
        Task DeleteOffer(int companyId, int offerId);
        Task DeleteOrderConfirmation(int companyId, int orderConfirmationId);
        Task DeleteProduct(int companyId, int productId);
        Task DeleteUser(int companyId, string userId);
    }
}
