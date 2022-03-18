using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Entities
{
    public class CompanyDocumentCounter : EntityObject
    {
        public int InvoiceCounter { get; set; } = 1;
        public int OfferCounter { get; set; } = 1;
        public int DeliveryNoteCounter { get; set; } = 1;
        public int OrderConfirmationCounter { get; set; } = 1;
        public Guid CompanyId { get; set; }
    }
}
