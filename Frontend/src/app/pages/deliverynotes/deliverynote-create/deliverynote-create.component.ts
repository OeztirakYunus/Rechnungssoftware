import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../../../forms/document-information/document-information.component";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, DeliveryNote, DeliveryNoteDto, OfferDto} from "../../../../../client";
import {Router} from "@angular/router";
import {DeliverynoteComponent} from "../../../forms/deliverynote/deliverynote.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-deliverynote-create',
  templateUrl: './deliverynote-create.component.html',
  styleUrls: ['./deliverynote-create.component.css']
})
export class DeliverynoteCreateComponent implements OnInit {
  @ViewChild('deliverynoteElement') private deliverynoteElement: DeliverynoteComponent | undefined = undefined;
  deliveryNote: DeliveryNote | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  add() {
    this.deliverynoteElement?.markAllAsTouched();
    if(this.deliverynoteElement?.isValid()){
      this.companiesService.apiCompaniesAddDeliveryNotePut(this.deliveryNote).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Der Lieferschein wurde erfolgreich angelegt!',
          () => {
            this.router.navigate(['/deliverynotes']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
