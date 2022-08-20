import { Guid } from "guid-typescript";
import { DocumentStatus } from "./Enums/DocumentStatus";

export class Invoice {
    constructor(
        public InvoiceNumber : string = "",
        public InvoiceDate : Date = new Date(),
        public PaymentTerm : Date = new Date(),
        public Status : DocumentStatus = DocumentStatus.CLOSED,
        public Subject : string = "",
        public HeaderText : string = "",
        public FlowText : string = "",
        public DocumentInformationId : string = "",
        public CompanyId : string = ""
    ){}
}
