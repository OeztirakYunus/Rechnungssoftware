import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatDialog, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'src/app/model/user.model';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;

  private userSubscribtion?: Subscription;

  registerForm = new FormGroup({
    company: new FormControl(),
    user: new FormControl(),
    bankInformation: new FormControl()
  });
  constructor(private _formBuilder: FormBuilder) {}

  formControl = new FormControl('', [
    Validators.required
    // Validators.email,
  ]);

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
  } 

  get registerFormControls(){ return this.registerForm.controls;}

  confirmAdd() {}

  onNoClick(): void {
    // this.dialogRef.close(this.data);
  }
}
