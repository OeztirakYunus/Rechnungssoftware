import { Guid } from "guid-typescript";
import { DiscountType } from "./Enums/DiscountType";

export class DocumentInformation {
    constructor(
        public TotalDiscount : number = 0,
        public TypeOfDiscount : DiscountType = DiscountType.Euro,
        public Tax : number = 1,
        public ClientId : string = "",
        public ContactPersonId : string = "",
        public TotalPriceNet : number = 1,
        public TotalPriceGross : number = 1,

    ) {}
}
