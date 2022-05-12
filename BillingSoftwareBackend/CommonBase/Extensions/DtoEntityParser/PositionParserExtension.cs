using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.DtoEntityParser
{
    public static class PositionParserExtension
    {
        public static Position ToEntity(this PositionDto source)
        {
            return new Position
            {
                Discount = source.Discount,
                ProductId = source.ProductId,
                Quantity = source.Quantity,
                TypeOfDiscount = source.TypeOfDiscount
            };
        }

        public static List<Position> ToEntity(this List<PositionDto> source)
        {
            var positions = new List<Position>();
            foreach (var item in source)
            {
                positions.Add(item.ToEntity());
            }
            return positions;
        }
    }
}
