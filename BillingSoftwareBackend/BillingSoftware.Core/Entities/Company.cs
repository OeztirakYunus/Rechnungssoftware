using BillingSoftware.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Company : EntityObject
    {
        [Required]
        public string CompanyName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public List<Address> Addresses { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<Contact> Contacts { get; set; } = new();
        public List<Offer> Offers { get; set; } = new();
        public List<OrderConfirmation> OrderConfirmations { get; set; } = new();
        public List<DeliveryNote> DeliveryNotes { get; set; } = new();
        public List<Invoice> Invoices { get; set; } = new();
        public void AddUser(User user)
        {
            user.Company = this;
            Users.Add(user);
        }
    }
}
