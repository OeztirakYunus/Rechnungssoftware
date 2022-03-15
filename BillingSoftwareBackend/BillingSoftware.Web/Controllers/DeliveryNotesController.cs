using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveryNotesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DeliveryNotesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryNote>>> GetDeliveryNotes()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var deliveryNotes = await _uow.DeliveryNoteRepository.GetAllAsync();
                deliveryNotes = deliveryNotes.Where(i => user.Company.DeliveryNotes.Any(a => a.Id.Equals(i.Id))).ToArray();
                return Ok(deliveryNotes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryNote>> GetDeliveryNote(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this delivery note!" });
                }

                var deliveryNote = await _uow.DeliveryNoteRepository.GetByIdAsync(guid);
                return deliveryNote;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutDeliveryNote(DeliveryNote deliveryNote)
        {
            try
            {
                if (!await CheckAuthorization(deliveryNote.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this delivery note!" });
                }

                var entity = await _uow.DeliveryNoteRepository.GetByIdAsync(deliveryNote.Id);
                deliveryNote.CopyProperties(entity);
                await _uow.DeliveryNoteRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDeliveryNote(DeliveryNote deliveryNote)
        {
            try
            {
                if (!await CheckAuthorization(deliveryNote.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this delivery note!" });
                }

                await _uow.DeliveryNoteRepository.AddAsync(deliveryNote);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryNote(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this delivery note!" });
                }

                await _uow.DeliveryNoteRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<bool> CheckAuthorization(Guid deliveryNoteId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.DeliveryNotes.Any(i => i.Id.Equals(deliveryNoteId));
        }
    }
}
