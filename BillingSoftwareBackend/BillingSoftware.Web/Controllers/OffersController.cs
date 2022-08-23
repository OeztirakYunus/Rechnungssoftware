using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using CommonBase.DocumentCreators;
using CommonBase.Extensions;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public OffersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var offers = await _uow.OfferRepository.GetAllAsync();
                offers = offers.Where(i => user.Company.Offers.Any(a => a.Id.Equals(i.Id))).ToArray();

                return Ok(offers);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOffer(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this offer!" });
                }

                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                return Ok(offer);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutOffer(Offer offer)
        {
            try
            {
                if (!await CheckAuthorization(offer.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this offer!" });
                }

                var entity = await _uow.OfferRepository.GetByIdAsync(offer.Id);
                offer.CopyProperties(entity);
                await _uow.OfferRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Offer updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostOffer(Offer offer)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(offer.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this offer!" });
        //        }

        //        await _uow.OfferRepository.AddAsync(offer);
        //        await _uow.SaveChangesAsync();
        //        return Ok(new { Status = "Success", Message = "Invoice updated." });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOffer(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
        //        }

        //        await _uow.OfferRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("offer-to-order-confirmation/{offerId}")]
        public async Task<IActionResult> OfferToOrderConfirmation(string offerId)
        {
            try
            {
                var guid = Guid.Parse(offerId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to transform this offer!" });
                }

                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                var orderConfirmation = await _uow.OfferRepository.OfferToOrderConfirmation(offer);
                await _uow.SaveChangesAsync();
                return Ok(orderConfirmation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-word/{offerId}")]
        public async Task<IActionResult> GetOfferAsWord(string offerId)
        {
            try
            {
                var guid = Guid.Parse(offerId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this offer as word!" });
                }
                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                var company = await _uow.CompanyRepository.GetByIdAsync(offer.CompanyId);
                var (bytes, path) = await DocxCreator.CreateWordForOffer(offer, company);          
                return File(bytes, "application/docx", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-pdf/{offerId}")]
        public async Task<IActionResult> GetOfferAsPdf(string offerId)
        {
            try
            {
                var guid = Guid.Parse(offerId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this offer as pdf!" });
                }
                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                var company = await _uow.CompanyRepository.GetByIdAsync(offer.CompanyId);
                var (bytes, path) = await PdfCreator.CreatePdfForOffer(offer, company);
                return File(bytes, "application/pdf", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid offerId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Offers.Any(i => i.Id.Equals(offerId));
        }
    }
}
