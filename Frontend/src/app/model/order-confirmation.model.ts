import { Guid } from "guid-typescript";
import { DocumentStatus } from "./Enums/DocumentStatus";

export class OrderConfirmation {
    constructor(
        public OrderConfirmationNumber : string = "",
        public OrderConfirmationDate : Date = new Date(),
        public Status : DocumentStatus = DocumentStatus.CLOSED,
        public Subject : string = "",
        public HeaderText : string = "",
        public FlowText : string = "",
        public CompanyId : string = "",
        public DocumentInformationId : string = ""
    ){}
}
