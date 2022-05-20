using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
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
    public class InvoicesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public InvoicesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var invoices = await _uow.InvoiceRepository.GetAllAsync();
                invoices = invoices.Where(i => user.Company.Invoices.Any(a => a.Id.Equals(i.Id))).ToArray();
                return Ok(invoices);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this invoice!" });
                }

                var invoice = await _uow.InvoiceRepository.GetByIdAsync(guid);
                return Ok(invoice);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutInvoice(Invoice invoice)
        {
            try
            {
                if (!await CheckAuthorization(invoice.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this invoice!" });
                }

                var entity = await _uow.InvoiceRepository.GetByIdAsync(invoice.Id);
                invoice.CopyProperties(entity);
                await _uow.InvoiceRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Invoice updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostInvoice(Invoice invoice)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(invoice.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this invoice!" });
        //        }

        //        await _uow.InvoiceRepository.AddAsync(invoice);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteInvoice(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this invoice!" });
        //        }

        //        await _uow.InvoiceRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        [HttpPost("invoice-to-delivery-note/{invoiceId}")]
        public async Task<IActionResult> InvoiceToDeliveryNote(string invoiceId)
        {
            try
            {
                var guid = Guid.Parse(invoiceId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to transform this invoice!" });
                }

                var invoice = await _uow.InvoiceRepository.GetByIdAsync(guid);
                var deliveryNote = await _uow.InvoiceRepository.InvoiceToDeliveryNote(invoice);
                await _uow.SaveChangesAsync();
                return Ok(deliveryNote);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-word/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceAsWord(string invoiceId)
        {
            try
            {
                var guid = Guid.Parse(invoiceId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this invoice as word!" });
                }
                var invoice = await _uow.InvoiceRepository.GetByIdAsync(guid);
                var (bytes, path) = await DocxCreator.CreateWordForInvoice(invoice);
                return File(bytes, "application/docx", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-pdf/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceAsPdf(string invoiceId)
        {
            try
            {
                var guid = Guid.Parse(invoiceId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this invoice as pdf!" });
                }
                var invoice = await _uow.InvoiceRepository.GetByIdAsync(guid);
                var (bytes, path) = await PdfCreator.CreatePdfForInvoice(invoice);
                return File(bytes, "application/pdf", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid invoiceId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Invoices.Any(i => i.Id.Equals(invoiceId));
        }
    }
}
