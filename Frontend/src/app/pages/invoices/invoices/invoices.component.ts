import {Component, Inject, OnInit, Optional} from '@angular/core';
import {BASE_PATH, CompaniesService, Invoice, InvoicesService, Status} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.css']
})
export class InvoicesComponent implements OnInit {
  invoices: Invoice[] = [];
  searchTerm: string = '';
  private basePath: string;
  public status: typeof Status = Status;

  constructor(
    private invoicesService: InvoicesService,
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private router: Router,
    private httpClient: HttpClient,
    @Optional()@Inject(BASE_PATH) basePath: string,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) {
    this.basePath = basePath;
  }

  loadData(){
    this.invoicesService.apiInvoicesGet().subscribe(x => {
      this.invoices = x;
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteInvoiceInvoiceIdPut(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Die Rechnung wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }

  createDeliverynote(id: string){
    this.invoicesService.apiInvoicesInvoiceToDeliveryNoteInvoiceIdPost(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      const id = x.id;
      const number = x.deliveryNoteNumber;
      this.dialogService.open(`Lieferschein (${number}) erzeugt`, 'Der Lieferschein wurde erfolgreich erzeugt.' +
        `Die Lieferscheinnummer lautet ${number}`);
    });
  }

  createPdf(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/Invoices/get-as-pdf/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
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
    this.httpClient.get(`${this.basePath}/api/Invoices/get-as-word/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob);
      a.href = url;
      a.download = `${id}.docx`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
