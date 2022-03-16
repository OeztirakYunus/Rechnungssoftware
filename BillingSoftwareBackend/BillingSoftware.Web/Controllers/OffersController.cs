using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using CommonBase.Extensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private const string TEMPLATE = @"U:\Yunus\Rechnungssoftware\BillingSoftwareBackend\BillingSoftware.Web\templates\default\offer_template.docx";
        private const string SAVE_DIRECTORY = @"U:\Yunus\Rechnungssoftware\BillingSoftwareBackend\BillingSoftware.Web\documents\offers";


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
                return BadRequest(ex.Message);
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
                return offer;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
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
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOffer(Offer offer)
        {
            try
            {
                if (!await CheckAuthorization(offer.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this offer!" });
                }

                await _uow.OfferRepository.AddAsync(offer);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
                }

                await _uow.OfferRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                return BadRequest(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        [HttpGet("get-as-word/{offerId}")]
        public async Task GetOfferAsWord(string offerId)
        {
            await GetAsWord(offerId);
        }

        private async Task GetAsWord(string offerId)
        {
            var guid = Guid.Parse(offerId);
            var offer = await _uow.OfferRepository.GetByIdAsync(guid);
            var newFile = SAVE_DIRECTORY + @$"\Angebot_{offer.OfferNumber}.docx";
            System.IO.File.Copy(TEMPLATE, newFile);

            //Footer
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.FooterParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);
                docText = docText.Replace("company.street", offer.Company.Addresses[0].Street);
                docText = docText.Replace("company.postCode", offer.Company.Addresses[0].ZipCode);
                docText = docText.Replace("company.city", offer.Company.Addresses[0].City);
                docText = docText.Replace("company.ustNumber", offer.Company.UstNumber);
                docText = docText.Replace("company.phoneNumber", offer.Company.PhoneNumber);
                docText = docText.Replace("company.email", offer.Company.Email);
                docText = docText.Replace("company.iban", offer.Company.Iban);
                docText = docText.Replace("company.bankName", offer.Company.BankName);
                docText = docText.Replace("company.bic", offer.Company.Bic);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.FooterParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }

            //Header
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.HeaderParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.HeaderParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }

            //Body
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);
                docText = docText.Replace("company.street", offer.Company.Addresses[0].Street);
                docText = docText.Replace("company.postCode", offer.Company.Addresses[0].ZipCode);
                docText = docText.Replace("company.city", offer.Company.Addresses[0].City);
                docText = docText.Replace("client.street", offer.DocumentInformation.Client.Addresses[0].Street);
                docText = docText.Replace("client.postCode", offer.DocumentInformation.Client.Addresses[0].ZipCode);
                docText = docText.Replace("client.city", offer.DocumentInformation.Client.Addresses[0].City);
                docText = docText.Replace("client.gender", offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Male ? "Herr" : "Frau");
                docText = docText.Replace("offer.date", offer.OfferDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.validUntil", offer.ValidUntil.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.subject", offer.Subject);
                docText = docText.Replace("offer.flowText", offer.FlowText);
                docText = docText.Replace("offer.headerText", offer.HeaderText);
                docText = docText.Replace("contact.name", offer.DocumentInformation.ContactPerson.LastName + " " + offer.DocumentInformation.ContactPerson.FirstName);
                docText = docText.Replace("offer.priceNet", offer.DocumentInformation.TotalPriceNet.ToString());
                docText = docText.Replace("offer.ustPrice", (offer.DocumentInformation.TotalPriceNet * 0.2).ToString());
                docText = docText.Replace("offer.total", offer.DocumentInformation.TotalPriceGross.ToString());

                var nameOfOrganisation = "";
                if (!string.IsNullOrEmpty(offer.DocumentInformation.Client.NameOfOrganisation))
                {
                    nameOfOrganisation = offer.DocumentInformation.Client.NameOfOrganisation;
                }
                docText = docText.Replace("client.nameOfOrganisation", nameOfOrganisation);

                var clientName = "";
                var greeting = "geehrte Damen und Herren,";
                if (!string.IsNullOrEmpty(offer.DocumentInformation.Client.FirstName) && !string.IsNullOrEmpty(offer.DocumentInformation.Client.LastName))
                {
                    clientName = offer.DocumentInformation.Client.LastName + " " + offer.DocumentInformation.Client.FirstName;
                    if (offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Male)
                    {
                        greeting = "geehrter Herr " + clientName + ",";
                    }
                    else if (offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Female)
                    {
                        greeting = "geehrte Frau " + clientName + ",";
                    }
                }
                docText = docText.Replace("client.name", clientName);
                docText = docText.Replace("client.greeting", greeting);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
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
