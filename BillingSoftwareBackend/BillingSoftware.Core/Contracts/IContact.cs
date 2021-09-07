using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Contracts
{
    public interface IContact
    {
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
