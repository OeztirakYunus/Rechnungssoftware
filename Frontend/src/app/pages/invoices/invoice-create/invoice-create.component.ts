import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, InvoiceDto, InvoicesService, Product, ProductsService} from "../../../../../client";
import {Router} from "@angular/router";
import {InvoiceComponent} from "../../../forms/invoice/invoice.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-invoice-create',
  templateUrl: './invoice-create.component.html',
  styleUrls: ['./invoice-create.component.css']
})
export class InvoiceCreateComponent implements OnInit {
  @ViewChild('invoiceElement') private invoiceElement: InvoiceComponent | undefined = undefined;
  invoice: InvoiceDto | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private productsService: ProductsService,
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  addInvoice() {
    this.invoiceElement?.markAllAsTouched();
    if(this.invoiceElement?.isValid()){
      this.companiesService.apiCompaniesAddInvoicePut(this.invoice).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Die Rechnung wurde erfolgreich angelegt!',
          () => {
          this.router.navigate(['/invoices']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
