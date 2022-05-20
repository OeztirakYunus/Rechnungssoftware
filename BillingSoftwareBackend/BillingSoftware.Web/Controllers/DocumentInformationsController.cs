using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using CommonBase.DtoEntityParser;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentInformationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DocumentInformationsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentInformations>>> GetDocumentInformations()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var result = new List<DocumentInformations>();
                var documentInformations = await _uow.DocumentInformationsRepository.GetAllAsync();
                var offers = await _uow.OfferRepository.GetAllAsync();
                var invoices = await _uow.InvoiceRepository.GetAllAsync();
                var orderConfirmations = await _uow.OrderConfirmationRepository.GetAllAsync();
                var deliveryNotes = await _uow.DeliveryNoteRepository.GetAllAsync();

                var linqResult = documentInformations.Where(i => offers.Any(x => x.DocumentInformationId.Equals(i))).ToArray();
                result.AddRange(linqResult);
                linqResult = documentInformations.Where(i => invoices.Any(x => x.DocumentInformationId.Equals(i))).ToArray();
                result.AddRange(linqResult);
                linqResult = documentInformations.Where(i => orderConfirmations.Any(x => x.DocumentInformationId.Equals(i))).ToArray();
                result.AddRange(linqResult);
                linqResult = documentInformations.Where(i => deliveryNotes.Any(x => x.DocumentInformationsId.Equals(i))).ToArray();
                result.AddRange(linqResult);

                return Ok(result.DistinctBy(i => i.Id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentInformations>> GetDocumentInformation(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this document information!" });
                }

                var documentInformations = await _uow.DocumentInformationsRepository.GetByIdAsync(guid);
                return Ok(documentInformations);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutDocumentInformation(DocumentInformations documentInformations)
        {
            try
            {
                if (!await CheckAuthorization(documentInformations.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this document information!" });
                }

                var entity = await _uow.DocumentInformationsRepository.GetByIdAsync(documentInformations.Id);
                documentInformations.CopyProperties(entity);
                await _uow.DocumentInformationsRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "DocumentInformation updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostDocumentInformations(DocumentInformations documentInformations)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(documentInformations.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this document information!" });
        //        }

        //        await _uow.DocumentInformationsRepository.AddAsync(documentInformations);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteDocumentInformations(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this document information!" });
        //        }

        //        await _uow.DocumentInformationsRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("add-position/{documentInformationId}")]
        public async Task<IActionResult> AddPositionToDocumentInformation(string documentInformationId, PositionDto position)
        {
            try
            {
                var docInfromationGuid = Guid.Parse(documentInformationId);
                if (!await CheckAuthorization(docInfromationGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this position to this document information!" });
                }

                await _uow.DocumentInformationsRepository.AddPosition(docInfromationGuid, position.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Position added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("add-positions/{documentInformationId}")]
        public async Task<IActionResult> AddPositionsToDocumentInformation(string documentInformationId, ICollection<PositionDto> positions)
        {
            try
            {
                var docInfromationGuid = Guid.Parse(documentInformationId);
                if (!await CheckAuthorization(docInfromationGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this positions to this document information!" });
                }

                await _uow.DocumentInformationsRepository.AddPositions(docInfromationGuid, positions.ToEntity());
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Positions added." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("delete-position/{documentInformationId}/{positionId}")]
        public async Task<IActionResult> DeletePositionFromDocumentInformation(string documentInformationId, string positionId)
        {
            try
            {
                var docInfromationGuid = Guid.Parse(documentInformationId);
                if (!await CheckAuthorization(docInfromationGuid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this position from this document information!" });
                }

                var toDeleteId = Guid.Parse(positionId);

                await _uow.DocumentInformationsRepository.DeletePosition(docInfromationGuid, toDeleteId);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Position deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid docInfoId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            var result = false;
            result = user.Company.DeliveryNotes.Any(i => i.DocumentInformationsId.Equals(docInfoId));
            if(result == false)
            {
                result = user.Company.Offers.Any(i => i.DocumentInformationId.Equals(docInfoId));
            }
            if (result == false)
            {
                result = user.Company.OrderConfirmations.Any(i => i.DocumentInformationId.Equals(docInfoId));
            }
            if (result == false)
            {
                result = user.Company.Invoices.Any(i => i.DocumentInformationId.Equals(docInfoId));
            }
            return result;
        }

        //private async Task<Guid> GetCompanyIdForUser()
        //{
        //    var email = HttpContext.User.Identity.Name;
        //    var user = await _uow.UserRepository.GetUserByEmail(email);
        //    if (user.Company != null)
        //    {
        //        return user.Company.Id;
        //    }

        //    return Guid.Empty;
        //}
    }
}
