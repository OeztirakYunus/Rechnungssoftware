import { GenderPerson } from "./Enums/GenderPerson";
import { TypeOfContact } from "./Enums/TypeOfContact";
import { Guid } from "guid-typescript";
import { Type } from "@angular/core";

export class Contact {
    constructor(
        public TypeOfContactEnum : TypeOfContact = TypeOfContact.Client,
        public Gender : GenderPerson = GenderPerson.Male,
        public Title : string = "",
        public FirstName : string = "",
        public LastName : string = "",
        public NameOfOrganisation : string = "",
        public PhoneNumber : string = "",
        public Email : string = "",
        public CompanyId : string = "",
        public AddressId : string = ""
    ) {}
}
