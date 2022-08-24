using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UpdateDtos
{
    public class UpdateOfferDto
    {
        [Required]
        public Guid Id { get; set; }
        public string OfferNumber { get; set; }
        [Required]
        public DateTime OfferDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }
        public Guid DocumentInformationId { get; set; }
        public virtual UpdateDocumentInformationDto DocumentInformation { get; set; }
    }
}
