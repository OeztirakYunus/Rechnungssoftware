import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, Subject, Subscription, take, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private userSubscribtion : Subscription = new Subscription;
  loginForm = new FormGroup({
    email: new FormControl(),
    password: new FormControl()
  });


  constructor(private authService : AuthService, private router: Router, private httpClient : HttpClient) { }

  public ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.userSubscribtion.unsubscribe();
  }

  get loginFormControls(){ return this.loginForm.controls;}

  async onSubmit() {
    /**
     * User: office@invoicer.at
     * Pw: invoicer2022
     */
    let email : string = this.loginFormControls['email'].value;
    let password : string = this.loginFormControls['password'].value;
    console.log(email + password);

    await this.authService.login(email, password);
    this.router.navigate(['']);
  }

}
