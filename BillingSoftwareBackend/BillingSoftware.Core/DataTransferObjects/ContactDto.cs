using BillingSoftware.Core.Enums;
using System;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public TypeOfContact TypeOfContactEnum { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameOfOrganisation { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}
