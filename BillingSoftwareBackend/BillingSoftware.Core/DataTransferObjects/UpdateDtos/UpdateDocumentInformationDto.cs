using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UpdateDtos
{
    public class UpdateDocumentInformationDto
    {
        public Guid? Id { get; set; }
        public double TotalDiscount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        [Required]
        public double Tax { get; set; }
        public Guid? ClientId { get; set; }
        public string? ContactPersonId { get; set; }
        public virtual List<UpdatePositionDto> Positions { get; set; } = new();
    }
}
