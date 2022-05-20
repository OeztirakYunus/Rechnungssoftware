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
    public class ContactsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ContactsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var contacts = await _uow.ContactRepository.GetAllAsync();
                contacts = contacts.Where(i => user.Company.Contacts.Any(a => a.Id.Equals(i.Id))).ToArray();
                return Ok(contacts);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            try
            {
                var guid = Guid.Parse(id);

                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this contact!" });
                }

                var contact = await _uow.ContactRepository.GetByIdAsync(guid);
                return Ok(contact);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutContact(Contact contact)
        {
            try
            {
                if (!await CheckAuthorization(contact.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to edit this contact!" });
                }

                var entity = await _uow.ContactRepository.GetByIdAsync(contact.Id);
                contact.CopyProperties(entity);
                await _uow.ContactRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Contact updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostContact(Contact contact)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(contact.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add a contact!" });
        //        }

        //        await _uow.ContactRepository.AddAsync(contact);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Contact>> DeleteContact(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this contact!" });
        //        }

        //        await _uow.ContactRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok(new { Status = "Success", Message = "Contact deleted." });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { Status = "Error", Message = ex.Message });
        //    }
        //}

        private async Task<bool> CheckAuthorization(Guid contactId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Contacts.Any(i => i.Id.Equals(contactId));
        }
    }
}
