import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AppCookieService} from "./services/cookie/app-cookie.service";

@Injectable()
export class AuthInterceptorInterceptor implements HttpInterceptor {

  constructor(private cookieService: AppCookieService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.cookieService.get('AuthToken');
    var newRequest = request;
    if(token != null){
      newRequest = request.clone({
        headers: request.headers.set(`Authorization`, `Bearer ${this.cookieService.get('AuthToken')}`)
      });
    }
    return next.handle(newRequest);
  }
}
