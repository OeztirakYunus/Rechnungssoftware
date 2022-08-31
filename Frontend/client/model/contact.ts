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
import { Address } from './address';
import { Company } from './company';
import { Gender } from './gender';
import { TypeOfContact } from './typeOfContact';

export interface Contact { 
    typeOfContactEnum?: TypeOfContact;
    gender?: Gender;
    title?: string;
    firstName?: string;
    lastName: string;
    nameOfOrganisation?: string;
    phoneNumber?: string;
    email?: string;
    companyId?: string;
    addressId?: string;
    address?: Address;
    company?: Company;
    id?: string;
    rowVersion?: string;
}