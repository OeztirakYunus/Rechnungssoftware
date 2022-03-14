using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonBase.Extensions;
using System;

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
                if(!companyId.Equals(Guid.Empty))
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
                if(!companyId.Equals(company.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to edit this company!" });
                }

                var entity = await _uow.CompanyRepository.GetByIdAsync(company.Id);
                company.CopyProperties(entity);            
                await _uow.CompanyRepository.Update(entity);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var companyId = await GetCompanyIdForUser();
                if (!companyId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this company!" });
                }

                await _uow.CompanyRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-address")]
        public async Task<IActionResult> AddAddressToCompany(Address address)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an address to this company!" });
                }

                await _uow.CompanyRepository.AddAddress(compId, address);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-contact/{companyId}")]
        public async Task<IActionResult> AddContactToCompany(string companyId, Contact contact)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a contact to this company!" });
                }

                await _uow.CompanyRepository.AddContact(guid, contact);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-delivery-note/{companyId}")]
        public async Task<IActionResult> AddDeliveryNoteToCompany(string companyId, DeliveryNote deliveryNote)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a delivery note to this company!" });
                }

                await _uow.CompanyRepository.AddDeliveryNote(guid, deliveryNote);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-invoice/{companyId}")]
        public async Task<IActionResult> AddInvoiceToCompany(string companyId, Invoice invoice)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an invoice to this company!" });
                }

                await _uow.CompanyRepository.AddInvoice(guid, invoice);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-offer/{companyId}")]
        public async Task<IActionResult> AddOfferToCompany(string companyId, Offer offer)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an offer to this company!" });
                }

                await _uow.CompanyRepository.AddOffer(guid, offer);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-order-confirmation/{companyId}")]
        public async Task<IActionResult> AddOrderConfirmationToCompany(string companyId, OrderConfirmation orderConfirmation)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an order confirmation to this company!" });
                }

                await _uow.CompanyRepository.AddOrderConfirmation(guid, orderConfirmation);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-product/{companyId}")]
        public async Task<IActionResult> AddProductToCompany(string companyId, Product product)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a product to this company!" });
                }

                await _uow.CompanyRepository.AddProduct(guid, product);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("add-user/{companyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToCompany(string companyId, UserAddDto user)
        {
            try
            {
                var guid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an user to this company!" });
                }

                await _uow.CompanyRepository.AddUser(guid, user);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-address/{companyId}/{addressId}")]
        public async Task<IActionResult> DeleteAddressFromCompany(string companyId, string addressId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(addressId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this address" });
                }

                await _uow.CompanyRepository.DeleteAddress(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-contact/{companyId}/{contactId}")]
        public async Task<IActionResult> DeleteContactFromCompany(string companyId, string contactId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(contactId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this contact!" });
                }

                await _uow.CompanyRepository.DeleteContact(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-delivery-note/{companyId}/{deliveryNoteId}")]
        public async Task<IActionResult> DeleteDeliveryNoteFromCompany(string companyId, string deliveryNoteId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(deliveryNoteId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this delivery note!" });
                }

                await _uow.CompanyRepository.DeleteDeliveryNote(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-invoice/{companyId}/{invoiceId}")]
        public async Task<IActionResult> DeleteInvoiceFromCompany(string companyId, string invoiceId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(invoiceId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this invoice!" });
                }

                await _uow.CompanyRepository.DeleteInvoice(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-offer/{companyId}/{offerId}")]
        public async Task<IActionResult> DeleteOfferFromCompany(string companyId, string offerId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(offerId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
                }

                await _uow.CompanyRepository.DeleteOffer(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-order-confirmation/{companyId}/{orderConfirmationId}")]
        public async Task<IActionResult> DeleteOrderConfirmationFromCompany(string companyId, string orderConfirmationId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(orderConfirmationId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this order confirmation!" });
                }

                await _uow.CompanyRepository.DeleteOrderConfirmation(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-product/{companyId}/{productId}")]
        public async Task<IActionResult> DeleteProductFromCompany(string companyId, string productId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var toDeleteId = Guid.Parse(productId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this product!" });
                }

                await _uow.CompanyRepository.DeleteProduct(compGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete-user/{companyId}/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserFromCompany(string companyId, string userId)
        {
            try
            {
                var compGuid = Guid.Parse(companyId);
                var compId = await GetCompanyIdForUser();
                if (!compId.Equals(compGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this user!" });
                }

                await _uow.CompanyRepository.DeleteUser(compGuid, userId);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Guid> GetCompanyIdForUser()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            if(user.Company != null)
            {
                return user.Company.Id;
            }

            return Guid.Empty;
        }
    }
}
