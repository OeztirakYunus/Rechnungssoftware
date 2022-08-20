import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './pages/contacts/contacts/contacts.component';
import { DeliverynotesComponent } from './pages/deliverynotes/deliverynotes/deliverynotes.component';
import { FilesComponent } from './pages/files/files/files.component';
import { HomeComponent } from './pages/home/home.component';
import { InvoicesComponent } from './pages/invoices/invoices/invoices.component';
import { LoginComponent } from './pages/login/login.component';
import { OffersComponent } from './pages/offers/offers/offers.component';
import { OrderconfirmationComponent } from './pages/orderconfirmation/orderconfirmation/orderconfirmation.component';
import { ProductsComponent } from './pages/products/products/products.component';
import { RegisterComponent } from './pages/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'deliverynotes', component: DeliverynotesComponent},
  { path: 'contacts', component: ContactsComponent},
  { path: 'files', component: FilesComponent},
  { path: 'invoices', component: InvoicesComponent},
  { path: 'offers', component: OffersComponent},
  { path: 'orderconfirmation', component: OrderconfirmationComponent},
  { path: 'products', component: ProductsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
