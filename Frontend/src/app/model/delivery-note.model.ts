import { Guid } from "guid-typescript";
import { DocumentStatus } from "./Enums/DocumentStatus";

export class DeliveryNote {
    constructor(
        public deliveryNoteNumber : string = "",
        public deliveryNoteDate : Date = new Date(),
        public status : DocumentStatus = DocumentStatus.CLOSED,
        public subject : string = "",
        public headerText : string = "",
        public flowText : string = "",
        public companyId : string = "",
        public documentInformationsId : string = ""
    ){}
}
