﻿using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Position : EntityObject
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public double Quantity { get; set; }
        public double Discount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        public double TotalPriceNet
        {
            get
            {
                if(Product == null)
                {
                    return 0;
                }
                else if(TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return Product.SellingPriceNet * (1 - (Discount / 100)) * Quantity;
                }
                else
                {
                    return (Product.SellingPriceNet - Discount) * Quantity;
                }
            }
        }

        public double TotalPriceGross
        {
            get
            {
                if (Product == null)
                {
                    return 0;
                }
                else if (TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return Product.SellingPriceGross * (1 - (Discount / 100)) * Quantity;
                }
                else
                {
                    return (Product.SellingPriceGross - Discount) * Quantity;
                }
            }
        }
        public Guid DocumentInformationId { get; set; }

        //Navigation Properties
        public virtual Product Product { get; set; }
        public virtual DocumentInformations DocumentInformation { get; set; }
    }
}
