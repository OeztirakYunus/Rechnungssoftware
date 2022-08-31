import {Component, Inject, OnInit} from '@angular/core';
import {CompaniesService, Product, ProductCategory, ProductsService} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products?: Product[];
  public productCategory: typeof ProductCategory = ProductCategory;
  searchTerm: string = "";


  constructor(
    private productsService: ProductsService,
    private companiesService: CompaniesService,
    private dialogService: DialogService,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) {
  }

  loadData(){
    this.productsService.apiProductsGet().pipe(this.commonHttpErrorHandling.catchError()).subscribe(x => {
      this.products = x as Product[];
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

  delete(id: string) {
    this.companiesService.apiCompaniesDeleteProductProductIdPut(id).subscribe(x => {
      this.dialogService.open('Erfolgreich gelöscht', `Das Produkt wurde erfolgreich gelöscht`, () => this.loadData());
    });
  }
}
