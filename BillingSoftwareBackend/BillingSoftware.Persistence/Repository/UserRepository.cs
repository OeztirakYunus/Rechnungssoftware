using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
                return users
                .IncludeAllRecursively()
                .ToArrayAsync();//usersDto.ToArray();
            });
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.Users
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid guid)
        {
            var id = guid.ToString();
            var user = await _userManager.Users
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
            return user;
        }

        public async Task<string> GetRolesForUserAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = "";
            for (int i = 0; i < roles.Count; i++)
            {
                rolesString += roles[i];
                if(i + 1 < roles.Count)
                {
                    rolesString += ", ";
                }
            }
            return rolesString;
        }
    }
}
