﻿using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class DeliveryNote : EntityObject
    {
        [Required]
        public string DeliveryNoteNumber { get; set; }
        [Required]
        public DateTime DeliveryNoteDate { get; set; }
        public Guid DocumentInformationsId { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public Guid CompanyId { get; set; }

        //Navigation Properties
        public virtual DocumentInformations DocumentInformations { get; set; }
        public virtual Company Company { get; set; }
    }
}
