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
        public ICollection<Address> Addresses { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<OrderConfirmation> OrderConfirmations { get; set; }
        public ICollection<DeliveryNote> DeliveryNotes { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public void AddUser(User user)
        {
            user.Company = this;
            Users.Add(user);
        }
    }
}
