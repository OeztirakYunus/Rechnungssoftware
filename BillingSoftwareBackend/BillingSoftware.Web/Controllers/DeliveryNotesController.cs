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
    public class DeliveryNotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeliveryNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryNote>>> GetDeliveryNotes()
        {
            return await _context.DeliveryNotes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryNote>> GetDeliveryNote(int id)
        {
            var deliveryNote = await _context.DeliveryNotes.FindAsync(id);

            if (deliveryNote == null)
            {
                return NotFound();
            }

            return deliveryNote;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryNote(int id, DeliveryNote deliveryNote)
        {
            if (id != deliveryNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(deliveryNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryNoteExists(id))
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
        public async Task<ActionResult<DeliveryNote>> PostDeliveryNote(DeliveryNote deliveryNote)
        {
            _context.DeliveryNotes.Add(deliveryNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryNote", new { id = deliveryNote.Id }, deliveryNote);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeliveryNote>> DeleteDeliveryNote(int id)
        {
            var deliveryNote = await _context.DeliveryNotes.FindAsync(id);
            if (deliveryNote == null)
            {
                return NotFound();
            }

            _context.DeliveryNotes.Remove(deliveryNote);
            await _context.SaveChangesAsync();

            return deliveryNote;
        }

        private bool DeliveryNoteExists(int id)
        {
            return _context.DeliveryNotes.Any(e => e.Id == id);
        }
    }
}
