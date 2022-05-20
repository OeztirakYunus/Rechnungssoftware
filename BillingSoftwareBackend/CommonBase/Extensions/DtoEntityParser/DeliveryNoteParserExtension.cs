using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class DeliveryNoteParserExtension
    {
        public static DeliveryNote ToEntity(this DeliveryNoteDto source)
        {
            return new DeliveryNote()
            {
                DeliveryNoteDate = source.DeliveryNoteDate,
                DeliveryNoteNumber = source.DeliveryNoteNumber,
                FlowText = source.FlowText,
                HeaderText = source.HeaderText,
                Status = source.Status,
                Subject = source.Subject,
                DocumentInformations = source.DocumentInformations.ToEntity()
            };
        }
    }
}
