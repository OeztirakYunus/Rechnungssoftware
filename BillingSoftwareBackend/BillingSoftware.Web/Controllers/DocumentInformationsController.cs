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
                return Ok(await _uow.DocumentInformationsRepository.GetAllAsync());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentInformations>> GetDocumentInformation(int id)
        {
            try
            {
                var documentInformations = await _uow.DocumentInformationsRepository.GetByIdAsync(id);
                return documentInformations;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutDocumentInformation(DocumentInformations documentInformations)
        {
            try
            {
                var entity = await _uow.DocumentInformationsRepository.GetByIdAsync(documentInformations.Id);
                entity.CopyProperties(documentInformations);
                _uow.DocumentInformationsRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDocumentInformations(DocumentInformations documentInformations)
        {
            try
            {
                await _uow.DocumentInformationsRepository.AddAsync(documentInformations);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteDocumentInformations(int id)
        {
            try
            {
                await _uow.DocumentInformationsRepository.Remove(id);
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
