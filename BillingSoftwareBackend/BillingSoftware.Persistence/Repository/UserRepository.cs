using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto[]> GetAllUsersAsync()
        {
            return await Task.Run(() =>
            {
                var usersDto = new List<UserDto>();
                var users = _userManager.Users;
                foreach (var item in users)
                {
                    usersDto.Add(MapUserToUserDto(item));
                }
                return usersDto.ToArray();
            });
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return MapUserToUserDto(user);
        }

        private UserDto MapUserToUserDto(User user)
        {
            return new UserDto()
            {
                Company = user.Company,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
        }

    }
}
