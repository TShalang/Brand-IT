import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { GlobalvariablesService } from '../globalvariables.service';
import { Router, RouterModule, Routes } from '@angular/router';
import { AppComponent } from '../app.component';
import { User } from '../shared/user';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public logUsernameVar = "";
  public logPasswordVar = "";

  logUsername : string;
  logPassword : string;

  constructor(private apiService: ApiService, private gVars:GlobalvariablesService, private router:Router, private appC:AppComponent,
              private _snackbar: MatSnackBar) { }

  userObj = new User;
  succLogin = false;
  emptyUSR=false;
  emptyPSR=false;
  invPass = false;
  ee:boolean;
  loggednIn : Object=null;


  ngOnInit() {
  }

  setAddressData(data){
    console.log(data);
    this.gVars.setStreetName(data.StreetName);
    this.gVars.setStreetNumber(data.StreetNumber);
    this.gVars.setPostalCode(data.PostalCode);
    this.gVars.setProvince(data.Province);
    sessionStorage.setItem('streetName',this.gVars.getStreetName());
    sessionStorage.setItem('streetNumber',this.gVars.getStreetNumber());
    sessionStorage.setItem('postalCode',this.gVars.getPostalCode());
    sessionStorage.setItem('province',this.gVars.getProvince());
  }

  getUserData(data) { 
    if(data.Message=="Invalid User!")
    {
      this.succLogin = true;
    }else if(data.Message=="Invalid Password!"){
      this.invPass = true;
    }else{ //customer

      this.gVars.setAddress_ID(data.Address_ID);
      this.gVars.setContact_Number(data.Contact_Number);
      this.gVars.setEmail_Address(data.Email_Address);
      this.gVars.setCustomer_ID(data.Customer_ID);
      this.gVars.setFirst_Name(data.First_Name);
      this.gVars.setLast_Name(data.Last_Name);
      this.gVars.setUserID(data.UserID);
      this.gVars.setVerified(data.verified);

      //customer
      sessionStorage.setItem('Address_ID',this.gVars.getAddress_ID().toString());
      sessionStorage.setItem('Contact_Number',this.gVars.getContact_Number());
      sessionStorage.setItem('Email_Address',this.gVars.getEmail_Address());
      sessionStorage.setItem('customer_ID',this.gVars.getCustomer_ID().toString());
      sessionStorage.setItem('First_Name',this.gVars.getFirst_Name());
      sessionStorage.setItem('Last_Name',this.gVars.getLast_Name());
      sessionStorage.setItem('userID',this.gVars.getUserID().toString());


      if(this.gVars.getVerified()==true)
      {
        sessionStorage.setItem('verified','true');
      }else{
        sessionStorage.setItem('verified','false');
      }
      

    

      //set logged in status
      this.gVars.setLoggedInStatus(true);
      sessionStorage.setItem('loggedInStatus',this.gVars.getLoggedInStatus().toString());
      this.succLogin = false;
      this.emptyPSR = false;
      this.emptyUSR = false;
      this.invPass = false;
      //get customer address data
   
      this.router.navigate(['/producttype']);
      //set global vars to session vars
      this.gVars.setAddress_ID(+sessionStorage.getItem('customerAddressID'));
      this.gVars.setContact_Number(sessionStorage.getItem('customerNumber'));
      this.gVars.setEmail_Address(sessionStorage.getItem('customerEmail'));
      this.gVars.setCustomer_ID(+sessionStorage.getItem('customerID'));
      this.gVars.setFirst_Name(sessionStorage.getItem('customerName'));
      this.gVars.setLast_Name(sessionStorage.getItem('customerLastname'));
      this.gVars.setUserID(+sessionStorage.getItem('userID'));
      this.gVars.setStreetName(sessionStorage.getItem('streetName'));
      this.gVars.setStreetNumber(sessionStorage.getItem('streetNumber'));
      this.gVars.setPostalCode(sessionStorage.getItem('postalCode'));
      this.gVars.setProvince(sessionStorage.getItem('province'));
      this.ee= (/true/i).test(sessionStorage.getItem('loggedInStatus'));
      this.gVars.setLoggedInStatus(this.ee);
    }
  }

  customerLogin()
  {
   
    this.userObj.UserName = this.logUsernameVar;
    this.userObj.UserPassword = this.logPasswordVar;
    console.log(JSON.stringify(this.userObj));
    
    console.log(JSON.stringify(this.userObj))
    this.apiService.LoginCustomer(this.userObj).subscribe((response: any)=> {
      if(response.Message){
        this.succLogin=false;
        this.emptyPSR=false;
        this.emptyUSR=false;
        if(this.logPasswordVar=="" && this.logUsernameVar=="")
        {
          this.emptyPSR=true;
          this.emptyUSR=true;
          return;
        }
        if(this.logUsernameVar=="")
        {
          this.emptyUSR=true;
          return;
        }
        if(this.logPasswordVar=="")
        {
          this.emptyPSR=true;
          return;
        }
    else
        this._snackbar.open('An incorrect username and/or password has been entered. Please try again.', 'OK', {
          duration: 3500,
        });
      }
      else{
        this.loggednIn = response;
        this.router.navigate(['/homepage']); // Change this to home once it has been integrated
        localStorage.setItem('loggedIn', 'loggedIn')
      }
      localStorage["Customer_ID"] = response["Customer_ID"];
    })  

  }
  clearInput(f : string)
  {
    switch(f) { 
      case f = "logUsernameVar" : {
        this.logUsernameVar = "";
        break;
      }
      case f = "logPasswordVar" : {
        this.logPasswordVar = "";
        break;
      }
      default : {
        break;
      }
    }
    
  }
}
