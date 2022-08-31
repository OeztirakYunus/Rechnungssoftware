import {Component, OnInit, QueryList, ViewChildren} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormArray,
  FormBuilder,
  FormGroup, NG_VALIDATORS,
  NG_VALUE_ACCESSOR, ValidationErrors,
  Validator,
  Validators
} from "@angular/forms";
import {doubleValidator} from "../../validators/doubleValidator";
import {
  Contact,
  ContactsService, DocumentInformationDto,
  DocumentInformations,
  PositionDto,
  Product,
  ProductsService,
  User,
  UsersService
} from "../../../../client";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {PositionComponent} from "../position/position.component";

@Component({
  selector: 'app-document-information',
  templateUrl: './document-information.component.html',
  styleUrls: ['./document-information.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: DocumentInformationComponent
    },
    {
      provide: NG_VALIDATORS,
      useExisting: DocumentInformationComponent,
      multi: true,
    }
  ]
})
export class DocumentInformationComponent implements OnInit, ControlValueAccessor, Validator {
  invoiceForm: FormGroup = this.fb.group({
    totalDiscount: ['0', [Validators.required, doubleValidator(), Validators.min(0)]],
    tax: ['0', [Validators.required, doubleValidator(), Validators.min(0)]],
    typeOfDiscount: ['undefined', [undefinedValidator()]],
    clientId: ['undefined', [undefinedValidator()]],
    contactPersonId: ['undefined', [undefinedValidator()]],
    positions: this.fb.array([])
  });
  products: Product[] = [];
  onChange = (x: any) => {};
  onTouched = () => {};
  onValidatorChange = () => {};
  users: User[] = [];
  contacts: Contact[] = [];

  constructor(
    private fb: FormBuilder,
    private productsService: ProductsService,
    private usersService: UsersService,
    private contactsService: ContactsService
  ) { }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  private state: DocumentInformations | undefined = undefined;

  writeValue(obj: any): void {
    const typed = obj as DocumentInformations;
    if(typed == null){
      return;
    }
    this.state = typed;
    this.totalDiscount?.setValue(typed.totalDiscount?.toString().replace('.',','));
    this.tax?.setValue(typed.tax?.toString().replace('.',','));
    this.clientId?.setValue(typed.clientId);
    this.contactPersonId?.setValue(typed.contactPersonId);
    this.typeOfDiscount?.setValue(typed.typeOfDiscount);
    for (const position of typed.positions ?? []) {
      const emptyPosition:PositionDto = {
        ...position,
        quantity: position.quantity,
        productId: position.productId,
        discount: position.discount,
        typeOfDiscount: position.typeOfDiscount
      };
      this.positions.push(this.fb.control(emptyPosition));
    }
  }
  alreadyTouched = false;

  ngOnInit(): void {
    this.invoiceForm.valueChanges.subscribe(x => {
      const pos : DocumentInformationDto = {
        ...this.state,
        totalDiscount: Number(x.totalDiscount.replace(',','.')),
        tax: Number(x.tax.replace(',','.')),
        clientId: x.clientId,
        contactPersonId: x.contactPersonId,
        positions: x.positions,
        typeOfDiscount: x.typeOfDiscount
      }
      this.onChange(pos);

      if(!this.alreadyTouched && this.onTouched){
        this.onTouched();
        this.alreadyTouched = true;
      }
    });


    this.productsService.apiProductsGet().subscribe(x => {
      this.products = x;
    })
    this.usersService.apiUsersGet().subscribe(x => {
      this.users = x;
    });
    this.contactsService.apiContactsGet().subscribe(x => {
      this.contacts = x;
    });
  }

  get positions() {
    return this.invoiceForm.get('positions') as FormArray;
  }

  get tax() {
    return this.invoiceForm.get('tax');
  }

  get totalDiscount(){
    return this.invoiceForm.get('totalDiscount');
  }

  get clientId(){
    return this.invoiceForm.get('clientId');
  }

  get contactPersonId(){
    return this.invoiceForm.get('contactPersonId');
  }

  get typeOfDiscount(){
    return this.invoiceForm.get('typeOfDiscount');
  }

  addPosition() {
    const emptyPosition:PositionDto = {
      quantity: 1,
      productId: undefined,
      discount: 0,
      typeOfDiscount: undefined
    };
    this.positions.push(this.fb.control(emptyPosition));
  }

  deletePosition(i: number) {
    this.positions.removeAt(i);
  }

  validate(control: AbstractControl): ValidationErrors | null {
    const validator = this.invoiceForm.validator;
    if (validator) {
      return validator(control);;
    }
    return null;
  }

  @ViewChildren('positions') components: QueryList<PositionComponent> | undefined;

  public markAllAsTouched(){
    this.invoiceForm.markAllAsTouched();
    for (const argument of this.components?.toArray()??[]) {
      argument.markAllAsTouched();
    }
  }

  isValid(){
    if(!this.invoiceForm.valid){
      return false;
    }
    for (const argument of this.components?.toArray()??[]) {
      if(!argument.isValid()){
        return false;
      }
    }
    return true;
  }

  registerOnValidatorChange?(fn: () => void){
    this.onValidatorChange = fn;
  }
}
