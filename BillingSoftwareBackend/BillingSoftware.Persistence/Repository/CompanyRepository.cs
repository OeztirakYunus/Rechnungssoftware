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
    public class CompanyRepository : ICompanyRepository
    {
        ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAddress(int companyId, Address address)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if(company == null)
            {
                throw new CompanyNotFoundException();
            }

            var tempAddress = await _context.Addresses.FindAsync(address.Id);
            if(tempAddress == null)
            {
                var res = await _context.Addresses.AddAsync(address);
                tempAddress = res.Entity;
            }

            company.Addresses.Add(tempAddress);
        }

        public async Task AddAsync(Company entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddContact(int companyId, Contact contact)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
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
                throw new CompanyNotFoundException();
            }

            var tempUser = await _context.Users.FindAsync(user.Id);
            if (tempUser == null)
            {
                var res = await _context.Users.AddAsync(user);
                tempUser = res.Entity;
            }

            company.Users.Add(tempUser);
        }

        public Task DeleteAddress(int companyId, Address address)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContact(int companyId, Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeliveryNote(int companyId, DeliveryNote deliveryNote)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInvoice(int companyId, Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOffer(int companyId, Offer offer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderConfirmation(int companyId, OrderConfirmation orderConfirmation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int companyId, Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int companyId, User user)
        {
            throw new NotImplementedException();
        }

        public Task<Company[]> GetAllAsync()
        {
            return _context.Companies.ToArrayAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public void Remove(Company entity)
        {
            _context.Companies.Remove(entity);
        }

        public Company Update(Company entity)
        {
            return _context.Companies.Update(entity).Entity;
        }
    }
}
