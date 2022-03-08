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

        public async Task<User[]> GetAllUsersAsync()
        {
            return await Task.Run(() =>
            {
                var users = _userManager.Users;
                return users.Include(i => i.Company).ToArrayAsync();//usersDto.ToArray();
            });
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
    }
}
