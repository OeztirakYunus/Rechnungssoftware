using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class AddressParserExtension
    {
        public static Address ToEntity(this AddressDto source)
        {
            return new Address()
            {
                City = source.City,
                Street = source.Street,
                Country = source.Country,
                ZipCode = source.ZipCode
            };
        }
    }
}
