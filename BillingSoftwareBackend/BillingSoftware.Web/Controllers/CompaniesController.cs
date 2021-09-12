using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company.Users == null)
            {
                System.Console.WriteLine("Is null");
            }
            else
            {
                System.Console.WriteLine(company.Users.First().FirstName);
            }

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }
        #region CompanyListOperations
        [HttpPut("add-user/{userId}/{companyId}")]
        public async Task<IActionResult> AddUserToCompany(int userId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var user = await _context.Users.FindAsync(userId);

            if (company == null || user == null)
            {
                return BadRequest("User or Company not found.");
            }

            company.AddUser(user);
            return await SaveModifiedCompany(company, user);
        }

        [HttpPut("delete-user/{userId}/{companyId}")]
        public async Task<IActionResult> DeleteUserFromCompany(int userId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var user = await _context.Users.FindAsync(userId);

            if (company == null || user == null)
            {
                return BadRequest("User or Company not found.");
            }

            company.DeleteUser(user);
            return await SaveModifiedCompany(company, user);
        }

        [HttpPut("add-offer/{offerId}/{companyId}")]
        public async Task<IActionResult> AddOfferToCompany(int offerId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var offer = await _context.Offers.FindAsync(offerId);

            if (company == null || offer == null)
            {
                return BadRequest("Offer or Company not found.");
            }

            company.Offers.Add(offer);
            return await SaveModifiedCompany(company, offer);
        }

        [HttpPut("delete-offer/{offerId}/{companyId}")]
        public async Task<IActionResult> DeleteOfferFromCompany(int offerId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var offer = await _context.Offers.FindAsync(offerId);

            if (company == null || offer == null)
            {
                return BadRequest("Offer or Company not found.");
            }

            company.Offers.Remove(offer);
            return await SaveModifiedCompany(company, offer);
        }

        [HttpPut("add-contact/{contactId}/{companyId}")]
        public async Task<IActionResult> AddContactToCompany(int contactId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var contact = await _context.Contacts.FindAsync(contactId);

            if (company == null || contact == null)
            {
                return BadRequest("Contact or Company not found.");
            }

            company.Contacts.Add(contact);
            return await SaveModifiedCompany(company, contact);
        }

        [HttpPut("delete-contact/{contactId}/{companyId}")]
        public async Task<IActionResult> DeleteContactFromCompany(int contactId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var contact = await _context.Contacts.FindAsync(contactId);

            if (company == null || contact == null)
            {
                return BadRequest("Contact or Company not found.");
            }

            company.Contacts.Remove(contact);
            return await SaveModifiedCompany(company, contact);
        }

        [HttpPut("add-delivery-note/{deliveryNoteId}/{companyId}")]
        public async Task<IActionResult> AddDeliveryNoteToCompany(int deliveryNoteId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var deliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNoteId);

            if (company == null || deliveryNote == null)
            {
                return BadRequest("Delivery note or Company not found.");
            }

            company.DeliveryNotes.Add(deliveryNote);
            return await SaveModifiedCompany(company, deliveryNote);
        }

        [HttpPut("delete-delivery-note/{deliveryNoteId}/{companyId}")]
        public async Task<IActionResult> DeleteDeliveryNoteFromCompany(int deliveryNoteId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var deliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNoteId);

            if (company == null || deliveryNote == null)
            {
                return BadRequest("Delivery note or Company not found.");
            }

            company.DeliveryNotes.Remove(deliveryNote);
            return await SaveModifiedCompany(company, deliveryNote);
        }

        [HttpPut("add-invoice/{invoiceId}/{companyId}")]
        public async Task<IActionResult> AddInvoiceToCompany(int invoiceId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var invoice = await _context.Invoices.FindAsync(invoiceId);

            if (company == null || invoice == null)
            {
                return BadRequest("Invoice or Company not found.");
            }

            company.Invoices.Add(invoice);
            return await SaveModifiedCompany(company, invoice);
        }

        [HttpPut("delete-invoice/{invoiceId}/{companyId}")]
        public async Task<IActionResult> DeleteInvoiceFromCompany(int invoiceId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var invoice = await _context.Invoices.FindAsync(invoiceId);

            if (company == null || invoice == null)
            {
                return BadRequest("Invoice or Company not found.");
            }

            company.Invoices.Remove(invoice);
            return await SaveModifiedCompany(company, invoice);
        }

        [HttpPut("add-order-confirmation/{orderConfirmationId}/{companyId}")]
        public async Task<IActionResult> AddOrderConfirmationToCompany(int orderConfirmationId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var orderConfirmation = await _context.OrderConfirmations.FindAsync(orderConfirmationId);

            if (company == null || orderConfirmation == null)
            {
                return BadRequest("Order confirmation or Company not found.");
            }

            company.OrderConfirmations.Add(orderConfirmation);
            return await SaveModifiedCompany(company, orderConfirmation);
        }

        [HttpPut("delete-order-confirmation/{orderConfirmationId}/{companyId}")]
        public async Task<IActionResult> DeleteOrderConfirmationFromCompany(int orderConfirmationId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var orderConfirmation = await _context.OrderConfirmations.FindAsync(orderConfirmationId);

            if (company == null || orderConfirmation == null)
            {
                return BadRequest("Order confirmation or Company not found.");
            }

            company.OrderConfirmations.Remove(orderConfirmation);
            return await SaveModifiedCompany(company, orderConfirmation);
        }

        [HttpPut("add-product/{productId}/{companyId}")]
        public async Task<IActionResult> AddProductToCompany(int productId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var product = await _context.Products.FindAsync(productId);

            if (company == null || product == null)
            {
                return BadRequest("Product or Company not found.");
            }

            company.Products.Add(product);
            return await SaveModifiedCompany(company, product);
        }

        [HttpPut("delete-product/{invoiceId}/{companyId}")]
        public async Task<IActionResult> DeleteProductFromCompany(int productId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var product = await _context.Products.FindAsync(productId);

            if (company == null || product == null)
            {
                return BadRequest("Product or Company not found.");
            }

            company.Products.Remove(product);
            return await SaveModifiedCompany(company, product);
        }

        [HttpPut("add-address/{addressId}/{companyId}")]
        public async Task<IActionResult> AddAddressToCompany(int addressId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var address = await _context.Addresses.FindAsync(addressId);

            if (company == null || address == null)
            {
                return BadRequest("Address or Company not found.");
            }

            company.Addresses.Add(address);
            return await SaveModifiedCompany(company, address);
        }

        [HttpPut("delete-address/{addressId}/{companyId}")]
        public async Task<IActionResult> DeleteAddressFromCompany(int addressId, int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            var address = await _context.Addresses.FindAsync(addressId);

            if (company == null || address == null)
            {
                return BadRequest("Address or Company not found.");
            }

            company.Addresses.Remove(address);
            return await SaveModifiedCompany(company, address);
        }

        private async Task<IActionResult> SaveModifiedCompany(Company company, object obj)
        {
            _context.Entry(company).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Company successfully modified");
        }
        #endregion
        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
