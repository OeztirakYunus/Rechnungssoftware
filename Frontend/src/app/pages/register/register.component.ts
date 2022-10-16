import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {AuthService, RegisterDto} from "../../../../client";
import {DialogService} from "../../services/dialog.service";
import {emailValidator} from "../../validators/emailValidator";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userForm: FormGroup = this.fb.group({
    company: this.fb.group({
      companyName: ['', Validators.required],
      email: ['', [Validators.required, emailValidator()]],
      phoneNumber: ['', Validators.required],
      ustNumber: ['', Validators.required],
      address: this.fb.group({
        street: ['', Validators.required],
        zipCode: ['', Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
      }),
      bankInformation: this.fb.group({
        bankName: ['', Validators.required],
        iban: ['', Validators.required],
        bic: ['', Validators.required]
      })
    }),
    user: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, emailValidator()]],
      password: ['', Validators.required],
    })
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialogService: DialogService,
    private router: Router
  ) {
  }

  get bankName(){
    return this.userForm.get('company.bankInformation.bankName');
  }
  get iban(){
    return this.userForm.get('company.bankInformation.iban');
  }
  get bic(){
    return this.userForm.get('company.bankInformation.bic');
  }

  get street(){
    return this.userForm.get('company.address.street');
  }
  get zipCode(){
    return this.userForm.get('company.address.zipCode');
  }
  get city(){
    return this.userForm.get('company.address.city');
  }
  get country(){
    return this.userForm.get('company.address.country');
  }

  get firstName(){
    return this.userForm.get('user.firstName');
  }
  get lastName(){
    return this.userForm.get('user.lastName');
  }
  get email(){
    return this.userForm.get('user.email');
  }
  get password(){
    return this.userForm.get('user.password');
  }

  get companyName(){
    return this.userForm.get('company.companyName');
  }

  get companyEmail(){
    return this.userForm.get('company.email');
  }

  get phoneNumber(){
    return this.userForm.get('company.phoneNumber');
  }

  get ustNumber(){
    return this.userForm.get('company.ustNumber');
  }


  ngOnInit() {
  }

  getData(): RegisterDto{
    return {
      user: {
        password: this.password?.value,
        email: this.email?.value,
        lastName: this.lastName?.value,
        firstName: this.firstName?.value,
      },
      company: {
        bankInformation: {
          bic: this.bic?.value,
          bankName: this.bankName?.value,
          iban: this.iban?.value
        },
        companyName: this.companyName?.value,
        email: this.companyEmail?.value,
        ustNumber: this.ustNumber?.value,
        address: {
          street: this.street?.value,
          city: this.city?.value,
          zipCode: this.zipCode?.value,
          country: this.country?.value
        },
        phoneNumber: this.phoneNumber?.value,
      }
    }
  }

  register() {
    this.userForm.markAllAsTouched();
    if(!this.userForm.valid){
      return;
    }
    this.authService.apiAuthRegisterPost(this.getData()).subscribe(x => {
      this.dialogService.open('Konto angelegt', 'Benutzeraccount wurde erfolgreich angelegt');
      this.router.navigate(['']);
    });
  }
}
