﻿using BillingSoftware.Core.Contracts;
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
        public Status Status { get; set; } = Status.OPEN;
        public Guid CompanyId { get; set; }
    }
}
