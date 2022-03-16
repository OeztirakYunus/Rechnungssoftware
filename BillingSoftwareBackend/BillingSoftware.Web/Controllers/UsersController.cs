using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects.UserDtos;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var companyId = await GetCompanyId();
                var users = await _uow.UserRepository.GetAllUsersAsync();
                users = users.Where(i => i.CompanyId.Equals(companyId)).ToArray();
                var usersDto = new List<UserDto>();
                users.ToList().ForEach(x => usersDto.Add(MapUserToUserDto(x)));
                return usersDto.ToArray();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
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
                return MapUserToUserDto(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private UserDto MapUserToUserDto(User user)
        {
            return new UserDto()
            {
                Company = user.Company,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id
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
