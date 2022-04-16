using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UserDtos
{
    public class CompanyRegisterDto
    {
        [Required]
        public string CompanyName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string UstNumber { get; set; }
        public AddressRegisterDto Address { get; set; }
    }
}
