import {Component, OnInit} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {UserModel} from "../../models/UserModel";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: UserComponent
    }
  ]
})
export class UserComponent implements OnInit, ControlValueAccessor {
  userForm: FormGroup = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
    role: ['User', Validators.required]
  });

  onChange = (x: UserModel) => {};
  onTouched = () => {};
  registerOnValidatorChange?(fn: () => void): void;
  alreadyTouched = false;

  ngOnInit(): void {
    this.userForm.valueChanges.subscribe(x => {
      if(this.onChange != null){
        this.onChange({
          firstName: x.firstName,
          password: x.password,
          email: x.email,
          lastName: x.lastName,
          role: x.role
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

  constructor(
    private fb: FormBuilder
  ) { }

  writeValue(obj: any): void {
    const typed = obj as UserModel;
    if(typed == null){
      return;
    }
    this.firstName?.setValue(typed.firstName);
    this.lastName?.setValue(typed.lastName);
    this.email?.setValue(typed.email);
    this.password?.setValue(typed.password);
    this.role?.setValue(typed.role);
  }

  isValid(){
    return this.userForm.valid;
  }

  markAllAsTouched(){
    this.userForm.markAllAsTouched();
  }

  get firstName(){
    return this.userForm.get('firstName');
  }

  get lastName(){
    return this.userForm.get('lastName');
  }

  get email(){
    return this.userForm.get('email');
  }

  get password(){
    return this.userForm.get('password');
  }

  get role(){
    return this.userForm.get('role');
  }
}
