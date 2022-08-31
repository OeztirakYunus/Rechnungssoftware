import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../../../forms/document-information/document-information.component";
import {DialogService} from "../../../services/dialog.service";
import {
  CompaniesService,
  OrderConfirmation, OrderConfirmationDto,
  OrderConfirmationsService
} from "../../../../../client";
import {ActivatedRoute, Router} from "@angular/router";
import {OrderconfirmationComponent} from "../../../forms/orderconfirmation/orderconfirmation.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-orderconfirmation-edit',
  templateUrl: './orderconfirmation-edit.component.html',
  styleUrls: ['./orderconfirmation-edit.component.css']
})
export class OrderconfirmationEditComponent implements OnInit {
  @ViewChild('orderconfirmationElement') private orderconfirmationElement: OrderconfirmationComponent | undefined = undefined;
  orderConfirmation: OrderConfirmation | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private orderConfirmationsService: OrderConfirmationsService,
    private companiesService: CompaniesService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.orderConfirmationsService.apiOrderConfirmationsIdGet(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(y => {
        this.orderConfirmation = y;
      });
    })
  }

  update() {
    this.orderconfirmationElement?.markAllAsTouched();
    if(this.orderconfirmationElement?.isValid()){
      this.orderConfirmationsService.apiOrderConfirmationsPut(this.orderConfirmation).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich aktualisiert', 'Die Auftragsbestätigung wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/orderconfirmation']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
