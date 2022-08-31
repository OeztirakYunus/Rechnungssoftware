import {ProductUnitType} from "./ProductUnitType";
import {ProductCategory} from "./ProductCategoryType";

export interface ProductModel {
  articleNumber?: string;
  productName?: string;
  sellingPriceNet?: number;
  category?: ProductCategory;
  unit?: ProductUnitType;
  description?: string;
  id?: string;
}
