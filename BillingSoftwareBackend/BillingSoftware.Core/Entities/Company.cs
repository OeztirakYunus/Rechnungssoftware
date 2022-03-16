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
        public int InvoiceCounter { get; set; } = 1;
        public int OfferCounter { get; set; } = 1;
        public int DeliveryNoteCounter { get; set; } = 1;
        public int OrderConfirmationCounter { get; set; } = 1;
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string UstNumber { get; set; }

        //Navigation Properties
        public virtual List<Address> Addresses { get; set; } = new();
        public virtual List<User> Users { get; set; } = new();
        public virtual List<Contact> Contacts { get; set; } = new();
        public virtual List<Offer> Offers { get; set; } = new();
        public virtual List<OrderConfirmation> OrderConfirmations { get; set; } = new();
        public virtual List<DeliveryNote> DeliveryNotes { get; set; } = new();
        public virtual List<Invoice> Invoices { get; set; } = new();
        public virtual List<Product> Products { get; set; } = new();
    }
}
