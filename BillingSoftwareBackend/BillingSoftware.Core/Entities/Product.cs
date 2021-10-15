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
        public double ValueAddedTax { get; set; }
        [Required]
        public double SellingPriceNet { get; set; }
        public double SellingPriceGross => SellingPriceNet * ((ValueAddedTax / 100) + 1);
        public double PurchasingPriceNet { get; set; }
        public double PurchasingPriceGross => PurchasingPriceNet * ((ValueAddedTax / 100) + 1);
        [Required]
        public ProductCategory Category { get; set; }
        [Required]
        public Unit Unit { get; set; }
        public string Description { get; set; }

        public void CopyProperties(Product other)
        {
            ArticleNumber = other.ArticleNumber;
            ProductName = other.ProductName;
            ValueAddedTax = other.ValueAddedTax;
            SellingPriceNet = other.SellingPriceNet;
            PurchasingPriceNet = other.PurchasingPriceNet;
            Category = other.Category;
            Unit = other.Unit;
            Description = other.Description;
        }
    }
}
