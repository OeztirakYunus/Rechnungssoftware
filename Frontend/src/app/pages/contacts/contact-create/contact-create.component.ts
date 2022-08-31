import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {
  CompaniesService,
  ContactDto,
  InvoicesService
} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {Router} from "@angular/router";
import {ContactComponent} from "../../../forms/contact/contact.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css']
})
export class ContactCreateComponent implements OnInit {
  contact: ContactDto | undefined = undefined;
  @ViewChild('contactElement') private contactComponent: ContactComponent | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  addContact() {
    this.contactComponent?.markAllAsTouched();

    if(this.contactComponent?.isValid()){
      this.companiesService.apiCompaniesAddContactPut(this.contact).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Der Kontakt wurde erfolgreich angelegt!',
          () => {
            this.router.navigate(['/contacts']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
