using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string UstNumber { get; set; }
        public Guid AddressId{ get; set; }
        public Guid BankInformationId { get; set; }

        //Navigation Properties
        public virtual CompanyDocumentCounter CompanyDocumentCounter { get; set; } = new CompanyDocumentCounter();
        public virtual BankInformation BankInformation { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<User> Users { get; set; } = new();
        public virtual List<Contact> Contacts { get; set; } = new();
        public virtual List<Offer> Offers { get; set; } = new();
        public virtual List<OrderConfirmation> OrderConfirmations { get; set; } = new();
        public virtual List<DeliveryNote> DeliveryNotes { get; set; } = new();
        public virtual List<Invoice> Invoices { get; set; } = new();
        public virtual List<Product> Products { get; set; } = new();
        public virtual List<BSFile> Files { get; set; }
    }
}
