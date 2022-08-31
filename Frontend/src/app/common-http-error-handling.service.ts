import { Injectable } from '@angular/core';
import {DialogService} from "./services/dialog.service";
import {catchError, OperatorFunction, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CommonHttpErrorHandlingService {
  constructor(
    private dialogService: DialogService
  ) {}

  catchError<T1>(): OperatorFunction<T1,any> {
    return catchError((error) => {
      if(error instanceof HttpErrorResponse){
        this.dialogService.open('Fehler beim Vorgang aufgetreten', 'Es ist leider bei diesem Vorgang ein Fehler aufgetreten. Fehlermeldung: ' + error.error.message);
      }
      return throwError(error);
    });
  }
}
