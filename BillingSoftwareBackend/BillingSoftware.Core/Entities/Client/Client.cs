using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSoftware.Core.Entities.Client
{
    public class Client : EntityObject
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
