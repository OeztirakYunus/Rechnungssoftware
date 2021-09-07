using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Position
    {
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Prozent;
        public double TotalPriceNet
        {
            get
            {
                if(TypeOfDiscount == TypeOfDiscount.Prozent)
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
                if (TypeOfDiscount == TypeOfDiscount.Prozent)
                {
                    return Product.SellingPriceGross * (1 - (Discount / 100)) * Quantity;
                }
                else
                {
                    return (Product.SellingPriceGross - Discount) * Quantity;
                }
            }
        }
    }
}
