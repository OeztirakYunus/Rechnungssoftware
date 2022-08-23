using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects.UserDtos;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var companyId = await GetCompanyId();
                var users = await _uow.UserRepository.GetAllUsersAsync();
                users = users.Where(i => i.CompanyId.Equals(companyId)).ToArray();
                var usersDto = new List<UserDto>();
                foreach (var user in users)
                {
                    usersDto.Add(await MapUserToUserDto(user));
                }
                return Ok(usersDto.ToArray());
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var user = await _uow.UserRepository.GetUserByIdAsync(guid);
                var companyId = await GetCompanyId();
                if (!user.CompanyId.Equals(companyId))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this user!" });
                }
                return Ok(await MapUserToUserDto(user));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        private async Task<UserDto> MapUserToUserDto(User user)
        {
            var role = await _uow.UserRepository.GetRolesForUserAsync(user);
            return new UserDto()
            {
                Company = user.Company,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                Role = role
            };
        }

        private async Task<Guid> GetCompanyId()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.CompanyId;
        }
    }
}
