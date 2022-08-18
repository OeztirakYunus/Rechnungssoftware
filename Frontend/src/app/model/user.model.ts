import { Guid } from "guid-typescript";

export class User {
    constructor(
        public FirstName : string = "",
        public LastName : string = "",
        public CompanyId : string = ""
    ) {}
}
