using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using CommonBase.DtoEntityParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class DocumentInformationParserExtension
    {
        public static DocumentInformations ToEntity(this DocumentInformationDto source)
        {
            return new DocumentInformations()
            {
                ClientId = source.ClientId,
                ContactPersonId = source.ContactPersonId,
                Tax = source.Tax,
                TotalDiscount = source.TotalDiscount,
                TypeOfDiscount = source.TypeOfDiscount,
                //Positions = source.Positions.ToEntity()
            };
        }
    }
}
