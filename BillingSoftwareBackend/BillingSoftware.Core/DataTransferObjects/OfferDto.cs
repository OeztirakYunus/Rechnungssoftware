using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class OfferDto
    {
        public string OfferNumber { get; set; }
        [Required]
        public DateTime OfferDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }
        public virtual DocumentInformationDto DocumentInformation { get; set; }
    }
}
