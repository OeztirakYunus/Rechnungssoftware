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
        public List<Address> Addresses { get; set; }
        [Required]
        public List<User> Users { get; set; }
        [NotMapped]
        public List<IContact> Contacts { get; set; }
        public List<Offer> Offers { get; set; }
        public List<OrderConfirmation> OrderConfirmations { get; set; }
        public List<DeliveryNote> DeliveryNotes { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
