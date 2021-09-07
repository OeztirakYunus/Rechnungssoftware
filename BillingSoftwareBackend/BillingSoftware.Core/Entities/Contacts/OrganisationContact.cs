using BillingSoftware.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities.Contacts
{
    public class OrganisationContact : EntityObject, IContact
    {
        [Required]
        public string NameOfOrganisation { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
