import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class JwttokenService {
  helper = new JwtHelperService();
  constructor() { }

  getDecodeToken(jwtToken:string) {
    return this.helper.decodeToken(jwtToken);
  }

  getExpiryTime(jwtToken:string) {
    return this.helper.getTokenExpirationDate(jwtToken);
  }

  isTokenExpired(jwtToken:string): boolean {
    return this.helper.isTokenExpired(jwtToken);
  }
  
}
