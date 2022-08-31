import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, DeliveryNote, DeliveryNoteDto, DeliveryNotesService} from "../../../../../client";
import {ActivatedRoute, Router} from "@angular/router";
import {DeliverynoteComponent} from "../../../forms/deliverynote/deliverynote.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-deliverynote-edit',
  templateUrl: './deliverynote-edit.component.html',
  styleUrls: ['./deliverynote-edit.component.css']
})
export class DeliverynoteEditComponent implements OnInit {
  @ViewChild('deliverynoteElement') private deliverynoteElement: DeliverynoteComponent | undefined = undefined;
  deliveryNote: DeliveryNote | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private deliveryNotesService: DeliveryNotesService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.deliveryNotesService.apiDeliveryNotesIdGet(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(y => {
        this.deliveryNote = y;
      });
    });
  }

  update() {
    this.deliverynoteElement?.markAllAsTouched();

    if(this.deliverynoteElement?.isValid()){
      this.companiesService.apiCompaniesAddDeliveryNotePut(this.deliveryNote).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich angelegt', 'Der Lieferschein wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/deliverynotes']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
