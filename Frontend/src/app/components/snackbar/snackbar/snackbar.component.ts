import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.css']
})
export class SnackbarComponent {

  constructor(private snackBar: MatSnackBar) { }

  private configSuccess: MatSnackBarConfig = {
    horizontalPosition: 'center',
    verticalPosition: 'top',
    duration: 5000, //5 Seconds
    panelClass: ['style-success']
  };

  public snackbarSuccess(message: string) {
    this.snackBar.open(message, "Schließen", this.configSuccess);
  }

  public snackbarError(message: string) {
    var configError: MatSnackBarConfig = {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      duration: 5000, //5 Seconds
      panelClass: ['style-error']
    };
    this.snackBar.open(message, 'Schließen', configError);
  }
}
