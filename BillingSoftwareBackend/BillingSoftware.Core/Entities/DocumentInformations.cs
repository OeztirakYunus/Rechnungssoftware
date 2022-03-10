﻿using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Entities
{
    public class DocumentInformations : EntityObject
    {
        [Required]
        public virtual Contact Client { get; set; }
        [Required]
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        [Required]
        public virtual List<Position> Positions { get; set; } = new();
        public string FlowText { get; set; }
        [Required]
        public virtual User ContactPerson { get; set; }
        public double TotalDiscount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        public double TotalPriceNet
        {
            get
            {
                double totalPriceNet = 0;
                foreach (var item in Positions)
                {
                    totalPriceNet += item.TotalPriceNet;
                }
                if (TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return totalPriceNet * (1 - (TotalDiscount / 100));
                }
                else
                {
                    return totalPriceNet - TotalDiscount;
                }
            }
        }
        public double TotalPriceGross
        {
            get
            {
                double totalPriceGross = 0;
                foreach (var item in Positions)
                {
                    totalPriceGross += item.TotalPriceGross;
                }
                if (TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return totalPriceGross * (1 - (TotalDiscount / 100));
                }
                else
                {
                    return totalPriceGross - TotalDiscount;
                }
            }
        }
    }
}
