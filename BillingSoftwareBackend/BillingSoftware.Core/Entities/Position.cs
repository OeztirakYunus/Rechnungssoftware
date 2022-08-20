using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Position : EntityObject
    {
        [Required]
        public double Quantity { get; set; }
        public double Discount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        public double TotalPriceNet
        {
            get
            {
                return Math.Round(ProductPriceNet * Quantity, 2);
            }
        }

        public double ProductPriceNet
        {
            get
            {
                if (Product == null)
                {
                    return 0;
                }
                else if (TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return Math.Round(Product.SellingPriceNet * (1 - (Discount / 100)), 2);
                }
                else
                {
                    return Math.Round((Product.SellingPriceNet - Discount), 2);
                }
            }
        }

        [Required]
        public Guid ProductId { get; set; }
        public Guid DocumentInformationId { get; set; }

        //Navigation Properties
        public virtual Product Product { get; set; }
        public virtual DocumentInformations DocumentInformation { get; set; }
    }
}
