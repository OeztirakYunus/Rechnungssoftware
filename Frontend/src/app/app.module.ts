import {LOCALE_ID, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatIconModule} from '@angular/material/icon';
import {FormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';

import {MatNativeDateModule} from '@angular/material/core';
import {MatRadioModule} from '@angular/material/radio';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatStepperModule} from '@angular/material/stepper';

import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import { SnackbarComponent } from './components/snackbar/snackbar/snackbar.component';
import { AuthService } from './services/auth.service.service';
import { DeliverynotesComponent } from './pages/deliverynotes/deliverynotes/deliverynotes.component';
import { ContactsComponent } from './pages/contacts/contacts/contacts.component';
import { FilesComponent } from './pages/files/files/files.component';
import { InvoicesComponent } from './pages/invoices/invoices/invoices.component';
import { OffersComponent } from './pages/offers/offers/offers.component';
import { ProductsComponent } from './pages/products/products/products.component';
import {MatDialogModule} from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import {ApiModule, BASE_PATH} from "../../client";
import {AuthInterceptorInterceptor} from "./auth-interceptor.interceptor";
import {environment} from "../environments/environment";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductCreateComponent } from './pages/products/product-create/product-create.component';
import { NgbdModalContentComponent } from './ngbd-modal-content/ngbd-modal-content.component';
import { MyDecimalPipe } from './my-decimal.pipe';
import { InvoiceCreateComponent } from './pages/invoices/invoice-create/invoice-create.component';
import { PositionComponent } from './forms/position/position.component';
import { DateComponent } from './forms/date/date.component';
import { FilterPipe } from './filter.pipe';
import { OfferCreateComponent } from './pages/offers/offer-create/offer-create.component';
import { DocumentInformationComponent } from './forms/document-information/document-information.component';
import { AddressComponent } from './forms/address/address.component';
import { ContactCreateComponent } from './pages/contacts/contact-create/contact-create.component';
import { ContactEditComponent } from './pages/contacts/contact-edit/contact-edit.component';
import { UsersComponent } from './pages/users/users/users.component';
import { InvoiceEditComponent } from './pages/invoices/invoice-edit/invoice-edit.component';
import { OfferEditComponent } from './pages/offers/offer-edit/offer-edit.component';
import {ProductEditComponent} from "./pages/products/product-edit/product-edit.component";
import { ProductComponent } from './forms/product/product.component';
import { DeliverynoteEditComponent } from './pages/deliverynotes/deliverynote-edit/deliverynote-edit.component';
import { DeliverynoteCreateComponent } from './pages/deliverynotes/deliverynote-create/deliverynote-create.component';
import { ContactComponent } from './forms/contact/contact.component';
import { OfferComponent } from './forms/offer/offer.component';
import { InvoiceComponent } from './forms/invoice/invoice.component';
import {OrderconfirmationsComponent} from "./pages/orderconfirmations/orderconfirmations/orderconfirmations.component";
import {OrderconfirmationCreateComponent} from "./pages/orderconfirmations/orderconfirmation-create/orderconfirmation-create.component";
import {OrderconfirmationComponent} from "./forms/orderconfirmation/orderconfirmation.component";
import {OrderconfirmationEditComponent} from "./pages/orderconfirmations/orderconfirmation-edit/orderconfirmation-edit.component";
import { DeliverynoteComponent } from './forms/deliverynote/deliverynote.component';
import { UserComponent } from './forms/user/user.component';
import { UserCreateComponent } from './pages/users/user-create/user-create.component';
import {HttpErrorResponseInterceptorInterceptor} from "./http-error-response-interceptor.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    SnackbarComponent,
    DeliverynotesComponent,
    ContactsComponent,
    FilesComponent,
    InvoicesComponent,
    OffersComponent,
    OrderconfirmationComponent,
    ProductsComponent,
    ProductCreateComponent,
    NgbdModalContentComponent,
    MyDecimalPipe,
    InvoiceCreateComponent,
    PositionComponent,
    DateComponent,
    FilterPipe,
    OrderconfirmationCreateComponent,
    OfferCreateComponent,
    DocumentInformationComponent,
    AddressComponent,
    ContactCreateComponent,
    ContactEditComponent,
    UsersComponent,
    InvoiceEditComponent,
    ProductEditComponent,
    OfferEditComponent,
    ProductComponent,
    OrderconfirmationEditComponent,
    DeliverynoteEditComponent,
    DeliverynoteCreateComponent,
    ContactComponent,
    OfferComponent,
    InvoiceComponent,
    OrderconfirmationsComponent,
    DeliverynoteComponent,
    UserComponent,
    UserCreateComponent
  ],
  imports: [
    MatNativeDateModule,
    MatRadioModule,
    MatGridListModule,
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatMenuModule,
    MatTableModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatOptionModule,
    FlexLayoutModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatSnackBarModule,
    MatStepperModule,
    MatDialogModule,
    MatPaginatorModule,
    ApiModule,
    NgbModule
  ],
  providers: [
    AuthService,
    HttpClient,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorResponseInterceptorInterceptor,
      multi: true
    },
    { provide: BASE_PATH, useValue: environment.apiUrl }
  ],
  bootstrap: [AppComponent],
  entryComponents: [SnackbarComponent]
})
export class AppModule { }
