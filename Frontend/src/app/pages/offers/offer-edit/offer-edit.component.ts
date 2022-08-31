import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {DialogService} from "../../../services/dialog.service";
import {CompaniesService, Offer, OfferDto, OffersService} from "../../../../../client";
import {ActivatedRoute, Router} from "@angular/router";
import {OfferComponent} from "../../../forms/offer/offer.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-offer-edit',
  templateUrl: './offer-edit.component.html',
  styleUrls: ['./offer-edit.component.css']
})
export class OfferEditComponent implements OnInit {
  @ViewChild('offerElement') private offerElement: OfferComponent | undefined = undefined;
  offer: Offer | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private offersService: OffersService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  loadData(){
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.offersService.apiOffersIdGet(id).pipe(this.commonHttpErrorHandling.catchError()).subscribe(y => {
        this.offer =  y as Offer;
      })
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  updateOffer() {
    this.offerElement?.markAllAsTouched();

    if(this.offerElement?.isValid()){
      this.offersService.apiOffersPut(this.offer).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich aktualisiert', 'Das Angebot wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/offers']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
