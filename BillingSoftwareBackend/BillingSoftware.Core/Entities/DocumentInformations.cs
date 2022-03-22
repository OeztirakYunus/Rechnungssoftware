using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Entities
{
    public class DocumentInformations : EntityObject
    {
        public double TotalDiscount { get; set; } = 0;
        public TypeOfDiscount TypeOfDiscount { get; set; } = TypeOfDiscount.Percent;
        [Required]
        public double Tax { get; set; }
        public Guid? ClientId { get; set; }
        public string? ContactPersonId { get; set; }
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
                    return Math.Round(totalPriceNet * (1 - (TotalDiscount / 100)), 2);
                }
                else
                {
                    return Math.Round(totalPriceNet - TotalDiscount, 2);
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
                    totalPriceGross += item.TotalPriceNet;
                }
                totalPriceGross = totalPriceGross * (1 + (Tax / 100));
                if (TypeOfDiscount == TypeOfDiscount.Percent)
                {
                    return Math.Round(totalPriceGross * (1 - (TotalDiscount / 100)), 2);
                }
                else
                {
                    return Math.Round(totalPriceGross - TotalDiscount, 2);
                }
            }
        }


        //Navigation Properties
        public virtual List<Position> Positions { get; set; } = new();
        public virtual User ContactPerson { get; set; }
        public virtual Contact Client { get; set; }
    }
}
