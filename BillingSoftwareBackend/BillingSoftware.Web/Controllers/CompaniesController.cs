using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects.UserDtos;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonBase.Extensions;
using System;
using BillingSoftware.Core.DataTransferObjects;
using System.Collections.Generic;
using CommonBase.Extensions.DtoEntityParser;

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
                    return BadRequest(new { Status = "Error", Message = $"Company not found!" });
                }
                
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { Status = "Error", Message = ex.Message });
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
                return Ok(new { Status = "Success", Message = "Company updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostCompany(Company company)
        //{
        //    try
        //    {
        //        await _uow.CompanyRepository.AddAsync(company);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompany()
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this company!" });
                }

                await _uow.CompanyRepository.Remove(companyId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Company deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-contact")]
        public async Task<IActionResult> AddContactToCompany(ContactDto contact)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a contact to this company!" });
                }

                await _uow.CompanyRepository.AddContact(compId, contact.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Contact added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-delivery-note")]
        public async Task<IActionResult> AddDeliveryNoteToCompany(DeliveryNoteDto deliveryNote)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a delivery note to this company!" });
                }

                await _uow.CompanyRepository.AddDeliveryNote(compId, deliveryNote.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Delivery Note added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-invoice")]
        public async Task<IActionResult> AddInvoiceToCompany(InvoiceDto invoice)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an invoice to this company!" });
                }

                await _uow.CompanyRepository.AddInvoice(compId, invoice.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Invoice added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-offer")]
        public async Task<IActionResult> AddOfferToCompany(OfferDto offerDto)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an offer to this company!" });
                }

                await _uow.CompanyRepository.AddOffer(compId, offerDto.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Offer added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-order-confirmation")]
        public async Task<IActionResult> AddOrderConfirmationToCompany(OrderConfirmationDto orderConfirmation)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an order confirmation to this company!" });
                }

                await _uow.CompanyRepository.AddOrderConfirmation(compId, orderConfirmation.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Order Confirmation added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-product")]
        public async Task<IActionResult> AddProductToCompany(ProductDto product)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a product to this company!" });
                }

                await _uow.CompanyRepository.AddProduct(compId, product.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Product added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToCompany(UserAddDto user)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add an user to this company!" });
                }

                await _uow.CompanyRepository.AddUser(compId, user);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "User added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPut("delete-address/{addressId}")]
        //public async Task<IActionResult> DeleteAddressFromCompany(string addressId)
        //{
        //    try
        //    {
        //        var toDeleteId = Guid.Parse(addressId);
        //        var compId = await GetCompanyIdForUser();
        //        if (compId.Equals(Guid.Empty))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this address" });
        //        }

        //        await _uow.CompanyRepository.DeleteAddress(compId, toDeleteId);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        [HttpPut("delete-contact/{contactId}")]
        public async Task<IActionResult> DeleteContactFromCompany(string contactId)
        {
            try
            {
                var toDeleteId = Guid.Parse(contactId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this contact!" });
                }

                await _uow.CompanyRepository.DeleteContact(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Contact deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-delivery-note/{deliveryNoteId}")]
        public async Task<IActionResult> DeleteDeliveryNoteFromCompany(string deliveryNoteId)
        {
            try
            {
                var toDeleteId = Guid.Parse(deliveryNoteId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this delivery note!" });
                }

                await _uow.CompanyRepository.DeleteDeliveryNote(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Delivery Note deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-invoice/{invoiceId}")]
        public async Task<IActionResult> DeleteInvoiceFromCompany(string invoiceId)
        {
            try
            {
                var toDeleteId = Guid.Parse(invoiceId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this invoice!" });
                }

                await _uow.CompanyRepository.DeleteInvoice(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Invoice deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-offer/{offerId}")]
        public async Task<IActionResult> DeleteOfferFromCompany(string offerId)
        {
            try
            {
                var toDeleteId = Guid.Parse(offerId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
                }

                await _uow.CompanyRepository.DeleteOffer(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Offer deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-order-confirmation/{orderConfirmationId}")]
        public async Task<IActionResult> DeleteOrderConfirmationFromCompany(string orderConfirmationId)
        {
            try
            {
                var toDeleteId = Guid.Parse(orderConfirmationId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this order confirmation!" });
                }

                await _uow.CompanyRepository.DeleteOrderConfirmation(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Order Confirmation deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-product/{productId}")]
        public async Task<IActionResult> DeleteProductFromCompany(string productId)
        {
            try
            {
                var toDeleteId = Guid.Parse(productId);
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this product!" });
                }

                await _uow.CompanyRepository.DeleteProduct(compId, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Product deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserFromCompany(string userId)
        {
            try
            {
                var compId = await GetCompanyIdForUser();
                if (compId.Equals(Guid.Empty))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this user!" });
                }

                await _uow.CompanyRepository.DeleteUser(compId, userId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "User deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
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
