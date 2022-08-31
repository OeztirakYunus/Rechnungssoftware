import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { SnackbarComponent } from 'src/app/components/snackbar/snackbar/snackbar.component';
import { environment } from 'src/environments/environment';
import { AppCookieService } from '../cookie/app-cookie.service';
import {DeliveryNote} from "../../../../client";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  @Output() mainDataImported = new EventEmitter<boolean>();
  private url = environment.apiUrl;
  private httpClient: HttpClient;
  public deliveryNotes?: DeliveryNote[];

  constructor(http: HttpClient, private cookieService: AppCookieService, private snackBar: SnackbarComponent) {
    this.httpClient = http;
  }

  public async initData(): Promise<void> {
    await this.getDeliveryNotes();
  }

  async getDeliveryNotes() {
    var path = "DeliveryNotes"
    var headers = new HttpHeaders().set('Authorization', 'Bearer ' + this.cookieService.get('AuthToken'));

    try {
      this.deliveryNotes = await this.httpClient.get<DeliveryNote[]>(this.url + path, {headers}).toPromise();
    }
    catch (error: any) {
      console.log(error);
      this.snackBar.snackbarError("Error: " + error["Message"]);
    }
  }





}
