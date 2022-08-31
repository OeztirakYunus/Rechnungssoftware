import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../../../forms/document-information/document-information.component";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, OfferDto, OrderConfirmation, OrderConfirmationDto} from "../../../../../client";
import {Router} from "@angular/router";
import {OrderconfirmationComponent} from "../../../forms/orderconfirmation/orderconfirmation.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-orderconfirmation-create',
  templateUrl: './orderconfirmation-create.component.html',
  styleUrls: ['./orderconfirmation-create.component.css']
})
export class OrderconfirmationCreateComponent implements OnInit {
  @ViewChild('orderconfirmationElement') private orderconfirmationElement: OrderconfirmationComponent | undefined = undefined;
  orderConfirmation: OrderConfirmationDto | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  addOrderConfirmation() {
    this.orderconfirmationElement?.markAllAsTouched();
    if(this.orderconfirmationElement?.isValid()){
      this.companiesService.apiCompaniesAddOrderConfirmationPut(this.orderConfirmation).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Die Auftragsbestätigung wurde erfolgreich angelegt!',
          () => {
            this.router.navigate(['/orderconfirmation']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
