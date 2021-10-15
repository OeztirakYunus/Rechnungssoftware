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
        [Required]
        public virtual DocumentInformations DeliveryNoteInformations { get; set; }

        public void CopyProperties(DeliveryNote other)
        {
            DeliveryNoteNumber = other.DeliveryNoteNumber;
            DeliveryNoteDate = other.DeliveryNoteDate;
            DeliveryNoteInformations.CopyProperties(other.DeliveryNoteInformations);
        }
    }
}
