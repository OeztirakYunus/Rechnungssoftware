import {Component, OnInit, ViewChild} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {undefinedValidator} from "../../validators/undefinedValidator";
import {AddressComponent} from "../address/address.component";
import {Contact, ContactDto} from "../../../../client";

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: ContactComponent
    }
  ]
})
export class ContactComponent implements OnInit, ControlValueAccessor {
  contactForm: FormGroup = this.fb.group({
    typeOfContactEnum: ['undefined', [Validators.required, undefinedValidator()]],
    gender: ['undefined', [Validators.required, undefinedValidator()]],
    title: [''],
    firstName	: ['', Validators.required],
    lastName: ['', Validators.required],
    nameOfOrganisation: [''],
    phoneNumber: [''],
    email: [''],
    address: ['', [Validators.required]]
  });
  @ViewChild('address') private addressComponent: AddressComponent | undefined = undefined;

  onChange = (x: ContactDto) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;
  alreadyTouched = false;
  private state: Contact | undefined = undefined;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.contactForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          ...this.state,
          typeOfContactEnum: x.typeOfContactEnum,
          gender: x.gender,
          title: x.title,
          firstName: x.firstName,
          lastName: x.lastName,
          nameOfOrganisation: x.nameOfOrganisation,
          phoneNumber: x.phoneNumber,
          email: x.email,
          address: x.address
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

  writeValue(obj: any): void {
    const x = obj as Contact;
    if(x == null){
      return;
    }
    this.state = x;
    this.typeOfContactEnum?.setValue(x.typeOfContactEnum);
    this.gender?.setValue(x.gender);
    this.title?.setValue(x.title);
    this.firstName?.setValue(x.firstName);
    this.lastName?.setValue(x.lastName);
    this.nameOfOrganisation?.setValue(x.nameOfOrganisation);
    this.phoneNumber?.setValue(x.phoneNumber);
    this.email?.setValue(x.email);
    this.address?.setValue(x.address);
  }

  get typeOfContactEnum() {
    return this.contactForm.get('typeOfContactEnum');
  }

  get gender() {
    return this.contactForm.get('gender');
  }

  get title(){
    return this.contactForm.get('title');
  }

  get firstName(){
    return this.contactForm.get('firstName');
  }

  get lastName(){
    return this.contactForm.get('lastName');
  }

  get nameOfOrganisation(){
    return this.contactForm.get('nameOfOrganisation');
  }

  get phoneNumber(){
    return this.contactForm.get('phoneNumber');
  }

  get email(){
    return this.contactForm.get('email');
  }

  get address(){
    return this.contactForm.get('address');
  }

  isValid(){
    return this.contactForm.valid && this.addressComponent?.isValid();
  }

  markAllAsTouched(){
    this.contactForm.markAllAsTouched();
    this.addressComponent?.markAllAsTouched();
  }
}
