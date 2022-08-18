import { Guid } from "guid-typescript";

export class BSFile {
    constructor(
        public FileName : string = "",
        public Bytes : string = "",
        public ContentType : string = "",
        public CreationTime : Date = new Date(),
        public CompanyId : string = ""
    ){};
}
