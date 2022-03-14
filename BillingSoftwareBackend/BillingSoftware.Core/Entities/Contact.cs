using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Contact : EntityObject
    {
        public TypeOfContact TypeOfContactEnum { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string NameOfOrganisation { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Guid CompanyId { get; set; }

        //Navigation Properties
        public List<Address> Addresses { get; set; } = new();
        public Company Company { get; set; }

    }
}
