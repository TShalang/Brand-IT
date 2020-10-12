import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { ConfirmdesignComponent } from './confirmdesign/confirmdesign.component';
import { DesignComponent } from './designs/design/design.component';
import { DesignsComponent } from './designs/designs.component';
import { EmptyComponent } from './empty/empty.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { PaypalComponent } from './paypal/paypal.component';
import { ProductsComponent } from './products/products.component';
import { QuotationrequestComponent } from './quotationrequests/quotationrequest/quotationrequest.component';
import { QuotationrequestsComponent } from './quotationrequests/quotationrequests.component';
import { RegisterComponent } from './register/register.component';
import { RequestComponent } from './request/request.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { TrialComponent } from './trial/trial.component';
import { VerifyComponent } from './verify/verify.component';
import { ViewquotationComponent } from './viewquotation/viewquotation.component';
import { WebsitesComponent } from './websites/websites.component';


const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'designs', component: DesignsComponent},
  {path: 'design', children: [
    {path: '', component: DesignComponent},
    {path: 'edit/:id', component: DesignComponent},
  
  ]},

  {path: 'quotationrequests', component: QuotationrequestsComponent},
  {path: 'quotationrequest', children: [
    {path: '', component: QuotationrequestComponent},
    {path: 'edit/:id', component: DesignComponent},
  ]},
  {path: 'products', component: ProductsComponent},
  {path: 'design/:id', component: DesignComponent},
  {path: 'trial/id', component: TrialComponent},

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'verify', component: VerifyComponent },
  { path: 'login', component: LoginComponent },
  {path: 'account', component: AccountComponent},
  {path: 'resetpassword', component: ResetpasswordComponent},
  {path: 'designs', component: DesignsComponent},
  {path: 'viewquotation', component: ViewquotationComponent},
  {path: 'websites', component: WebsitesComponent},
  {path: 'paypal', component: PaypalComponent},
  {path: 'empty', component: EmptyComponent},
  {path: 'confirmdesign', component: ConfirmdesignComponent},
  {path: 'request', component: RequestComponent},
  {path: 'homepage', component: HomepageComponent}



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
