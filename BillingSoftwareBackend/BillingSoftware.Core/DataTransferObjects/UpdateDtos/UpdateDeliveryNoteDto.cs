using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UpdateDtos
{
    public class UpdateDeliveryNoteDto
    {
        [Required]
        public Guid Id { get; set; }
        public string DeliveryNoteNumber { get; set; }
        [Required]
        public DateTime DeliveryNoteDate { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }
        [Required]
        public Guid DocumentInformationsId { get; set; }

        //Navigation Properties
        public virtual UpdateDocumentInformationDto DocumentInformations { get; set; }
    }
}
