import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {
  CompaniesService,
  OfferDto,
} from "../../../../../client";
import {DocumentInformationComponent} from "../../../forms/document-information/document-information.component";
import {DialogService} from "../../../services/dialog.service";
import {Router} from "@angular/router";
import {OfferComponent} from "../../../forms/offer/offer.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-offer-create',
  templateUrl: './offer-create.component.html',
  styleUrls: ['./offer-create.component.css']
})
export class OfferCreateComponent implements OnInit {
  offer: OfferDto | undefined = undefined;
  @ViewChild('offerElement') private offerElement: OfferComponent | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void{}


  addOffer() {
    this.offerElement?.markAllAsTouched();

    if(this.offerElement?.isValid()){
      this.companiesService.apiCompaniesAddOfferPut(this.offer).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Das Angebot wurde erfolgreich angelegt!',
          () => {
            this.router.navigate(['/offers']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
