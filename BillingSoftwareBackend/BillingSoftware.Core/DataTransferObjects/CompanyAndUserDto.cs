using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class CompanyAndUserDto
    {
        public Company Company { get; set; }
        public User User { get; set; }
    }
}
