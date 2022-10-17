import {Component, OnInit, ViewChild} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../document-information/document-information.component";
import {Contact, ContactDto, Offer, OfferDto} from "../../../../client";

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: OfferComponent
    }
  ]
})
export class OfferComponent implements OnInit, ControlValueAccessor {
  offerForm: FormGroup = this.fb.group({
    headerText: [''],
    offerDate: [new Date(), Validators.required],
    validUntil: [new Date(), Validators.required],
    flowText: [''],
    offerNumber: [''],
    status: ['undefined', [Validators.required, undefinedValidator()]],
    subject: [''],
    documentInformation: [null, []],
  });
  @ViewChild('documentInformation') private documentInformationComponent: DocumentInformationComponent | undefined = undefined;

  onChange = (x: OfferDto) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;
  alreadyTouched = false;

  constructor(
    private fb: FormBuilder
  ) { }

  writeValue(obj: any): void {
    const y = obj as Offer;
    if(y == null){
      return;
    }
    this.flowText?.setValue(y.flowText);
    this.headerText?.setValue(y.headerText);
    this.subject?.setValue(y.subject);
    this.status?.setValue(y.status);
    this.offerDate?.setValue(y.offerDate);
    this.validUntil?.setValue(y.validUntil);
    this.offerNumber?.setValue(y.offerNumber);
    this.documentInformation?.setValue(y.documentInformation);
  }

  ngOnInit(): void {
    this.offerForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          offerDate: this.offerDate?.value,
          offerNumber: this.offerNumber?.value,
          status: this.status?.value,
          flowText: this.flowText?.value,
          validUntil: this.validUntil?.value,
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

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  get flowText() {
    return this.offerForm.get('flowText');
  }

  get headerText() {
    return this.offerForm.get('headerText');
  }

  get subject(){
    return this.offerForm.get('subject');
  }

  get status(){
    return this.offerForm.get('status');
  }

  get offerDate(){
    return this.offerForm.get('offerDate');
  }

  get validUntil(){
    return this.offerForm.get('validUntil');
  }

  get offerNumber(){
    return this.offerForm.get('offerNumber');
  }

  get documentInformation(){
    return this.offerForm.get('documentInformation');
  }

  isValid(){
    return this.offerForm.valid && this.documentInformationComponent?.isValid();
  }

  markAllAsTouched(){
    this.offerForm.markAllAsTouched();
    this.documentInformationComponent?.markAllAsTouched();
  }
}
