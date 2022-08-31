import {Component, OnInit, ViewChild} from '@angular/core';
import {CompaniesService, UserAddDto} from "../../../../../client";
import {UserComponent} from "../../../forms/user/user.component";
import {DialogService} from "../../../services/dialog.service";
import {Router} from "@angular/router";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {
  user: UserAddDto | undefined = undefined;
  @ViewChild('userElement') private userElement: UserComponent | undefined = undefined;

  constructor(
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }

  create(){
    this.userElement?.markAllAsTouched();
    if(this.userElement?.isValid()){
      this.companiesService.apiCompaniesAddUserPut(this.user).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich durchgeführt','Der Benutzer wurde erfolgreich erstellt', () => {
          this.router.navigate(['/users']);
        });
      })
    }else{
      this.dialogService.open('Eingabe überprüfen','Überprüfen Sie bitte Ihre Eingaben');
    }
  }
}
