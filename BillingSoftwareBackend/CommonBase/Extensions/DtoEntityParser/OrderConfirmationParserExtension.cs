using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class OrderConfirmationParserExtension
    {
        public static OrderConfirmation ToEntity(this OrderConfirmationDto source)
        {
            return new OrderConfirmation()
            {
                OrderConfirmationDate = source.OrderConfirmationDate,
                OrderConfirmationNumber = source.OrderConfirmationNumber,
                FlowText = source.FlowText,
                HeaderText = source.HeaderText,
                Status = source.Status,
                Subject = source.Subject,
                DocumentInformation = source.DocumentInformation.ToEntity()
            };
        }
    }
}
