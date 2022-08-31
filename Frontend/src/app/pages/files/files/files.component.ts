import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {BASE_PATH, BSFile, FilesService} from "../../../../../client";
import {HttpClient, HttpContext, HttpHeaders, HttpParams} from "@angular/common/http";
import {DialogService} from "../../../services/dialog.service";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {
  public files: BSFile[] = [];
  @ViewChild('uploadForm') uploadForm: any | undefined;
  private selectedFiles?: FileList;
  searchTerm: string = '';
  constructor(private filesService: FilesService,
              private httpClient: HttpClient,
              private dialogService: DialogService,
              @Inject(BASE_PATH) public basePath: string,
              private commonHttpErrorHandling: CommonHttpErrorHandlingService) { }

  ngOnInit(): void {
    this.loadData();
  }

  download(file: BSFile){
    const config = { responseType: 'blob' as 'blob'};
    this.httpClient.get(`https://backend.invoicer.at/api/Files/byId/${file.id}`, config).pipe(this.commonHttpErrorHandling.catchError()).subscribe(blob => {
      const a = document.createElement('a');
      document.body.appendChild(a);
      const url = window.URL.createObjectURL(blob);
      a.href = url;
      a.download = file.fileName;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  upload() {
    if(this.selectedFiles){
      const file: File | null = this.selectedFiles.item(0);
      if(file){
        this.filesService.apiFilesPostForm(file, 'response', true).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
          this.loadData();
          this.uploadForm.nativeElement.reset();
          this.dialogService.open("Erfolgreich hochgeladen","Die Datei wurde erfolgreich hochgeladen.");
        }, x => {
          if(400 == x.status){
            this.dialogService.open("Datei mit diesen Dateinamen vorhanden","Eine Datei mit dem selben Dateinamen ist bereits auf dem Server.");
          }
        })
      }
    }
  }

  selectFile(event: any) {
    this.selectedFiles = event.target.files;
  }

  private loadData() {
    this.filesService.apiFilesGet().pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.files = x;
    });
  }

  delete(id: string) {
    this.filesService.apiFilesIdDelete(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Die Datei wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }
}
