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
    public class OrderConfirmationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public OrderConfirmationsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderConfirmation>>> GetOrderConfirmations()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var orderConfirmations = await _uow.OrderConfirmationRepository.GetAllAsync();
                orderConfirmations = orderConfirmations.Where(i => user.Company.OrderConfirmations.Any(a => a.Id.Equals(i.Id))).ToArray();

                return Ok(orderConfirmations);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderConfirmation>> GetOrderConfirmation(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this order confirmation!" });
                }

                var orderInformation = await _uow.OrderConfirmationRepository.GetByIdAsync(guid);
                return Ok(orderInformation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            try
            {
                if (!await CheckAuthorization(orderConfirmation.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this order confirmation!" });
                }

                var entity = await _uow.OrderConfirmationRepository.GetByIdAsync(orderConfirmation.Id);
                orderConfirmation.CopyProperties(entity);
                await _uow.OrderConfirmationRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Invoice updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostOrderConfirmation(OrderConfirmation orderConfirmation)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(orderConfirmation.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this order confirmation!" });
        //        }

        //        await _uow.OrderConfirmationRepository.AddAsync(orderConfirmation);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<OrderConfirmation>> DeleteOrderConfirmation(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this order confirmation!" });
        //        }

        //        await _uow.OrderConfirmationRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("order-confirmation-to-invoice/{orderConfirmationId}")]
        public async Task<IActionResult> OrderConfirmationToInvoice(string orderConfirmationId)
        {
            try
            {
                var guid = Guid.Parse(orderConfirmationId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to transform this order confirmation!" });
                }

                var orderConfirmation = await _uow.OrderConfirmationRepository.GetByIdAsync(guid);
                var invoice = await _uow.OrderConfirmationRepository.OrderConfirmationToInvoice(orderConfirmation);
                await _uow.SaveChangesAsync();
                return Ok(invoice);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("order-confirmation-to-delivery-note/{orderConfirmationId}")]
        public async Task<IActionResult> OrderConfirmationToDeliveryNote(string orderConfirmationId)
        {
            try
            {
                var guid = Guid.Parse(orderConfirmationId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to transform this order confirmation!" });
                }

                var orderConfirmation = await _uow.OrderConfirmationRepository.GetByIdAsync(guid);
                var deliveryNote = await _uow.OrderConfirmationRepository.OrderConfirmationToDeliveryNote(orderConfirmation);
                await _uow.SaveChangesAsync();
                return Ok(deliveryNote);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-word/{orderConfirmationId}")]
        public async Task<IActionResult> GetOrderConfirmationAsWord(string orderConfirmationId)
        {
            try
            {
                var guid = Guid.Parse(orderConfirmationId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this order confirmation as word!" });
                }
                var orderConfirmation = await _uow.OrderConfirmationRepository.GetByIdAsync(guid);
                var (bytes, path) = await DocxCreator.CreateWordForOrderConfirmation(orderConfirmation);
                return File(bytes, "application/docx", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("get-as-pdf/{orderConfirmationId}")]
        public async Task<IActionResult> GetOrderConfirmationAsPdf(string orderConfirmationId)
        {
            try
            {
                var guid = Guid.Parse(orderConfirmationId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this order confirmation as pdf!" });
                }
                var orderConfirmation = await _uow.OrderConfirmationRepository.GetByIdAsync(guid);
                var (bytes, path) = await PdfCreator.CreatePdfForOrderConfirmation(orderConfirmation);
                return File(bytes, "application/pdf", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid orderConfirmationId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.OrderConfirmations.Any(i => i.Id.Equals(orderConfirmationId));
        }
    }
}
