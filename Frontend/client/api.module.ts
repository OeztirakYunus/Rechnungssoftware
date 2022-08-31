import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { AddressesService } from './api/addresses.service';
import { AuthService } from './api/auth.service';
import { BankInformationsService } from './api/bankInformations.service';
import { CompaniesService } from './api/companies.service';
import { ContactsService } from './api/contacts.service';
import { DeliveryNotesService } from './api/deliveryNotes.service';
import { DocumentInformationsService } from './api/documentInformations.service';
import { FilesService } from './api/files.service';
import { InvoicesService } from './api/invoices.service';
import { OffersService } from './api/offers.service';
import { OrderConfirmationsService } from './api/orderConfirmations.service';
import { PositionsService } from './api/positions.service';
import { ProductsService } from './api/products.service';
import { UsersService } from './api/users.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AddressesService,
    AuthService,
    BankInformationsService,
    CompaniesService,
    ContactsService,
    DeliveryNotesService,
    DocumentInformationsService,
    FilesService,
    InvoicesService,
    OffersService,
    OrderConfirmationsService,
    PositionsService,
    ProductsService,
    UsersService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
