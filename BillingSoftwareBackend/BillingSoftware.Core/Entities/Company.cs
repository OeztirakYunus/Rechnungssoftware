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

        //public void CopyProperties(Company other)
        //{
        //    CompanyName = other.CompanyName;
        //    Email = other.Email;
        //    PhoneNumber = other.PhoneNumber;
        //    Addresses = new List<Address>();
        //    Users = new List<User>();
        //    Contacts = new List<Contact>();
        //    Offers = new List<Offer>();
        //    OrderConfirmations = new List<OrderConfirmation>();
        //    DeliveryNotes = new List<DeliveryNote>();
        //    Invoices = new List<Invoice>();
        //    Products = new List<Product>();

        //    foreach (var item in other.Addresses)
        //    {
        //        Addresses.Add(item);
        //    }
        //    foreach (var item in other.Users)
        //    {
        //        Users.Add(item);
        //    }
        //    foreach (var item in other.Contacts)
        //    {
        //        Contacts.Add(item);
        //    }
        //    foreach (var item in other.Offers)
        //    {
        //        Offers.Add(item);
        //    }
        //    foreach (var item in other.OrderConfirmations)
        //    {
        //        OrderConfirmations.Add(item);
        //    }
        //    foreach (var item in other.Invoices)
        //    {
        //        Invoices.Add(item);
        //    }
        //    foreach (var item in other.DeliveryNotes)
        //    {
        //        DeliveryNotes.Add(item);
        //    }
        //    foreach (var item in other.Products)
        //    {
        //        Products.Add(item);
        //    }
        //}
    }
}
