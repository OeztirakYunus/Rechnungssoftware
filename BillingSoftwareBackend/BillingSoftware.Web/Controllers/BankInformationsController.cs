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
    public class BankInformationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public BankInformationsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankInformation>>> GetBankInformation()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var bankInformation = await _uow.BankInformationRepository.GetAllAsync();
                bankInformation = bankInformation.Where(i => user.Company.BankInformation.Id.Equals(i)).ToArray();
                return Ok(bankInformation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Address>> GetAddress(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);

        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this address!" });
        //        }

        //        var address = await _uow.AddressRepository.GetByIdAsync(guid);
        //        return address;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> PutBankInformation(BankInformation bankInformation)
        {
            try
            {
                if (!await CheckAuthorization(bankInformation.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to edit this bank information!" });
                }
                var entity = await _uow.BankInformationRepository.GetByIdAsync(bankInformation.Id);
                bankInformation.CopyProperties(entity);
                await _uow.BankInformationRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Bank Information updated." });
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
        public async Task<IActionResult> DeleteBankInformation(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this bank information!" });
                }

                await _uow.BankInformationRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Bank Information deleted." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<bool> CheckAuthorization(Guid bankInformationId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.BankInformation.Id.Equals(bankInformationId);
        }
    }
}
