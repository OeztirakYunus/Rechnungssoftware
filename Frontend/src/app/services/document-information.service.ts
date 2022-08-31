import { Injectable } from '@angular/core';
import {DocumentInformations, DocumentInformationsService} from "../../../client";

@Injectable({
  providedIn: 'root'
})
export class DocumentInformationService {

  constructor(
    private documentationInformationsService: DocumentInformationsService
  ) { }

  update(doc: DocumentInformations){
  }
}
