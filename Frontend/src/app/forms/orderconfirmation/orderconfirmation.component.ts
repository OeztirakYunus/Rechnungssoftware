import {Component, OnInit, ViewChild} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../document-information/document-information.component";
import {OrderConfirmation, OrderConfirmationDto} from "../../../../client";

@Component({
  selector: 'app-orderconfirmation',
  templateUrl: './orderconfirmation.component.html',
  styleUrls: ['./orderconfirmation.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: OrderconfirmationComponent
    }
  ]
})
export class OrderconfirmationComponent implements OnInit, ControlValueAccessor {
  orderconfirmationForm: FormGroup = this.fb.group({
    headerText: ['', Validators.required],
    orderConfirmationDate: [new Date(), Validators.required],
    flowText: ['', Validators.required],
    orderConfirmationNumber: ['', Validators.required],
    status: ['undefined', [Validators.required, undefinedValidator()]],
    subject: ['', [Validators.required]],
    documentInformation: [null, []],
  });
  private state: OrderConfirmation | undefined = undefined;
  @ViewChild('documentInformation') private documentInformationComponent: DocumentInformationComponent | undefined = undefined;

  onChange = (x: OrderConfirmationDto) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;
  alreadyTouched = false;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.orderconfirmationForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          ...this.state,
          orderConfirmationNumber: this.orderConfirmationNumber?.value,
          orderConfirmationDate: this.orderConfirmationDate?.value,
          status: this.status?.value,
          flowText: this.flowText?.value,
          subject: this.subject?.value,
          documentInformation: this.documentInformation?.value,
          headerText: this.headerText?.value
        });
      }
      if(!this.alreadyTouched && this.onTouched){
        this.onTouched();
      }
    });
  }

  writeValue(obj: any): void {
    const y = obj as OrderConfirmation;
    if(y == null){
      return;
    }
    this.state = y;
    this.flowText?.setValue(y.flowText);
    this.headerText?.setValue(y.headerText);
    this.subject?.setValue(y.subject);
    this.status?.setValue(y.status);
    this.orderConfirmationDate?.setValue(y.orderConfirmationDate);
    this.orderConfirmationNumber?.setValue(y.orderConfirmationNumber);
    this.documentInformation?.setValue(y.documentInformation);
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  get flowText() {
    return this.orderconfirmationForm.get('flowText');
  }

  get headerText() {
    return this.orderconfirmationForm.get('headerText');
  }

  get subject(){
    return this.orderconfirmationForm.get('subject');
  }

  get status(){
    return this.orderconfirmationForm.get('status');
  }

  get orderConfirmationDate(){
    return this.orderconfirmationForm.get('orderConfirmationDate');
  }

  get orderConfirmationNumber(){
    return this.orderconfirmationForm.get('orderConfirmationNumber');
  }

  get documentInformation(){
    return this.orderconfirmationForm.get('documentInformation');
  }

  isValid(){
    return this.orderconfirmationForm.valid && this.documentInformationComponent?.isValid();
  }

  markAllAsTouched(){
    this.orderconfirmationForm.markAllAsTouched();
    this.documentInformationComponent?.markAllAsTouched();
  }
}
