using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class InvoiceParserExtension
    {
        public static Invoice ToEntity(this InvoiceDto source)
        {
            return new Invoice()
            {
                InvoiceDate = source.InvoiceDate,
                InvoiceNumber = source.InvoiceNumber,
                FlowText = source.FlowText,
                HeaderText = source.HeaderText,
                Status = source.Status,
                Subject = source.Subject,
                DocumentInformation = source.DocumentInformation.ToEntity(),
                PaymentTerm = source.PaymentTerm
            };
        }
    }
}
