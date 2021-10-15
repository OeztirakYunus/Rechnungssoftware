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
                return Ok(await _uow.OrderConfirmationRepository.GetAllAsync());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderConfirmation>> GetOrderConfirmation(int id)
        {
            try
            {
                var orderInformation = await _uow.OrderConfirmationRepository.GetByIdAsync(id);
                return orderInformation;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            try
            {
                var entity = await _uow.OrderConfirmationRepository.GetByIdAsync(orderConfirmation.Id);
                entity.CopyProperties(orderConfirmation);
                _uow.OrderConfirmationRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            try
            {
                await _uow.OrderConfirmationRepository.AddAsync(orderConfirmation);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderConfirmation>> DeleteOrderConfirmation(int id)
        {
            try
            {
                await _uow.AddressRepository.Remove(id);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
