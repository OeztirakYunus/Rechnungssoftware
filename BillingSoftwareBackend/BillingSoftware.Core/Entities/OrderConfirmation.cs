using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class OrderConfirmation : EntityObject
    {
        [Required]
        public string OrderConfirmationNumber { get; set; }
        [Required]
        public DateTime OrderConfirmationDate { get; set; }
        [Required]
        public virtual DocumentInformations OrderConfirmationInformations { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public void CopyProperties(OrderConfirmation other)
        {
            OrderConfirmationNumber = other.OrderConfirmationNumber;
            OrderConfirmationDate = other.OrderConfirmationDate;
            OrderConfirmationInformations.CopyProperties(other.OrderConfirmationInformations);
        }
    }
}
