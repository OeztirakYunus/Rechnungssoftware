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
        Task DeleteAddress(int companyId, Address address);
        Task DeleteContact(int companyId, Contact contact);
        Task DeleteDeliveryNote(int companyId, DeliveryNote deliveryNote);
        Task DeleteInvoice(int companyId, Invoice invoice);
        Task DeleteOffer(int companyId, Offer offer);
        Task DeleteOrderConfirmation(int companyId, OrderConfirmation orderConfirmation);
        Task DeleteProduct(int companyId, Product product);
        Task DeleteUser(int companyId, User user);
    }
}
