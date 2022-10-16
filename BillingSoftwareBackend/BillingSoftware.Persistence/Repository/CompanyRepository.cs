using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects.UserDtos;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                contact.CompanyId = companyId;
                await _context.Contacts.AddAsync(contact);
            }
        }

        public async Task AddDeliveryNote(Guid companyId, DeliveryNote deliveryNote)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(companyId)).SingleOrDefault();
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempDeliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNote.Id);
            if (tempDeliveryNote == null)
            {
                deliveryNote.CompanyId = companyId;
                if(string.IsNullOrEmpty(deliveryNote.DeliveryNoteNumber))
                {
                    deliveryNote.DeliveryNoteNumber = "DN" + DateTime.Now.ToString("yy") + cDocumentCounter.DeliveryNoteCounter.ToString().PadLeft(5, '0');
                    cDocumentCounter.DeliveryNoteCounter++;
                    await Update(company);
                }
                if (string.IsNullOrEmpty(deliveryNote.Subject))
                {
                    deliveryNote.Subject = "Lieferschein " + deliveryNote.DeliveryNoteNumber;
                }
                if (string.IsNullOrEmpty(deliveryNote.HeaderText))
                {
                    deliveryNote.HeaderText = "Vielen Dank für die Zusammenarbeit. Vereinbarungsgemäß liefern wir Ihnen folgende Waren:";
                }
                if (string.IsNullOrEmpty(deliveryNote.FlowText))
                {
                    deliveryNote.FlowText = "Die gelieferte Ware bleibt bis zu vollständigen Bezahlung unser Eigentum.";
                }
                await _context.DeliveryNotes.AddAsync(deliveryNote);
                await Update(cDocumentCounter);
            }
        }

        public async Task AddInvoice(Guid companyId, Invoice invoice)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(companyId)).SingleOrDefault();
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempInvoice = await _context.Invoices.FindAsync(invoice.Id);
            if (tempInvoice == null)
            {
                invoice.CompanyId = companyId;
                if (string.IsNullOrEmpty(invoice.InvoiceNumber))
                {
                    invoice.InvoiceNumber = "I" + DateTime.Now.ToString("yy") + cDocumentCounter.InvoiceCounter.ToString().PadLeft(5, '0');
                    cDocumentCounter.InvoiceCounter++;
                    await Update(company);
                }
                if (string.IsNullOrEmpty(invoice.Subject))
                {
                    invoice.Subject = "Rechnung " + invoice.InvoiceNumber;
                }
                if (string.IsNullOrEmpty(invoice.HeaderText))
                {
                    invoice.HeaderText = "Vielen Dank für Ihren Auftrag. Wir berechnen Ihnen folgende Leistung:";
                }
                if (string.IsNullOrEmpty(invoice.FlowText))
                {
                    invoice.FlowText = "Zahlbar sofort ohne Abzug. Für Rückfragen zu dieser Rechnung stehen wir gerne jederzeit zur Verfügung.";
                }
                await _context.Invoices.AddAsync(invoice);
                await Update(cDocumentCounter);
            }
        }

        public async Task AddOffer(Guid companyId, Offer offer)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(companyId)).SingleOrDefault();
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOffer = await _context.Offers.FindAsync(offer.Id);
            if (tempOffer == null)
            {
                offer.CompanyId = companyId;
                if (string.IsNullOrEmpty(offer.OfferNumber))
                {
                    offer.OfferNumber = "O" + DateTime.Now.ToString("yy") + cDocumentCounter.OfferCounter.ToString().PadLeft(5, '0');
                    cDocumentCounter.OfferCounter++;
                    await Update(company);
                }
                if (string.IsNullOrEmpty(offer.Subject))
                {
                    offer.Subject = "Angebot " + offer.OfferNumber;
                }
                if (string.IsNullOrEmpty(offer.HeaderText))
                {
                    offer.HeaderText = "Vielen Dank für Ihre Anfrage und das damit verbundene Interesse an einer Zusammenarbeit.\nGerne unterbreiten wir Ihnen folgendes Angebot:";
                }
                if (string.IsNullOrEmpty(offer.FlowText))
                {
                    offer.FlowText = "Wir hoffen, dass das Angebot Ihren Anforderungen entspricht und würden uns über eine zukünftige Zusammenarbeit sehr freuen. Für Rückfragen und weitere Informationen stehen wir gerne jederzeit zur Verfügung.";
                }
                await _context.Offers.AddAsync(offer);
                await Update(cDocumentCounter);
            }
        }

        public async Task AddOrderConfirmation(Guid companyId, OrderConfirmation orderConfirmation)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var cDocumentCounters = await _context.CompanyDocumentCounters.ToArrayAsync();
            var cDocumentCounter = cDocumentCounters.Where(i => i.CompanyId.Equals(companyId)).SingleOrDefault();
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempOrderConfirmation = await _context.OrderConfirmations.FindAsync(orderConfirmation.Id);
            if (tempOrderConfirmation == null)
            {
                orderConfirmation.CompanyId = companyId;
                if (string.IsNullOrEmpty(orderConfirmation.OrderConfirmationNumber))
                {
                    orderConfirmation.OrderConfirmationNumber = "OC" + DateTime.Now.ToString("yy") + cDocumentCounter.OrderConfirmationCounter.ToString().PadLeft(5, '0');
                    cDocumentCounter.OrderConfirmationCounter++;
                    await Update(company);
                }
                if (string.IsNullOrEmpty(orderConfirmation.Subject))
                {
                    orderConfirmation.Subject = "Auftragsbestätigung " + orderConfirmation.OrderConfirmationNumber;
                }
                if (string.IsNullOrEmpty(orderConfirmation.HeaderText))
                {
                    orderConfirmation.HeaderText = "Vielen Dank für Ihr Vertrauen und den Auftrag. Gemäß unserem Angebot erbringen wir folgende Leistungen:";
                }
                if (string.IsNullOrEmpty(orderConfirmation.FlowText))
                {
                    orderConfirmation.FlowText = "Bei Rückfragen stehen wir selbstverständlich jeder Zeit gerne zur Verfügung.";
                }
                await _context.OrderConfirmations.AddAsync(orderConfirmation);
                await Update(cDocumentCounter);
            }
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
                product.CompanyId = companyId;
                await _context.Products.AddAsync(product);
            }
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
                CompanyId = companyId,
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

        private async Task DeleteCompanyDocumentCounter(Guid companyId, Guid cDocumentCounterId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempDocCounter = await _context.CompanyDocumentCounters.FindAsync(cDocumentCounterId);
            if (tempDocCounter == null)
            {
                throw new EntityNotFoundException("DocCounter does not exist.");
            }

            _context.CompanyDocumentCounters.Remove(tempDocCounter);
        }
        private async Task DeleteFile(Guid companyId, Guid fileId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new EntityNotFoundException("Company does not exist.");
            }

            var tempFile = await _context.BSFiles.FindAsync(fileId);
            if (tempFile == null)
            {
                throw new EntityNotFoundException("File does not exist.");
            }

            _context.BSFiles.Remove(tempFile);
        }

        public override async Task Remove(Guid id)
        {
            var company = await GetByIdAsync(id);
            foreach (var item in company.Products)
            {
                await DeleteProduct(id, item.Id);
            }

            await DeleteCompanyDocumentCounter(id, company.CompanyDocumentCounter.Id);
            await DeleteAddress(id, company.AddressId);
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
            foreach (var item in company.Files)
            {
                await DeleteFile(id, item.Id);
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
        public override async Task Update(Company entity)
        {
            AddressRepository addressRepository = new AddressRepository(_context);
            BankInformationRepository bankInformationRepository = new BankInformationRepository(_context);
            
            var comp = await GetByIdAsync(entity.Id);
            var add = await addressRepository.GetByIdAsync(entity.AddressId);
            var bank = await bankInformationRepository.GetByIdAsync(entity.BankInformationId);

            entity.CopyProperties(comp);
            entity.Address.CopyProperties(add);
            entity.BankInformation.CopyProperties(bank);


            await Update(bank);
            await Update(add);
            await base.Update(comp);
        }
    }
}
