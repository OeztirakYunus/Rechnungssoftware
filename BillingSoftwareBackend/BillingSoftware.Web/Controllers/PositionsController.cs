//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BillingSoftware.Core.Contracts;
//using BillingSoftware.Core.Entities;
//using BillingSoftware.Persistence;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BillingSoftware.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PositionsController : ControllerBase
//    {
//        private readonly IUnitOfWork _uow;

//        public PositionsController(IUnitOfWork uow)
//        {
//            _uow = uow;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
//        {
//            try
//            {
//                return Ok(await _uow.PositionRepository.GetAllAsync());
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Position>> GetPosition(int id)
//        {
//            try
//            {
//                var position = await _uow.PositionRepository.GetByIdAsync(id);
//                return position;
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> PutPosition(Position position)
//        {
//            try
//            {
//                var entity = await _uow.PositionRepository.GetByIdAsync(position.Id);
//                entity.CopyProperties(position);
//                _uow.PositionRepository.Update(entity);
//                await _uow.SaveChangesAsync();
//                return Ok();
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> PostPosition(Position position)
//        {
//            try
//            {
//                await _uow.PositionRepository.AddAsync(position);
//                await _uow.SaveChangesAsync();
//                return Ok();
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Position>> DeletePosition(int id)
//        {
//            try
//            {
//                await _uow.PositionRepository.Remove(id);
//                await _uow.SaveChangesAsync();
//                return Ok();
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
