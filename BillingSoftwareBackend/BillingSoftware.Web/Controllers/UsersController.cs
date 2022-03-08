using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects;
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
                var users = await _uow.UserRepository.GetAllUsersAsync();
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
                var user = await _uow.UserRepository.GetUserByIdAsync(id);
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
    }
}
