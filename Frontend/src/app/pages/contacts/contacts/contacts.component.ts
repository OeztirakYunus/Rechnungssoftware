import { Component, OnInit } from '@angular/core';
import {CompaniesService, Contact, ContactsService} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {catchError, EMPTY, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {
  contacts: Contact[] = [];
  searchTerm: string = '';

  constructor(
    private contactsService: ContactsService,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  loadData(){
    this.contactsService.apiContactsGet().pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.contacts = x;
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteContactContactIdPut(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
      this.dialogService.open('Erfolgreich gelöscht', `Der Kontakt wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }
}
