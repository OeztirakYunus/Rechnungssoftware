using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PositionsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var result = new List<Position>();
                var positions = await _uow.PositionRepository.GetAllAsync();
                var offers = await _uow.OfferRepository.GetAllAsync();
                var invoices = await _uow.InvoiceRepository.GetAllAsync();
                var orderConfirmations = await _uow.OrderConfirmationRepository.GetAllAsync();
                var deliveryNotes = await _uow.DeliveryNoteRepository.GetAllAsync();

                var linqResult = positions.Where(i => offers.Any(x => x.DocumentInformation.Positions.Any(x => x.Equals(i)))).ToArray();
                result.AddRange(linqResult);
                linqResult = positions.Where(i => invoices.Any(x => x.DocumentInformation.Positions.Any(x => x.Equals(i)))).ToArray();
                result.AddRange(linqResult);
                linqResult = positions.Where(i => orderConfirmations.Any(x => x.DocumentInformation.Positions.Any(x => x.Equals(i)))).ToArray();
                result.AddRange(linqResult);
                linqResult = positions.Where(i => deliveryNotes.Any(x => x.DocumentInformations.Positions.Any(x => x.Equals(i)))).ToArray();
                result.AddRange(linqResult);

                return Ok(result.DistinctBy(i => i.Id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this position!" });
                }

                var position = await _uow.PositionRepository.GetByIdAsync(guid);
                return position;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPosition(Position position)
        {
            try
            {
                if (!await CheckAuthorization(position.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this position!" });
                }

                var entity = await _uow.PositionRepository.GetByIdAsync(position.Id);
                position.CopyProperties(entity);
                await _uow.PositionRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPosition(Position position)
        {
            try
            {
                if (!await CheckAuthorization(position.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this position!" });
                }

                await _uow.PositionRepository.AddAsync(position);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Position>> DeletePosition(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this position!" });
                }

                await _uow.PositionRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<bool> CheckAuthorization(Guid positionId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            var result = false;
            result = user.Company.DeliveryNotes.Any(i => i.DocumentInformations.Positions.Any(x => x.Id.Equals(positionId)));
            if (result == false)
            {
                result = user.Company.Offers.Any(i => i.DocumentInformation.Positions.Any(x => x.Id.Equals(positionId)));
            }
            if (result == false)
            {
                result = user.Company.OrderConfirmations.Any(i => i.DocumentInformation.Positions.Any(x => x.Id.Equals(positionId)));
            }
            if (result == false)
            {
                result = user.Company.Invoices.Any(i => i.DocumentInformation.Positions.Any(x => x.Id.Equals(positionId)));
            }
            return result;
        }
    }
}
