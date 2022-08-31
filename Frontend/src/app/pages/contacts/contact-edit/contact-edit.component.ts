import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup} from "@angular/forms";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, Contact, ContactDto, ContactsService, InvoicesService} from "../../../../../client";
import {ContactComponent} from "../../../forms/contact/contact.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit {
  contact: Contact | undefined = undefined;
  @ViewChild('contactElement') private contactComponent: ContactComponent | undefined = undefined;


  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.contactsService.apiContactsIdGet(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
        this.contact = x;
      });
    })
  }

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private contactsService: ContactsService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  editContact() {
    this.contactComponent?.markAllAsTouched();

    if(this.contactComponent?.isValid()){
      this.contactsService.apiContactsPut(this.contact).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Der Kontakt wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/contacts']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
