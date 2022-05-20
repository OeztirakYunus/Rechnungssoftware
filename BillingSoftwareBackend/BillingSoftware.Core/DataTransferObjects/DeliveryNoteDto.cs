using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class DeliveryNoteDto
    {
        public string DeliveryNoteNumber { get; set; }
        public DateTime DeliveryNoteDate { get; set; }
        public Status Status { get; set; } = Status.OPEN;
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        public string FlowText { get; set; }
        public virtual DocumentInformationDto DocumentInformations { get; set; }
    }
}
