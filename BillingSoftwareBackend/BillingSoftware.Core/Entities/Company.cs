using BillingSoftware.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Company : EntityObject
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Address> Addresses { get; set; }
        public List<User> Users { get; set; }
        public List<IContact> Contacts { get; set; }
        public List<Offer> Offers { get; set; }
        public List<OrderConfirmation> OrderConfirmations { get; set; }
        public List<DeliveryNote> DeliveryNotes { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
