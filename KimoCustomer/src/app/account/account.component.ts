import { Component, OnInit, NgModule, } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { GlobalvariablesService } from '../globalvariables.service';
import { Otp } from '../otp';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})

export class AccountComponent implements OnInit {

  constructor( private router:Router, private gVars:GlobalvariablesService,  private apiService : ApiService) { }
  
  notVerified=true;
  resent=false;
  otpRequest= new Otp();
  failed=false;
  saveInProgress=false;

  ngOnInit() {
    // check whether customer is logged in. if so, nothing, otherwise redirect to login screen
    //if (this.gVars.getCustomerID() == 0){
      //this.router.navigate(['/login']);
    //}
    //if(this.gVars.getVerified()==false)
    //{
      //this.notVerified=true;
    //}else{
      //this.notVerified=false;
    // }
  }

  resend()
  {
    this.saveInProgress=true;
    document.getElementById("overlay").style.display = "block";

    this.failed=false;
    this.otpRequest.addr = this.gVars.getEmail_Address();
    this.otpRequest.cID = this.gVars.getCustomer_ID();
    this.apiService.resendOTP(this.otpRequest).subscribe(x=>{
      this.setSuccess(x);
    });
    //this.resent=true;
  }

  setSuccess(x:any)
  {
    this.saveInProgress=false;
    document.getElementById("overlay").style.display = "none";
    
    if(x==true)
    {
      this.resent=true;
    }else{
      this.failed=true;
    }
  }


  /*how to navigate in typescript. Remember to include router and declare in conscructor
  bind (click)="dothis()" to buttom to call function
  dothis(){
    if(true)
    {
      this.router.navigate(['/about']);
    }
  }*/

}
