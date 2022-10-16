import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service.service';
import { AppCookieService } from 'src/app/services/cookie/app-cookie.service';
import { JwttokenService } from 'src/app/services/jwt/jwttoken.service';
import { SnackbarComponent } from '../snackbar/snackbar/snackbar.component';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  public constructor(private auth : AuthService, private cookieService : AppCookieService, private jwtService : JwttokenService, private snackBar : SnackbarComponent) { }

  isLoggedIn() {
    if (this.cookieService.get('AuthToken') == null || this.jwtService.isTokenExpired(this.cookieService.get('AuthToken'))) {
      return false;
    }
    else {
      return true;
    }
  }

  logout(){
    this.auth.logout();
    this.snackBar.snackbarSuccess("Erfolgreich ausgeloggt!");
  }

}
