using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UserDtos
{
    public class UserRegisterDTO
    {
        public UserDto User { get; set; }
        public string Password { get; set; }
    }
}
