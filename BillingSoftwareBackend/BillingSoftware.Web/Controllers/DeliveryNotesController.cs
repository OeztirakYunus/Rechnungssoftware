﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using CommonBase.DocumentCreators;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest(new { Status = "Error", Message = ex.Message });
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
                return Ok(deliveryNote);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutDeliveryNote(UpdateDeliveryNoteDto deliveryNote)
        {
            try
            {
                if (!await CheckAuthorization(deliveryNote.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this delivery note!" });
                }

                await _uow.DeliveryNoteRepository.UpdateWithDto(deliveryNote);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Delivery Note updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostDeliveryNote(DeliveryNote deliveryNote)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(deliveryNote.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this delivery note!" });
        //        }

        //        await _uow.DeliveryNoteRepository.AddAsync(deliveryNote);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDeliveryNote(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this delivery note!" });
        //        }

        //        await _uow.DeliveryNoteRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("get-as-word/{deliveryNoteId}")]
        public async Task<IActionResult> GetDeliveryNoteAsWord(string deliveryNoteId)
        {
            try
            {
                var guid = Guid.Parse(deliveryNoteId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this delivery note as word!" });
                }
                var deliveryNote = await _uow.DeliveryNoteRepository.GetByIdAsync(guid);
                var company = await _uow.CompanyRepository.GetByIdAsync(deliveryNote.CompanyId);
                var (bytes, path) = await DocxCreator.CreateWordForDeliveryNote(deliveryNote, company);
                return File(bytes, "application/docx", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-pdf/{deliveryNoteId}")]
        public async Task<IActionResult> GetDeliveryNoteAsPdf(string deliveryNoteId)
        {
            try
            {
                var guid = Guid.Parse(deliveryNoteId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this delivery note as pdf!" });
                }
                var deliveryNote = await _uow.DeliveryNoteRepository.GetByIdAsync(guid);
                var company = await _uow.CompanyRepository.GetByIdAsync(deliveryNote.CompanyId);
                var (bytes, path) = await PdfCreator.CreatePdfForDeliveryNote(deliveryNote, company);
                return File(bytes, "application/pdf", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
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
