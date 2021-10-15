using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAddress(int companyId, Address address)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if(company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempAddress = await _context.Addresses.FindAsync(address.Id);
            if(tempAddress == null)
            {
                var res = await _context.Addresses.AddAsync(address);
                tempAddress = res.Entity;
            }

            company.Addresses.Add(tempAddress);
        }

        public async Task AddContact(int companyId, Contact contact)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempContact = await _context.Contacts.FindAsync(contact.Id);
            if (tempContact == null)
            {
                var res = await _context.Contacts.AddAsync(contact);
                tempContact = res.Entity;
            }

            company.Contacts.Add(tempContact);
        }

        public async Task AddDeliveryNote(int companyId, DeliveryNote deliveryNote)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempDeliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNote.Id);
            if (tempDeliveryNote == null)
            {
                var res = await _context.DeliveryNotes.AddAsync(deliveryNote);
                tempDeliveryNote = res.Entity;
            }

            company.DeliveryNotes.Add(tempDeliveryNote);
        }

        public async Task AddInvoice(int companyId, Invoice invoice)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempInvoice = await _context.Invoices.FindAsync(invoice.Id);
            if (tempInvoice == null)
            {
                var res = await _context.Invoices.AddAsync(invoice);
                tempInvoice = res.Entity;
            }

            company.Invoices.Add(tempInvoice);
        }

        public async Task AddOffer(int companyId, Offer offer)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOffer = await _context.Offers.FindAsync(offer.Id);
            if (tempOffer == null)
            {
                var res = await _context.Offers.AddAsync(offer);
                tempOffer = res.Entity;
            }

            company.Offers.Add(tempOffer);
        }

        public async Task AddOrderConfirmation(int companyId, OrderConfirmation orderConfirmation)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOrderConfirmation = await _context.OrderConfirmations.FindAsync(orderConfirmation.Id);
            if (tempOrderConfirmation == null)
            {
                var res = await _context.OrderConfirmations.AddAsync(orderConfirmation);
                tempOrderConfirmation = res.Entity;
            }

            company.OrderConfirmations.Add(tempOrderConfirmation);
        }

        public async Task AddProduct(int companyId, Product product)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempProduct = await _context.Products.FindAsync(product.Id);
            if (tempProduct == null)
            {
                var res = await _context.Products.AddAsync(product);
                tempProduct = res.Entity;
            }

            company.Products.Add(tempProduct);
        }

        public async Task AddUser(int companyId, User user)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempUser = await _context.Users.FindAsync(user.Id);
            if (tempUser == null)
            {
                var res = await _context.Users.AddAsync(user);
                tempUser = res.Entity;
            }

            company.Users.Add(tempUser);
        }

        public async Task DeleteAddress(int companyId, int addressId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempAddress = await _context.Addresses.FindAsync(addressId);
            if (tempAddress == null)
            {
                throw new EntityNotFoundException("Address does not exist.");
            }

            _context.Addresses.Remove(tempAddress);
        }

        public async Task DeleteContact(int companyId, int contactId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempContact = await _context.Contacts.FindAsync(contactId);
            if (tempContact == null)
            {
                throw new EntityNotFoundException("Contact does not exist.");
            }

            _context.Contacts.Remove(tempContact);
        }

        public async Task DeleteDeliveryNote(int companyId, int deliveryNoteId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempDeliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNoteId);
            if (tempDeliveryNote == null)
            {
                throw new EntityNotFoundException("Delivery Note does not exist.");
            }

            _context.DeliveryNotes.Remove(tempDeliveryNote);
        }

        public async Task DeleteInvoice(int companyId, int invoiceId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempInvoice = await _context.Invoices.FindAsync(invoiceId);
            if (tempInvoice == null)
            {
                throw new EntityNotFoundException("Invoice does not exist.");
            }

            _context.Invoices.Remove(tempInvoice);
        }

        public async Task DeleteOffer(int companyId, int offerId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOffer = await _context.Offers.FindAsync(offerId);
            if (tempOffer == null)
            {
                throw new EntityNotFoundException("Offer does not exist.");
            }

            _context.Offers.Remove(tempOffer);
        }

        public async Task DeleteOrderConfirmation(int companyId, int orderConfirmationId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOrderConfirmation = await _context.OrderConfirmations.FindAsync(orderConfirmationId);
            if (tempOrderConfirmation == null)
            {
                throw new EntityNotFoundException("Order Confirmation does not exist.");
            }

            _context.OrderConfirmations.Remove(tempOrderConfirmation);
        }

        public async Task DeleteProduct(int companyId, int productId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempProduct = await _context.Products.FindAsync(productId);
            if (tempProduct == null)
            {
                throw new EntityNotFoundException("Product does not exist.");
            }

            _context.Products.Remove(tempProduct);
        }

        public async Task DeleteUser(int companyId, int userId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempUser = await _context.Users.FindAsync(userId);
            if (tempUser == null)
            {
                throw new EntityNotFoundException("User does not exist.");
            }

            _context.Users.Remove(tempUser);
        }

        override public Task<Company[]> GetAllAsync()
        {
            return _context.Companies.ToArrayAsync();
        }

    }
}
