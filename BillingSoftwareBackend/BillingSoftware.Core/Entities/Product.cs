using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillingSoftware.Core.Entities
{
    public class Product : EntityObject
    {
        [Required]
        public string ArticleNumber { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double SellingPriceNet { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
        [Required]
        public Unit Unit { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }

        //Navigation Properties
        //public virtual Company Company { get; set; }
    }
}
