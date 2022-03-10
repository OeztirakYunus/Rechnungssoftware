using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Address : EntityObject
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        //public void CopyProperties(Address other)
        //{
        //    Street = other.Street;
        //    ZipCode = other.ZipCode;
        //    City = other.City;
        //    Country = other.Country;
        //}
    }
}
