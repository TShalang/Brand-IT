import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonModule }     from '@angular/common';

import { DesignsComponent } from './designs/designs.component';
import { DesignComponent } from './designs/design/design.component';
import { DesignlineComponent } from './designs/designline/designline.component';
import { DesignService } from './shared/design.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import {HttpClientModule} from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { QuotationrequestsComponent } from './quotationrequests/quotationrequests.component';
import { QuotationrequestComponent } from './quotationrequests/quotationrequest/quotationrequest.component';
import { QuotereqlineComponent } from './quotationrequests/quotereqline/quotereqline.component';
import { ProductsComponent } from './products/products.component';
import { TrialComponent } from './trial/trial.component';
import { AccountComponent } from './account/account.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { VerifyComponent } from './verify/verify.component';
import { QuotationrequestService } from './shared/quotationrequest.service';
import { ViewquotationComponent } from './viewquotation/viewquotation.component';
import { WebsitesComponent } from './websites/websites.component';
import { PaypalComponent } from './paypal/paypal.component';
import { EmptyComponent } from './empty/empty.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ConfirmdesignComponent } from './confirmdesign/confirmdesign.component';
import { RequestComponent } from './request/request.component';
import { QuotereqsComponent } from './quotereqs/quotereqs.component';
import { QuotationsComponent } from './quotations/quotations.component';
import { HomepageComponent } from './homepage/homepage.component';
import {MatSnackBarModule} from '@angular/material/snack-bar';
@NgModule({
  declarations: [
    AppComponent,
    DesignsComponent,
    DesignComponent,
    DesignlineComponent,
    QuotationrequestsComponent,
    QuotationrequestComponent,
    QuotereqlineComponent,
    ProductsComponent,
    TrialComponent,
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    ResetpasswordComponent,
    VerifyComponent,
    ViewquotationComponent,
    WebsitesComponent,
    PaypalComponent,
    EmptyComponent,
    NavbarComponent,
    ConfirmdesignComponent,
    RequestComponent,
    QuotereqsComponent,
    QuotationsComponent,
    HomepageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    HttpClientModule,
    CommonModule,
    MatSnackBarModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-full-width',
      preventDuplicates: true,
    }),
  ],

  entryComponents:[QuotereqlineComponent],
  //entryComponents:[DesignlineComponent],
  providers: [QuotationrequestService],
 // providers: [DesignService],
  bootstrap: [AppComponent]
})
export class AppModule { }
