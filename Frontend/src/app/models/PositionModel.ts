import {DiscountType} from "./DiscountType";

export class PositionModel {
  quantity?: number;
  discount?: number;
  typeOfDiscount?: DiscountType;
  productId?: string;
}
