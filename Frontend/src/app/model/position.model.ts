import { Guid } from "guid-typescript";
import { DiscountType } from "./Enums/DiscountType";

export class Position {
    constructor(
        public Quantity : number = 1,
        public Discount = 0,
        public TypeDiscount : DiscountType = DiscountType.Euro,
        public TotalPriceNet : number = 1,
        public ProductPriceNet : number = 1,
        public ProductId : string = "",
        public DocumentInformationId : string = ""
    ) {}
}
