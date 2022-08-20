import { Guid } from "guid-typescript";
import { Category } from "./Enums/Category";
import { Units } from "./Enums/Units";

export class Product {
    constructor(
        public ArticleNumber : string = "",
        public ProductName : string = "",
        public SellingPriceNet : number = 1,
        public ProductCategory : Category = Category.Article,
        public Unit : Units = Units.Kilogramm,
        public Description : string = "",
        public CompanyId : string = ""
    ){}
}
