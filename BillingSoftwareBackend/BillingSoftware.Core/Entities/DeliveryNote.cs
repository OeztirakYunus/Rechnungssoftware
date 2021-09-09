﻿using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class DeliveryNote : EntityObject
    {
        [Required]
        public Contact Client { get; set; }
        [Required]
        public string DeliveryNoteNumber { get; set; }
        [Required]
        public DateTime DeliveryNoteDate { get; set; }
        [Required]
        public string Subject { get; set; }
        public string HeaderText { get; set; }
        [Required]
        public List<Position> Positions { get; set; } = new();
        public string FlowText { get; set; }
        [Required]
        public User ContactPerson { get; set; }
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
