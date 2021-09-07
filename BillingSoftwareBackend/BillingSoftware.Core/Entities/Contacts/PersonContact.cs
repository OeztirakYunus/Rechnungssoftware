using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities.Contacts
{
    public class PersonContact : EntityObject, IContact
    {
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
