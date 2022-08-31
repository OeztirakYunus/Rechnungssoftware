import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {undefinedValidator} from "../../../validators/undefinedValidator";
import {
  CompaniesService,
  Invoice,
  InvoiceDto,
  InvoicesService,
  PositionsService,
  Product,
  ProductsService
} from "../../../../../client";
import {DocumentInformationComponent} from "../../../forms/document-information/document-information.component";
import {DialogService} from "../../../services/dialog.service";
import {ActivatedRoute, Router} from "@angular/router";
import {InvoiceComponent} from "../../../forms/invoice/invoice.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-invoice-edit',
  templateUrl: './invoice-edit.component.html',
  styleUrls: ['./invoice-edit.component.css']
})
export class InvoiceEditComponent implements OnInit {
  @ViewChild('invoiceElement') private invoiceElement: InvoiceComponent | undefined = undefined;
  invoice: Invoice | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private productsService: ProductsService,
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.invoicesService.apiInvoicesIdGet(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
        this.invoice = x;
      });
    })
  }

  addInvoice() {
    this.invoiceElement?.markAllAsTouched();

    if(this.invoiceElement?.isValid()){
      this.invoicesService.apiInvoicesPut(this.invoice).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Die Rechnung wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/invoices']);
          });
      });


    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
