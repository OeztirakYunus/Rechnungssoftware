import { Component, OnInit } from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {AddressModel} from "../../models/AddressModel";

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: AddressComponent
    }]
})
export class AddressComponent implements OnInit, ControlValueAccessor {
  addressForm: FormGroup = this.fb.group({
    street: ['',  Validators.required],
    zipCode: ['',  Validators.required],
    city: ['',  Validators.required],
    country: ['',  Validators.required],
  });
  onChange = (x: AddressModel) => {};
  onTouched = () => {};

    constructor(
    private fb: FormBuilder
  ) { }

  get street(){
    return this.addressForm.get('street');
  }

  get zipCode(){
    return this.addressForm.get('zipCode');
  }

  get city(){
    return this.addressForm.get('city');
  }

  get country(){
    return this.addressForm.get('country');
  }

  ngOnInit(): void {
      this.addressForm.valueChanges.subscribe(x => {
        const obj : AddressModel = {
          street: x.street,
          city: x.city,
          zipCode: x.zipCode,
          country: x.country
        };
        this.onChange(obj);
      })
  }

  registerOnChange(fn: any): void {
      this.onChange = fn;
  }

  markAllAsTouched(){
      this.addressForm.markAllAsTouched();
  }

  registerOnTouched(fn: any): void {
      this.onTouched = fn;
  }

  writeValue(obj: any): void {
      const typedObj = obj as AddressModel;
      if(typedObj == null){
        return;
      }
      this.country?.setValue(typedObj.country);
      this.city?.setValue(typedObj.city);
      this.street?.setValue(typedObj.street);
      this.zipCode?.setValue(typedObj.zipCode);
  }

  isValid(){
    return this.addressForm.valid;
  }
}
