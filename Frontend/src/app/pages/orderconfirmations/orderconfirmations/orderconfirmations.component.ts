import {Component, Inject, OnInit, Optional} from '@angular/core';
import {
  BASE_PATH,
  CompaniesService,
  OrderConfirmation,
  OrderConfirmationsService,
  Status
} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {HttpClient} from "@angular/common/http";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-orderconfirmations',
  templateUrl: './orderconfirmation.component.html',
  styleUrls: ['./orderconfirmations.component.css']
})
export class OrderconfirmationsComponent implements OnInit {
  orderConfirmations?: OrderConfirmation[];
  public status: typeof Status = Status;
  searchTerm: string = "";


  constructor(
    private orderConfirmationsService: OrderConfirmationsService,
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private httpClient: HttpClient,
    @Optional()@Inject(BASE_PATH) private basePath: string,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) {
  }

  loadData(){
    this.orderConfirmationsService.apiOrderConfirmationsGet().subscribe(x => {
      this.orderConfirmations = x;
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteOrderConfirmationOrderConfirmationIdPut(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Die Auftragsbestätigung wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }

  createWord(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/orderconfirmations/get-as-word/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob as Blob);
      a.href = url;
      a.download = `${id}.docx`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  createPdf(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/orderconfirmations/get-as-pdf/${encodeURIComponent(id)}`, config).subscribe(blob => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob);
      a.href = url;
      a.download = `${id}.pdf`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  createInvoice(id: string) {
    this.orderConfirmationsService.apiOrderConfirmationsOrderConfirmationToInvoiceOrderConfirmationIdPost(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe((x:any) => {
      const id = x.id;
      const number = x.invoiceNumber;
      this.dialogService.open(`Rechnung (${number}) erzeugt`, 'Die Rechnung wurde erfolgreich erzeugt.' +
        `Die Rechnungsnummer lautet ${number}`);
    });
  }

  createDeliverynote(id: string) {
    this.orderConfirmationsService.apiOrderConfirmationsOrderConfirmationToDeliveryNoteOrderConfirmationIdPost(id).subscribe(x => {
      const id = x.id;
      const number = x.deliveryNoteNumber;
      this.dialogService.open(`Auftragsbestätigung (${number}) erzeugt`, 'Die Auftragsbestätigung wurde erfolgreich erzeugt.' +
        `Die Auftragsbestätigungsnummer lautet ${number}`);
    });
  }
}
