﻿using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects.UpdateDtos
{
    public class UpdatePositionDto
    {
        public Guid? Id { get; set; }
        [Required]
        public double Quantity { get; set; }
        public double Discount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        [Required]
        public Guid ProductId { get; set; }
    }
}
