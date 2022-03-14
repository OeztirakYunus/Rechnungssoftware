using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly UserManager<User> _userManager;

        public CompanyRepository(ApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task AddAddress(Guid companyId, Address address)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if(company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempAddress = await _context.Addresses.FindAsync(address.Id);
            if(tempAddress == null)
            {
                address.CompanyId = companyId;
                var res = await _context.Addresses.AddAsync(address);
                tempAddress = res.Entity;
            }
            company.Addresses.Add(tempAddress);
        }

        public async Task AddContact(Guid companyId, Contact contact)
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

        public async Task AddDeliveryNote(Guid companyId, DeliveryNote deliveryNote)
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

        public async Task AddInvoice(Guid companyId, Invoice invoice)
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

        public async Task AddOffer(Guid companyId, Offer offer)
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

        public async Task AddOrderConfirmation(Guid companyId, OrderConfirmation orderConfirmation)
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

        public async Task AddProduct(Guid companyId, Product product)
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

        public async Task AddUser(Guid companyId, UserAddDto user)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempUser = await _userManager.FindByEmailAsync(user.Email);
            if (tempUser != null)
            {
                throw new Exception($"User with email {user.Email} exists already!");
            }

            User userToAdd = new User
            {
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.Email,
                Company = company,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            var resultUser = await _userManager.CreateAsync(userToAdd, user.Password);
            var resultRole = await _userManager.AddToRoleAsync(userToAdd, user.Role.ToString());

            if (!resultUser.Succeeded)
            {
                throw new Exception("Error while adding user!");
            }
            else if (!resultRole.Succeeded)
            {
                throw new Exception("Error while adding role!");
            }

            company.Users.Add(userToAdd as User);
        }

        public async Task DeleteAddress(Guid companyId, Guid addressId)
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

        public async Task DeleteContact(Guid companyId, Guid contactId)
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

        public async Task DeleteDeliveryNote(Guid companyId, Guid deliveryNoteId)
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

        public async Task DeleteInvoice(Guid companyId, Guid invoiceId)
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

        public async Task DeleteOffer(Guid companyId, Guid offerId)
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

        public async Task DeleteOrderConfirmation(Guid companyId, Guid orderConfirmationId)
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

        public async Task DeleteProduct(Guid companyId, Guid productId)
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

        public async Task DeleteUser(Guid companyId, string userId)
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

        public override async Task Remove(Guid id)
        {
            var company = await GetByIdAsync(id);
            foreach (var item in company.Products)
            {
                await DeleteProduct(id, item.Id);
            }
            foreach (var item in company.Addresses)
            {
                await DeleteAddress(id, item.Id);
            }
            foreach (var item in company.Offers)
            {
                await DeleteOffer(id, item.Id);
            }
            foreach (var item in company.Invoices)
            {
                await DeleteInvoice(id, item.Id);
            }
            foreach (var item in company.OrderConfirmations)
            {
                await DeleteOrderConfirmation(id, item.Id);
            }
            foreach (var item in company.DeliveryNotes)
            {
                await DeleteDeliveryNote(id, item.Id);
            }
            foreach (var item in company.Users)
            {
                await DeleteUser(id, item.Id);
            }
            foreach (var item in company.Contacts)
            {
                await DeleteContact(id, item.Id);
            }  
            await base.Remove(id);
        }

        override public async Task<Company[]> GetAllAsync()
        {
            return await _context.Companies
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<Company> GetByIdAsync(Guid id)
        {
            return await _context.Companies
                    .IncludeAllRecursively()
                    .SingleOrDefaultAsync(x => x.Id == id);
        }

        //public override async Task Update(Company entity)
        //{
        //    entity.Addresses.ForEach(async i => {
        //        var address = await _context.Addresses.FindAsync(i.Id);
        //        if(address == null)
        //        {
        //            address = i;
        //        }
        //        await Update(address);
        //    });
           
        //    await base.Update(entity);
        //}
    }
}
