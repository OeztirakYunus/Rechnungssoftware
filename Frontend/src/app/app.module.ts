import { NgModule } from '@angular/core';
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

import { HttpClient, HttpClientModule } from '@angular/common/http';
import { SnackbarComponent } from './components/snackbar/snackbar/snackbar.component';
import { AuthService } from './services/auth.service.service';
import { DeliverynotesComponent } from './pages/deliverynotes/deliverynotes/deliverynotes.component';
import { ContactsComponent } from './pages/contacts/contacts/contacts.component';
import { FilesComponent } from './pages/files/files/files.component';
import { InvoicesComponent } from './pages/invoices/invoices/invoices.component';
import { OffersComponent } from './pages/offers/offers/offers.component';
import { OrderconfirmationComponent } from './pages/orderconfirmation/orderconfirmation/orderconfirmation.component';
import { ProductsComponent } from './pages/products/products/products.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDialogRef} from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import {JitCompilerFactory} from '@angular/platform-browser-dynamic';


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
    ProductsComponent
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
    MatPaginatorModule
  ],
  providers: [
    AuthService,
    HttpClient
  ],
  bootstrap: [AppComponent],
  entryComponents: [SnackbarComponent]
})
export class AppModule { }
