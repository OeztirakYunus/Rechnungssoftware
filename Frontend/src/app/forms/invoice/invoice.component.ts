import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {
  Invoice, InvoiceDto,
  Product,
  ProductsService
} from "../../../../client";
import {DocumentInformationComponent} from "../document-information/document-information.component";
import {DialogService} from "../../services/dialog.service";

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: InvoiceComponent
    }
  ]
})
export class InvoiceComponent implements OnInit {
  invoiceForm: FormGroup = this.fb.group({
    headerText: ['', Validators.required],
    invoiceDate: [new Date(), Validators.required],
    paymentTerm: [new Date(), Validators.required],
    flowText: ['', Validators.required],
    invoiceNumber: ['', Validators.required],
    status: ['undefined', [Validators.required, undefinedValidator()]],
    subject: ['', [Validators.required]],
    documentInformation: [null, []],
  });
  products: Product[] = [];
  invoice: Invoice | undefined = undefined;
  alreadyTouched = false;
  @ViewChild('documentInformation') private documentInformationComponent: DocumentInformationComponent | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private productsService: ProductsService
  ) { }

  onChange = (x: InvoiceDto) => {};
  onTouched = () => {};

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  ngOnInit(): void {
    this.invoiceForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          ...this.state,
          invoiceNumber: this.invoiceNumber?.value,
          paymentTerm: this.paymentTerm?.value,
          status: this.status?.value,
          flowText: this.flowText?.value,
          invoiceDate: this.invoiceDate?.value,
          subject: this.subject?.value,
          documentInformation: this.documentInformation?.value,
          headerText: this.headerText?.value
        });
      }
      if(!this.alreadyTouched && this.onTouched){
        this.onTouched();
      }
    });
    this.productsService.apiProductsGet().subscribe(x => {
      this.products = x;
    });
  }

  private state: Invoice | undefined = undefined;

  writeValue(obj: any): void {
    const y = obj as Invoice;
    if(y == null){
      return;
    }
    this.state = y;
    this.flowText?.setValue(y.flowText);
    this.headerText?.setValue(y.headerText);
    this.subject?.setValue(y.subject);
    this.status?.setValue(y.status);
    this.invoiceDate?.setValue(y.invoiceDate);
    this.paymentTerm?.setValue(y.paymentTerm);
    this.invoiceNumber?.setValue(y.invoiceNumber);
    this.documentInformation?.setValue(y.documentInformation);
  }

  get flowText() {
    return this.invoiceForm.get('flowText');
  }

  get headerText() {
    return this.invoiceForm.get('headerText');
  }

  get subject(){
    return this.invoiceForm.get('subject');
  }

  get status(){
    return this.invoiceForm.get('status');
  }

  get invoiceDate(){
    return this.invoiceForm.get('invoiceDate');
  }

  get paymentTerm(){
    return this.invoiceForm.get('paymentTerm');
  }

  get invoiceNumber(){
    return this.invoiceForm.get('invoiceNumber');
  }

  get documentInformation(){
    return this.invoiceForm.get('documentInformation');
  }

  isValid(){
    return this.invoiceForm.valid && this.documentInformationComponent?.isValid();
  }

  markAllAsTouched(){
    this.invoiceForm.markAllAsTouched();
    this.documentInformationComponent?.markAllAsTouched();
  }
}
