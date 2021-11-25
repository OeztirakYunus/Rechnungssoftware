using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                return Ok(await _uow.InvoiceRepository.GetAllAsync());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            try
            {
                var invoice = await _uow.InvoiceRepository.GetByIdAsync(id);
                return invoice;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutInvoice(Invoice invoice)
        {
            try
            {
                var entity = await _uow.InvoiceRepository.GetByIdAsync(invoice.Id);
                entity.CopyProperties(invoice);
                _uow.InvoiceRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostInvoice(Invoice invoice)
        {
            try
            {
                await _uow.InvoiceRepository.AddAsync(invoice);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                await _uow.InvoiceRepository.Remove(id);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("invoice-to-delivery-note")]
        public async Task<IActionResult> InvoiceToDeliveryNote(Invoice invoice)
        {
            try
            {
                var deliveryNote = _uow.InvoiceRepository.InvoiceToDeliveryNote(invoice);
                await _uow.SaveChangesAsync();
                return Ok(deliveryNote);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.InnerException.Message);
            }
        }
    }
}
