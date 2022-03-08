using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IUserRepository
    {
        public Task<User[]> GetAllUsersAsync();
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> GetUserByEmail(string email);
    }
}
