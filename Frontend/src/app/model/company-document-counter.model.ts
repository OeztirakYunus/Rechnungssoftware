import { Guid } from "guid-typescript";

export class CompanyDocumentCounter {
    constructor(
        public InvoiceCounter = 1,
        public OfferCounter = 1,
        public DeliveryNoteCounter = 1,
        public OrderConfirmationCounter = 1,
        public CompanyId : string = ""
    ){}
}
