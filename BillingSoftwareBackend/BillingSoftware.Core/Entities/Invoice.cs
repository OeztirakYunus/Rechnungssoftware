using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Invoice : EntityObject
    {
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        public DateTime PaymentTerm { get; set; }
        [Required]
        public virtual DocumentInformations InvoiceInformations { get; set; }
        public Status Status { get; set; } = Status.OPEN;

        public void CopyProperties(Invoice other)
        {
            InvoiceNumber = other.InvoiceNumber;
            InvoiceDate = other.InvoiceDate;
            PaymentTerm = other.PaymentTerm;
            InvoiceInformations.CopyProperties(other.InvoiceInformations);
            Status = other.Status;
        }
    }
}
