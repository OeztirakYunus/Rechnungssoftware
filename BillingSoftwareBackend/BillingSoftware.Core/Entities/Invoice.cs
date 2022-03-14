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
        public Guid DocumentInformationId { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public Guid CompanyId { get; set; }

        //Navigation Properties
        public DocumentInformations DocumentInformation { get; set; }
        public Company Company { get; set; }
    }
}
