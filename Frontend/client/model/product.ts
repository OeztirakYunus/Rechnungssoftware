/**
 * BillingSoftware.Web
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Company } from './company';
import { ProductCategory } from './productCategory';
import { Unit } from './unit';

export interface Product { 
    articleNumber: string;
    productName: string;
    sellingPriceNet: number;
    category: ProductCategory;
    unit: Unit;
    description?: string;
    companyId?: string;
    company?: Company;
    id?: string;
    rowVersion?: string;
}