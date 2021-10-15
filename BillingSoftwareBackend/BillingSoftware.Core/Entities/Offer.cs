using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Offer : EntityObject
    {
        [Required]
        public string OfferNumber { get; set; }
        [Required]
        public DateTime OfferDate { get; set; }
        [Required]
        public virtual DocumentInformations OfferInformations { get; set; }

        public void CopyProperties(Offer other)
        {
            OfferNumber = other.OfferNumber;
            OfferDate = other.OfferDate;
            OfferInformations.CopyProperties(other.OfferInformations);
        }
    }
}
