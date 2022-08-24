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
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        public DateTime PaymentTerm { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }
        public Guid DocumentInformationId { get; set; }
        public Guid CompanyId { get; set; }

        //Navigation Properties
        public virtual DocumentInformations DocumentInformation { get; set; }
        //public virtual Company Company { get; set; }
    }
}
