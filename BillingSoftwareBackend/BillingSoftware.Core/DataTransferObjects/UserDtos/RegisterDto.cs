using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UserDtos
{
    public class RegisterDto
    {
        public CompanyRegisterDto Company { get; set; }
        public UserRegisterDTO User { get; set; }
    }
}
