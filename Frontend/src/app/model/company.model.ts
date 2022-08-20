import { Guid } from "guid-typescript";

export class Company {
    constructor(
        public CompanyName : string = "",
        public Email : string = "",
        public PhoneNumber : string = "",
        public UstNumber : string = "",
        public AddressId : string = ""
    ){}
}
