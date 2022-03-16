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
        public string OfferNumber { get; set; }
        [Required]
        public DateTime OfferDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public Guid DocumentInformationId { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public Guid CompanyId { get; set; }
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }

        //Navigation Properties
        public virtual DocumentInformations DocumentInformation { get; set; }
        public virtual Company Company { get; set; }
    }
}
