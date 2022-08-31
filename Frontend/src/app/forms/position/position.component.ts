import {Component, Input} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormBuilder,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR, ValidationErrors, Validator,
  Validators
} from "@angular/forms";
import {doubleValidator} from "../../validators/doubleValidator";
import {intValidator} from "../../validators/intValidator";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {ProductModel} from "../../models/ProductModel";
import {PositionModel} from "../../models/PositionModel";

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: PositionComponent
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: PositionComponent
    }
  ]
})
export class PositionComponent implements ControlValueAccessor, Validator  {
  _products: ProductModel[] = [];
  positionForm: FormGroup = this.fb.group({
      quantity: ['1', [Validators.required, intValidator(), Validators.min(1)]],
      discount: ['0', [Validators.required, doubleValidator(), Validators.min(0)]],
      productId: ['undefined', [Validators.required, undefinedValidator()]],
      typeOfDiscount: ['undefined', [Validators.required, undefinedValidator()]]
    }
  );

  get products(): ProductModel[] {
    return this._products;
  }
  @Input() set products(products: ProductModel[]){
    this._products = products;
  }

  onChange = (x: PositionModel) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;

  alreadyTouched = false;

  constructor(
    private fb: FormBuilder
  ) {
    this.positionForm.valueChanges.subscribe(x => {
      const pos : PositionModel = {
        ...this.state,
        quantity: Number(x.quantity),
        discount: Number(x.discount.replace(',','.')),
        productId: x.productId,
        typeOfDiscount: x.typeOfDiscount
      }
      this.onChange(pos);

      if(!this.alreadyTouched && this.onTouched){
        this.onTouched();
        this.alreadyTouched = true;
      }
    });
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  private state: PositionModel | undefined = undefined;

  writeValue(obj: any): void {
    const typed = obj as PositionModel;
    if(typed == null){
      return;
    }
    this.state = typed;
    this.quantity?.setValue(typed.quantity);
    this.discount?.setValue(typed.discount?.toString().replace('.',','));
    this.typeOfDiscount?.setValue(typed.typeOfDiscount);
    this.productId?.setValue(typed.productId);
  }

  get quantity(){
    return this.positionForm.get('quantity');
  }
  get discount(){
    return this.positionForm.get('discount');
  }
  get productId(){
    return this.positionForm.get('productId');
  }
  get typeOfDiscount(){
    return this.positionForm.get('typeOfDiscount');
  }

  public markAllAsTouched(){
    this.positionForm.markAllAsTouched();
  }

  validate(control: AbstractControl): ValidationErrors | null {
    const validator = this.positionForm.validator;
    if (validator) {
      return validator(control);
    }
    return null;
  }

  isValid(){
    return this.positionForm.valid;
  }
}
