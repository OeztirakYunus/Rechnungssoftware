using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public AddressesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var addresses = await _uow.AddressRepository.GetAllAsync();
                addresses = addresses.Where(i => user.Company.Addresses.Any(a => a.Id == i.Id)).ToArray();
                return Ok(addresses);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Address>> GetAddress(int id)
        //{
        //    try
        //    {
        //        var address = await _uow.AddressRepository.GetByIdAsync(id);
        //        return address;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> PutAddress(Address address)
        {
            try
            {
                if (!await CheckAuthorization(address.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to edit this address!" });
                }
                var entity = await _uow.AddressRepository.GetByIdAsync(address.Id);
                address.CopyProperties(entity);
                _uow.AddressRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAddress(Address address)
        {
            try
            {
                if (!await CheckAuthorization(address.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this address!" });
                }
                await _uow.AddressRepository.AddAsync(address);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                if (!await CheckAuthorization(id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this address!" });
                }

                await _uow.AddressRepository.Remove(id);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<bool> CheckAuthorization(int addressId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Addresses.Any(i => i.Id == addressId);
        }
    }
}
