import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { SnackbarComponent } from '../components/snackbar/snackbar/snackbar.component';

import { AppCookieService } from './cookie/app-cookie.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router, private cookieService: AppCookieService, private snackBar: SnackbarComponent) { }

  async login(email: string, password: string): Promise<void> {
    await this.logout();
    var path = '/api/Auth/login';
    var headers = new HttpHeaders().set('Authorization', 'Basic ' + btoa(email + ':' + password));
    try {
      var response = await this.httpClient.get<IAuthResponse>(environment.apiUrl + path,
        { headers }).toPromise();
      if(response != null) {
        this.cookieService.set('AuthToken', response.auth_token);
      }
    } catch (error: any) {
      this.snackBar.snackbarError("Error: " + error["error"]["message"]);
      console.log(error);
    }
  }

  async register(email: string, password: string) {
    var path = 'Auth/register';
    var user = new User();
    user.email = email;
    user.password = password;

    try {
      await this.httpClient.post<IAuthResponse>(environment.apiUrl + path, user).toPromise();
    } catch (error: any) {
      console.log(error);
      this.snackBar.snackbarError("Error: " + error["error"]["message"]);
    }
  }

  async logout() {
    try {
      this.cookieService.remove('AuthToken');
    } catch (error) {
      this.snackBar.snackbarError("Error: " + error);
    }
  }
}

  export interface IAuthResponse {
    auth_token: string
  }


  class User {
    email: string = "";
    password: string = "";

    User(email: string, password: string) {
      this.email = email;
      this.password = password;
    }
}
