import { Guid } from "guid-typescript";
import { DocumentStatus } from "./Enums/DocumentStatus";

export class Offer {
    constructor(
        public OfferNumber : string = "",
        public OfferDate : Date = new Date(),
        public ValidUntil : Date = new Date(),
        public Status : DocumentStatus = DocumentStatus.CLOSED,
        public Subject : string = "",
        public HeaderText : string = "",
        public FlowText : string = "",
        public DocumentInformationId : string = "",
        public CompanyId : string = ""
    ) {}
}
