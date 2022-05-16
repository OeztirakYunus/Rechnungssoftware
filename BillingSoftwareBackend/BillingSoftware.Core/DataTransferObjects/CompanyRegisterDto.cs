using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class CompanyRegisterDto
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string UstNumber { get; set; }
        public AddressDto Address { get; set; }
    }
}
