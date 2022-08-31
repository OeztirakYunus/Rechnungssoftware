import {Component, OnInit, ViewChild} from '@angular/core';
import {CompaniesService, OrderConfirmationsService, Product, ProductsService} from "../../../../../client";
import {FormBuilder} from "@angular/forms";
import {DialogService} from "../../../services/dialog.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ProductComponent} from "../../../forms/product/product.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  product: Product | undefined = undefined;
  @ViewChild('productElement') private productElement: ProductComponent | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private orderConfirmationsService: OrderConfirmationsService,
    private companiesService: CompaniesService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productsService: ProductsService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      const id = x['id'];
      this.productsService.apiProductsIdGet(id).subscribe(y => {
        this.product = y;
      });
    })
  }


  update() {
    this.productElement?.markAllAsTouched();
    if(this.productElement?.isValid()){
      this.productsService.apiProductsPut(this.product).pipe(this.commonHttpErrorHandling.catchError()).subscribe(_ => {
        this.dialogService.open('Erfolgreich aktualisiert', 'Das Produkt wurde erfolgreich aktualisiert!',
          () => {
            this.router.navigate(['/products']);
          });
      })
    }else{
      this.dialogService.open('Fehlgeschlagen', 'Überprüfen Sie bitte Ihre Eingaben!');
    }
  }
}
