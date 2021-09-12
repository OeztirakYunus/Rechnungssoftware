using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ApplicationDbContext _context;

        public OrderConfirmationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderConfirmation>>> GetOrderConfirmations()
        {
            return await _context.OrderConfirmations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderConfirmation>> GetOrderConfirmation(int id)
        {
            var orderConfirmation = await _context.OrderConfirmations.FindAsync(id);

            if (orderConfirmation == null)
            {
                return NotFound();
            }

            return orderConfirmation;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderConfirmation(int id, OrderConfirmation orderConfirmation)
        {
            if (id != orderConfirmation.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderConfirmation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderConfirmationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<OrderConfirmation>> PostOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            _context.OrderConfirmations.Add(orderConfirmation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderConfirmation", new { id = orderConfirmation.Id }, orderConfirmation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderConfirmation>> DeleteOrderConfirmation(int id)
        {
            var orderConfirmation = await _context.OrderConfirmations.FindAsync(id);
            if (orderConfirmation == null)
            {
                return NotFound();
            }

            _context.OrderConfirmations.Remove(orderConfirmation);
            await _context.SaveChangesAsync();

            return orderConfirmation;
        }

        private bool OrderConfirmationExists(int id)
        {
            return _context.OrderConfirmations.Any(e => e.Id == id);
        }
    }
}
