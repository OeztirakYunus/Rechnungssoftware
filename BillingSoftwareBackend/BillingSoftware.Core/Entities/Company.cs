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
        public virtual List<Address> Addresses { get; set; } = new();
        public virtual List<User> Users { get; set; } = new();
        public virtual List<Contact> Contacts { get; set; } = new();
        public virtual List<Offer> Offers { get; set; } = new();
        public virtual List<OrderConfirmation> OrderConfirmations { get; set; } = new();
        public virtual List<DeliveryNote> DeliveryNotes { get; set; } = new();
        public virtual List<Invoice> Invoices { get; set; } = new();
        public virtual List<Product> Products { get; set; } = new();

        public void CopyProperties(Company other)
        {
            CompanyName = other.CompanyName;
            Email = other.Email;
            PhoneNumber = other.PhoneNumber;
            for (int i = 0; i < Addresses.Count; i++)
            {
                Addresses[i].CopyProperties(other.Addresses[i]);
            }
            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].CopyProperties(other.Users[i]);
            }
            for (int i = 0; i < Contacts.Count; i++)
            {
                Contacts[i].CopyProperties(other.Contacts[i]);
            }
            for (int i = 0; i < Offers.Count; i++)
            {
                Offers[i].CopyProperties(other.Offers[i]);
            }
            for (int i = 0; i < OrderConfirmations.Count; i++)
            {
                OrderConfirmations[i].CopyProperties(other.OrderConfirmations[i]);
            }
            for (int i = 0; i < DeliveryNotes.Count; i++)
            {
                DeliveryNotes[i].CopyProperties(other.DeliveryNotes[i]);
            }
            for (int i = 0; i < Invoices.Count; i++)
            {
                Invoices[i].CopyProperties(other.Invoices[i]);
            }
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].CopyProperties(other.Products[i]);
            }
        }
    }
}
