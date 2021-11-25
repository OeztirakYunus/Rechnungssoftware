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
        public virtual List<Address> Addresses { get; set; } = new();

        public void CopyProperties(Contact other)
        {
            TypeOfContactEnum = other.TypeOfContactEnum;
            Gender = other.Gender;
            Title = other.Title;
            FirstName = other.FirstName;
            LastName = other.LastName;
            NameOfOrganisation = other.NameOfOrganisation;
            PhoneNumber = other.PhoneNumber;
            Email = other.Email;
            for (int i = 0; i < Addresses.Count; i++)
            {
                Addresses[i].CopyProperties(other.Addresses[i]);
            }
        }
    }
}
