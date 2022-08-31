import { Component, OnInit } from '@angular/core';
import {CompaniesService, UserDto, UsersService} from "../../../../../client";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";
import {DialogService} from "../../../services/dialog.service";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: UserDto[] = [];
  searchTerm: string = '';

  constructor(
    private usersService: UsersService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService,
    private companiesService: CompaniesService,
    private dialogService: DialogService
  ) { }

  loadData(){
    this.usersService.apiUsersGet().pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.users = x;
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

  create(){

  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteUserUserIdPut(id).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Der Benutzer wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }
}
