﻿using System.Collections.Generic;
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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return Ok(await _uow.UserRepository.GetAllAsync());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _uow.UserRepository.GetByIdAsync(id);
                return user;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> PutUser(User user)
        //{
        //    try
        //    {
        //        var entity = await _uow.UserRepository.GetByIdAsync(user.Id);
        //        entity.CopyProperties(user);
        //        _uow.UserRepository.Update(entity);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                await _uow.UserRepository.AddAsync(user);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                await _uow.UserRepository.Remove(id);
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
