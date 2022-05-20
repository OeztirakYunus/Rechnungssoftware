using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class ProductParserExtension
    {
        public static Product ToEntity(this ProductDto source)
        {
            return new Product()
            {
                ArticleNumber = source.ArticleNumber,
                Description = source.Description,
                Category = source.Category,
                ProductName = source.ProductName,
                SellingPriceNet = source.SellingPriceNet,
                Unit = source.Unit
            };
        }
    }
}
