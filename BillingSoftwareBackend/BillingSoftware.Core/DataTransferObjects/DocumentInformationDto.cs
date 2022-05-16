using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class DocumentInformationDto
    {
        public double TotalDiscount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        public double Tax { get; set; }
        public Guid? ClientId { get; set; }
        public string? ContactPersonId { get; set; }
        public virtual List<PositionDto> Positions { get; set; } = new();
    }
}
