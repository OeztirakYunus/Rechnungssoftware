import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './pages/contacts/contacts/contacts.component';
import { DeliverynotesComponent } from './pages/deliverynotes/deliverynotes/deliverynotes.component';
import { FilesComponent } from './pages/files/files/files.component';
import { HomeComponent } from './pages/home/home.component';
import { InvoicesComponent } from './pages/invoices/invoices/invoices.component';
import { LoginComponent } from './pages/login/login.component';
import { OffersComponent } from './pages/offers/offers/offers.component';
import { ProductsComponent } from './pages/products/products/products.component';
import { RegisterComponent } from './pages/register/register.component';
import {ProductCreateComponent} from "./pages/products/product-create/product-create.component";
import {InvoiceCreateComponent} from "./pages/invoices/invoice-create/invoice-create.component";
import {OfferCreateComponent} from "./pages/offers/offer-create/offer-create.component";
import {ContactCreateComponent} from "./pages/contacts/contact-create/contact-create.component";
import {ContactEditComponent} from "./pages/contacts/contact-edit/contact-edit.component";
import {UsersComponent} from "./pages/users/users/users.component";
import {InvoiceEditComponent} from "./pages/invoices/invoice-edit/invoice-edit.component";
import {ProductEditComponent} from "./pages/products/product-edit/product-edit.component";
import {OfferEditComponent} from "./pages/offers/offer-edit/offer-edit.component";
import {DeliverynoteEditComponent} from "./pages/deliverynotes/deliverynote-edit/deliverynote-edit.component";
import {DeliverynoteCreateComponent} from "./pages/deliverynotes/deliverynote-create/deliverynote-create.component";
import {OrderconfirmationCreateComponent} from "./pages/orderconfirmations/orderconfirmation-create/orderconfirmation-create.component";
import {OrderconfirmationEditComponent} from "./pages/orderconfirmations/orderconfirmation-edit/orderconfirmation-edit.component";
import {OrderconfirmationsComponent} from "./pages/orderconfirmations/orderconfirmations/orderconfirmations.component";
import {UserCreateComponent} from "./pages/users/user-create/user-create.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'deliverynotes', component: DeliverynotesComponent},
  { path: 'deliverynotes/edit/:id', component: DeliverynoteEditComponent},
  { path: 'deliverynotes/create', component: DeliverynoteCreateComponent},
  { path: 'contacts', component: ContactsComponent},
  { path: 'contacts/create', component: ContactCreateComponent},
  { path: 'contacts/edit/:id', component: ContactEditComponent},
  { path: 'files', component: FilesComponent},
  { path: 'invoices', component: InvoicesComponent},
  { path: 'invoices/edit/:id', component: InvoiceEditComponent},
  { path: 'invoices/create', component: InvoiceCreateComponent},
  { path: 'offers', component: OffersComponent},
  { path: 'offers/create', component: OfferCreateComponent},
  { path: 'offers/edit/:id', component: OfferEditComponent},
  { path: 'orderconfirmation', component: OrderconfirmationsComponent},
  { path: 'orderconfirmation/create', component: OrderconfirmationCreateComponent},
  { path: 'orderconfirmation/edit/:id', component: OrderconfirmationEditComponent},
  { path: 'products', component: ProductsComponent},
  { path: 'products/create', component: ProductCreateComponent},
  { path: 'products/edit/:id', component: ProductEditComponent},
  { path: 'users', component: UsersComponent},
  { path: 'users/create', component: UserCreateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
