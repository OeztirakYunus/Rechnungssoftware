using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CompaniesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<Company>> GetCompany()
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if(companyId != -999)
                {
                    var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
                    return Ok(company);
                }
                else
                {
                    return BadRequest("No Company found!");
                }
                
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCompany(Company company)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if(companyId != company.Id)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to edit this company!" });
                }

                //var entity = await _uow.CompanyRepository.GetByIdAsync(company.Id);
                //entity = entity as Company;
                //entity.CopyProperties(company);
                _uow.CompanyRepository.Update(company);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompany(Company company)
        {
            try
            {
                await _uow.CompanyRepository.AddAsync(company);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId != id)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this company!" });
                }

                await _uow.CompanyRepository.Remove(id);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-address/{companyId}")]
        public async Task<IActionResult> AddAddressToCompany(int companyId, Address address)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an address to this company!" });
                }

                await _uow.CompanyRepository.AddAddress(companyId, address);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-contact/{companyId}")]
        public async Task<IActionResult> AddContactToCompany(int companyId, Contact contact)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a contact to this company!" });
                }

                await _uow.CompanyRepository.AddContact(companyId, contact);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-delivery-note/{companyId}")]
        public async Task<IActionResult> AddDeliveryNoteToCompany(int companyId, DeliveryNote deliveryNote)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a delivery note to this company!" });
                }

                await _uow.CompanyRepository.AddDeliveryNote(companyId, deliveryNote);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-invoice/{companyId}")]
        public async Task<IActionResult> AddInvoiceToCompany(int companyId, Invoice invoice)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an invoice to this company!" });
                }

                await _uow.CompanyRepository.AddInvoice(companyId, invoice);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-offer/{companyId}")]
        public async Task<IActionResult> AddOfferToCompany(int companyId, Offer offer)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an offer to this company!" });
                }

                await _uow.CompanyRepository.AddOffer(companyId, offer);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-order-confirmation/{companyId}")]
        public async Task<IActionResult> AddOrderConfirmationToCompany(int companyId, OrderConfirmation orderConfirmation)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an order confirmation to this company!" });
                }

                await _uow.CompanyRepository.AddOrderConfirmation(companyId, orderConfirmation);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-product/{companyId}")]
        public async Task<IActionResult> AddProductToCompany(int companyId, Product product)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a product to this company!" });
                }

                await _uow.CompanyRepository.AddProduct(companyId, product);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-user/{companyId}")]
        public async Task<IActionResult> AddUserToCompany(int companyId, User user)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an user to this company!" });
                }

                await _uow.CompanyRepository.AddUser(companyId, user);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-address/{companyId}/{addressId}")]
        public async Task<IActionResult> DeleteAddressFromCompany(int companyId, int addressId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this address" });
                }

                await _uow.CompanyRepository.DeleteAddress(companyId, addressId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-contact/{companyId}/{contactId}")]
        public async Task<IActionResult> DeleteContactFromCompany(int companyId, int contactId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this contact!" });
                }

                await _uow.CompanyRepository.DeleteContact(companyId, contactId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-delivery-note/{companyId}/{deliveryNoteId}")]
        public async Task<IActionResult> DeleteDeliveryNoteFromCompany(int companyId, int deliveryNoteId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this delivery note!" });
                }

                await _uow.CompanyRepository.DeleteDeliveryNote(companyId, deliveryNoteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-invoice/{companyId}/{invoiceId}")]
        public async Task<IActionResult> DeleteInvoiceFromCompany(int companyId, int invoiceId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this invoice!" });
                }

                await _uow.CompanyRepository.DeleteInvoice(companyId, invoiceId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-offer/{companyId}/{offerId}")]
        public async Task<IActionResult> DeleteOfferFromCompany(int companyId, int offerId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
                }

                await _uow.CompanyRepository.DeleteOffer(companyId, offerId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-order-confirmation/{companyId}/{orderConfirmationId}")]
        public async Task<IActionResult> DeleteOrderConfirmationFromCompany(int companyId, int orderConfirmationId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this order confirmation!" });
                }

                await _uow.CompanyRepository.DeleteOrderConfirmation(companyId, orderConfirmationId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-product/{companyId}/{productId}")]
        public async Task<IActionResult> DeleteProductFromCompany(int companyId, int productId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this product!" });
                }

                await _uow.CompanyRepository.DeleteProduct(companyId, productId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-user/{companyId}/{userId}")]
        public async Task<IActionResult> DeleteUserFromCompany(int companyId, string userId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (companyId != compId)
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this user!" });
                }

                await _uow.CompanyRepository.DeleteUser(companyId, userId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<int> GetCompanyIdForUser()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            if(user.Company != null)
            {
                return user.Company.Id;
            }

            return -999;
        }
    }
}
