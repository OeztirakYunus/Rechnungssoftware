using System;
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
                addresses = addresses.Where(i => user.Company.Address.Id.Equals(i)).ToArray();
                return Ok(addresses);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(string id)
        {
            try
            {
                var guid = Guid.Parse(id);

                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this address!" });
                }

                var address = await _uow.AddressRepository.GetByIdAsync(guid);
                return address;
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

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
                await _uow.AddressRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Address updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostAddress(Address address)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(address.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this address!" });
        //        }
        //        await _uow.AddressRepository.AddAsync(address);
        //        await _uow.SaveChangesAsync();
        //        return Ok(new { Status = "Success", Message = "Address added." });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this address!" });
                }

                await _uow.AddressRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Address deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid addressId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Address.Id.Equals(addressId);
        }
    }
}
