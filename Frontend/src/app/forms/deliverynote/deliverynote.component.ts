import {Component, OnInit, ViewChild} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {DocumentInformationComponent} from "../document-information/document-information.component";
import {
  CompaniesService,
  DeliveryNote, DeliveryNoteDto,
  DeliveryNotesService,
  OrderConfirmation,
  OrderConfirmationDto
} from "../../../../client";
import {DialogService} from "../../services/dialog.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-deliverynote',
  templateUrl: './deliverynote.component.html',
  styleUrls: ['./deliverynote.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: DeliverynoteComponent
    }
  ]
})
export class DeliverynoteComponent implements OnInit, ControlValueAccessor {
  deliveryNoteForm: FormGroup = this.fb.group({
    headerText: [''],
    deliveryNoteDate: [new Date(), Validators.required],
    flowText: [''],
    deliveryNoteNumber: [''],
    status: ['undefined', [Validators.required, undefinedValidator()]],
    subject: [''],
    documentInformation: [null, []],
  });
  @ViewChild('documentInformation') private documentInformationComponent: DocumentInformationComponent | undefined = undefined;
  onChange = (x: DeliveryNoteDto) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;
  alreadyTouched = false;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private deliveryNotesService: DeliveryNotesService
  ) { }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  ngOnInit(): void {
    this.deliveryNoteForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          deliveryNoteNumber: this.deliveryNoteNumber?.value,
          deliveryNoteDate: this.deliveryNoteDate?.value,
          status: this.status?.value,
          flowText: this.flowText?.value,
          subject: this.subject?.value,
          headerText: this.headerText?.value,
          documentInformations: this.documentInformation?.value
        });
      }
      if(!this.alreadyTouched && this.onTouched){
        this.onTouched();
      }
    });
  }

  writeValue(obj: any): void {
    const y = obj as DeliveryNote;
    if(y == null){
      return;
    }
    this.flowText?.setValue(y.flowText);
    this.headerText?.setValue(y.headerText);
    this.subject?.setValue(y.subject);
    this.status?.setValue(y.status);
    this.deliveryNoteNumber?.setValue(y.deliveryNoteNumber);
    this.deliveryNoteDate?.setValue(y.deliveryNoteDate);
    this.documentInformation?.setValue(y.documentInformations);
  }

  get flowText() {
    return this.deliveryNoteForm.get('flowText');
  }

  get headerText() {
    return this.deliveryNoteForm.get('headerText');
  }

  get subject(){
    return this.deliveryNoteForm.get('subject');
  }

  get status(){
    return this.deliveryNoteForm.get('status');
  }

  get deliveryNoteDate(){
    return this.deliveryNoteForm.get('deliveryNoteDate');
  }

  get deliveryNoteNumber(){
    return this.deliveryNoteForm.get('deliveryNoteNumber');
  }

  get documentInformation(){
    return this.deliveryNoteForm.get('documentInformation');
  }

  isValid(){
    return this.deliveryNoteForm.valid && this.documentInformationComponent?.isValid();
  }

  markAllAsTouched(){
    this.deliveryNoteForm.markAllAsTouched();
    this.documentInformationComponent?.markAllAsTouched();
  }
}
