import { Component, OnInit } from '@angular/core';
import { Otp } from '../otp';
import { ApiService } from '../api.service';
import { Router, RouterModule, Routes } from '@angular/router';
import { GlobalvariablesService } from '../globalvariables.service';
import { stripGeneratedFileSuffix } from '@angular/compiler/src/aot/util';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.css']
})
export class VerifyComponent implements OnInit {

  constructor(private apiService: ApiService,private router:Router,private gVars:GlobalvariablesService) { }

  enteredOTP="";
  otpObj = new Otp();
  success=false;
  failure=false;
  noOTP=false;
  ngOnInit() {
  }

  checkOTP()
  {
    this.noOTP=false;
    if(this.enteredOTP.length!=6)
    {
      this.noOTP=true;
    }else{
      this.otpObj.addr = this.enteredOTP;
      this.otpObj.cID = this.gVars.getCustomer_ID();
      this.apiService.verifyWithOTP(this.otpObj).subscribe(p=>{
        this.getResult(p);
        debugger
      });
    }
    
  }

  getResult(p:any)
  {
    if(p==true)
    {
      this.success=true;
      this.failure=false;
      this.gVars.setVerified(true);
      sessionStorage.setItem('verified','true')
    }else{
      this.failure=true;
    }
  }

}
