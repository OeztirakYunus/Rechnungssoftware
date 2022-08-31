import {Component, Inject, OnInit, Optional} from '@angular/core';
import {BASE_PATH, CompaniesService, Offer, OffersService, Status} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {HttpClient} from "@angular/common/http";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {
  public offers: Offer[] = [];
  public status: typeof Status = Status;
  searchTerm: string = "";

  constructor(
    private offerService: OffersService,
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private httpClient: HttpClient,
    @Optional()@Inject(BASE_PATH) private basePath: string,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  loadData(){
    this.offerService.apiOffersGet().subscribe(x => {
      this.offers = x;
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteOfferOfferIdPut(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Das Angebot wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }

  createOrder(id: string){
    this.offerService.apiOffersOfferToOrderConfirmationOfferIdPost(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe((x: any) => {
      const id = x.id;
      const number = x.orderConfirmationNumber;
      this.dialogService.open(`Auftragsbestätigung (${number}) erzeugt`, 'Die Auftragsbestätigung wurde erfolgreich erzeugt.' +
        `Die Auftragsbestätigungsnummer lautet ${number}`);
    });
  }

  createPdf(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/Offers/get-as-pdf/${encodeURIComponent(id)}`, config).subscribe(blob => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob);
      a.href = url;
      a.download = `${id}.pdf`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  createWord(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/Offers/get-as-word/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe((blob) => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob as Blob);
      a.href = url;
      a.download = `${id}.docx`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
