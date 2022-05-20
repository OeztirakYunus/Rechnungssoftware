using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class ProductDto
    {
        public string ArticleNumber { get; set; }
        public string ProductName { get; set; }
        public double SellingPriceNet { get; set; }
        public ProductCategory Category { get; set; }
        public Unit Unit { get; set; }
        public string Description { get; set; }
    }
}
