import { Component, OnInit } from '@angular/core';
import {
  ControlValueAccessor,
  FormBuilder,
  FormGroup,
  NG_VALUE_ACCESSOR,
  Validators
} from "@angular/forms";
import {doubleValidator} from "../../validators/doubleValidator";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {ProductModel} from "../../models/ProductModel";
import {ProductUnitType} from "../../models/ProductUnitType";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: ProductComponent
    }
  ]
})
export class ProductComponent implements OnInit, ControlValueAccessor {
  public productUnit: typeof ProductUnitType = ProductUnitType;
  productForm: FormGroup = this.fb.group({
      articleNumber: ['', Validators.required],
      productName: ['', [Validators.required]],
      sellingPriceNet: ['', [Validators.required, Validators.min(0), doubleValidator()]],
      category: ['undefined', undefinedValidator()],
      unit: ['undefined', undefinedValidator()],
      description: [''],
    }
  );
  onChange = (x: ProductModel) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;

  alreadyTouched = false;
  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.productForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          ...this.state,
          articleNumber: x.articleNumber,
          unit: x.unit,
          productName: x.productName,
          category: x.category,
          description: x.description,
          sellingPriceNet: Number(x.sellingPriceNet.replace(',','.'))
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

  private state: ProductModel | undefined = undefined;

  writeValue(obj: any): void {
    const typed = obj as ProductModel;
    if(typed == null){
      return;
    }
    this.state = typed;
    this.articleNumber?.setValue(typed.articleNumber);
    this.unit?.setValue(typed.unit == undefined  ? "undefined" : typed.unit );
    this.category?.setValue(typed.category == undefined ? "undefined" : typed.category);
    this.productName?.setValue(typed.productName);
    this.description?.setValue(typed.description);
    this.sellingPriceNet?.setValue(typed.sellingPriceNet?.toString().replace('.',','));
  }

  get articleNumber(){
    return this.productForm.get('articleNumber');
  }
  get productName(){
    return this.productForm.get('productName');
  }
  get sellingPriceNet(){
    return this.productForm.get('sellingPriceNet');
  }
  get category(){
    return this.productForm.get('category');
  }
  get description(){
    return this.productForm.get('description');
  }
  get unit(){
    return this.productForm.get('unit');
  }

  isValid(){
    return this.productForm.valid;
  }

  markAllAsTouched(){
    this.productForm.markAllAsTouched();
  }
}
