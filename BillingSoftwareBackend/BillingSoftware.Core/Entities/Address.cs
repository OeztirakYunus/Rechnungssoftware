using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Address : EntityObject
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
