using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class OfferParserExtension
    {
        public static Offer ToEntity(this OfferDto source)
        {
            return new Offer()
            {
                OfferDate = source.OfferDate,
                OfferNumber = source.OfferNumber,
                FlowText = source.FlowText,
                HeaderText = source.HeaderText,
                Status = source.Status,
                Subject = source.Subject,
                ValidUntil = source.ValidUntil,
                DocumentInformation = source.DocumentInformation.ToEntity()
            };
        }
    }
}
