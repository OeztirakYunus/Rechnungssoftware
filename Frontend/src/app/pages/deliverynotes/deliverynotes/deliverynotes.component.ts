import {Component, Inject, OnInit, Optional} from '@angular/core';
import {BASE_PATH, CompaniesService, DeliveryNote, DeliveryNotesService, Status} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-deliverynotes',
  templateUrl: './deliverynotes.component.html',
  styleUrls: ['./deliverynotes.component.css']
})


export class DeliverynotesComponent implements OnInit {
  deliveryNotes: DeliveryNote[] = [];
  searchTerm: string = '';
  public status: typeof Status = Status;

  constructor(
    private deliveryNotesService: DeliveryNotesService,
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private httpClient: HttpClient,
    @Optional()@Inject(BASE_PATH) private basePath: string,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  loadData(){
    this.deliveryNotesService.apiDeliveryNotesGet().pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.deliveryNotes = x;
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteDeliveryNoteDeliveryNoteIdPut(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Der Lieferschein wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }


  createPdf(id: string) {
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`${this.basePath}/api/DeliveryNotes/get-as-pdf/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
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
    this.httpClient.get(`${this.basePath}/api/DeliveryNotes/get-as-word/${encodeURIComponent(id)}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
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

