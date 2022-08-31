import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CompaniesService, ProductDto, Unit} from "../../../../../client";
import {DialogService} from "../../../services/dialog.service";
import {Router} from "@angular/router";
import {ProductComponent} from "../../../forms/product/product.component";
import {CommonHttpErrorHandlingService} from "../../../common-http-error-handling.service";

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {
  productForm: FormGroup = this.fb.group({
    product: ['', []]
  });
  public productUnit: typeof Unit = Unit;
  @ViewChild('productElement') private productElement: ProductComponent | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private companiesService: CompaniesService,
    private router: Router,
    private commonHttpErrorHandling: CommonHttpErrorHandlingService
  ) { }

  ngOnInit(): void {
  }


  create() {
    this.productElement?.markAllAsTouched();
    const productDto: ProductDto = this.productForm?.get('product')?.value;

    if(this.productElement?.isValid()){
      this.companiesService.apiCompaniesAddProductPut(productDto).pipe(this.commonHttpErrorHandling.catchError()).subscribe(() => {
        this.dialogService.open('Erfolgreich durchgeführt','Das Produkt wurde erfolgreich erstellt', () => {
          this.router.navigate(['/products']);
        });
      });
    }else{
      this.dialogService.open('Eingabe überprüfen','Überprüfen Sie bitte Ihre Eingaben');
    }
  }
}
