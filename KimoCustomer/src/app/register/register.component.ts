import { Component, OnInit } from '@angular/core';
import { Customer } from '../customer';
import { CustomerAddress } from '../customer-address';
import { ApiService } from '../api.service';
import { exist } from 'src/app/exist';
import { Router, RouterModule, Routes } from '@angular/router';
import { of } from 'rxjs';
import { User } from '../shared/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public fNameVar = "";
  public lNameVar = "";
  public uNameVar = "";
  public eMailVar = "";
  public passwordVar = "";
  public cPasswordVar = "";
  public cNumberVar = "";
  public sNameVar = "";
  public sNumberVar = "";
  public pCodeVar = "";
  public provinceVar = "Gauteng";
  //public uExists : any;


  constructor(private apiService: ApiService,private router:Router) { }

  //Used to send JSON object to api service with new client information
  customerObj = new Customer;
  userObj = new User;
  customerAddressObj = new CustomerAddress;
  exist = new exist;
  isValidFN:boolean;
  isValidLN:boolean;
  isValidUN:boolean;
  isValidEM:boolean;
  isValidP:boolean;
  isValidPC:boolean;
  isValidSN:boolean;
  isValidSNo:boolean;
  isValidCN:boolean;
  isValidPCo:boolean;
  splChars = "*|,\":<>[]{}`\';.()@&$#%0123456789";
  splChars2 = "*|,\":<>[]{}`\';.()@&$#%abcdefghijklmnopqrstuvwxyz";
  uNamChars = "*|,\":<>[]{}`\';.()@&$#%";
  emChars = "*|,\":<>[]{}`\';()&$#%";
  succReg = false;
  invFN = false;
  invLN = false;
  invUN = false;
  invEM = false;
  invPCo = false;
  invSN = false;
  invSNo = false;
  invCN = false;
  invPC = false;
  usrEx = false;
  saveInProgress=false;


  ngOnInit() {
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  valInput(x : string){
    switch(x) {
      case x = 'firstName' : {
        this.isValidFN=true;
        for(var y:number =0 ; y < this.fNameVar.length ; y++ )
        {
          if(this.splChars.indexOf(this.fNameVar[y])!=-1)
          {
            this.isValidFN = false;
          }
        }        
        if(this.isValidFN)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invFN = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invFN = true;
        break;
      }
      case x = 'lastName' : {
        this.isValidLN=true;
        for(var y:number =0 ; y < this.lNameVar.length ; y++ )
        {
          if(this.splChars.indexOf(this.lNameVar[y])!=-1)
          {
            this.isValidLN = false;
          }
        }        
        if(this.isValidLN)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invLN = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invLN = true;
        break;
      }
      case x = 'username' : {
        this.isValidUN=true;
        for(var y:number =0 ; y < this.uNameVar.length ; y++ )
        {
          if(this.uNamChars.indexOf(this.uNameVar[y])!=-1)
          {
            this.isValidUN = false;
          }
        }        
        if(this.isValidUN)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invUN = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invUN = true;
        break;
      }
      case x = 'email' : {
        this.isValidEM=true;
        for(var y:number =0 ; y < this.eMailVar.length ; y++ )
        {
          if(this.emChars.indexOf(this.eMailVar[y])!=-1)
          {
            this.isValidEM = false;
          }
        }        
        if(this.isValidEM)
        {
          if(this.eMailVar.indexOf('@')!=-1)
          {
            document.getElementById(x).classList.add('is-valid');
            document.getElementById(x).classList.remove('is-invalid');
            this.invEM = false;
            break;
          }else{
            document.getElementById(x).classList.remove('is-valid');
            document.getElementById(x).classList.add('is-invalid');
            this.invEM = true;
            break;
          }          
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        break;
      }
      case x = 'password' : {
        if(this.passwordVar==this.cPasswordVar)
        {
          this.isValidP = true;
          this.isValidPC = true;
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          document.getElementById('passConfirm').classList.add('is-valid');
          document.getElementById('passConfirm').classList.remove('is-invalid');
          this.invPCo = false;
          break;
        }else{
          this.isValidP = false;
          this.isValidPC = false;
          //document.getElementById(x).classList.add('is-invalid');
          document.getElementById(x).classList.remove('is-valid');
          document.getElementById('passConfirm').classList.add('is-invalid');
          document.getElementById('passConfirm').classList.remove('is-valid');
          this.invPCo = true;
          break;
        }
      }
      case x = 'passConfirm' : {
        if(this.passwordVar==this.cPasswordVar)
        {
          this.isValidP = true;
          this.isValidPC = true;
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          document.getElementById('password').classList.add('is-valid');
          document.getElementById('password').classList.remove('is-invalid');
          this.invPCo = false;
          break;
        }else{
          this.isValidP = false;
          this.isValidPC = false;
          document.getElementById(x).classList.add('is-invalid');
          document.getElementById(x).classList.remove('is-valid');
          document.getElementById('password').classList.add('is-invalid');
          document.getElementById('password').classList.remove('is-valid');
          this.invPCo = true;
          break;
        }
      }
      case x = 'streetName' : {
        this.isValidSN=true;
        for(var y:number =0 ; y < this.sNameVar.length ; y++ )
        {
          if(this.splChars.indexOf(this.sNameVar[y])!=-1)
          {
            this.isValidSN = false;
          }
        }        
        if(this.isValidSN)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invSN = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invSN = true;
        break;
      }
      case x = 'streetNumber' : {
        this.isValidSNo=true;
        for(var y:number =0 ; y < this.sNumberVar.length ; y++ )
        {
          if(this.splChars2.indexOf(this.sNumberVar[y])!=-1)
          {
            this.isValidSNo = false;
          }
        }        
        if(this.isValidSNo)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invSNo = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invSNo = true;
        break;
      }
      case x = 'contactNumber' : {
        this.isValidCN=true;
        for(var y:number =0 ; y < this.cNumberVar.length ; y++ )
        {
          if(this.splChars2.indexOf(this.cNumberVar[y])!=-1)
          {
            this.isValidCN = false;
          }
        }
        if(this.cNumberVar.length!=10){
          this.isValidCN = false;
        }       
        if(this.isValidCN)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invCN = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invCN = true;
        break;
      }
      case x = 'postalCode' : {
        this.isValidPCo=true;
        for(var y:number =0 ; y < this.pCodeVar.length ; y++ )
        {
          if(this.splChars2.indexOf(this.pCodeVar[y])!=-1)
          {
            this.isValidPCo = false;
          }
        }     
        if(this.isValidPCo)
        {
          document.getElementById(x).classList.add('is-valid');
          document.getElementById(x).classList.remove('is-invalid');
          this.invPC = false;
          break;
        }
        document.getElementById(x).classList.remove('is-valid');
        document.getElementById(x).classList.add('is-invalid');
        this.invPC = true;
        break;
      }
      default: {
        break;
      }
    }
  }

  clearInput(f : string)
  {
    switch(f) { 
      case f = "fNameVar" : {
        this.fNameVar = "";
        break;
      }
      case f = "lNameVar" : {
        this.lNameVar = "";
        break;
      }
      case f = "uNameVar" : {
        this.uNameVar = "";
        break;
      }
      case f = "eMailVar" : {
        this.eMailVar = "";
        break;
      }
      case f = "passwordVar" : {
        this.passwordVar = "";
        break;
      }
      case f = "cPasswordVar" : {
        this.cPasswordVar = "";
        break;
      }
      case f = "cNumberVar" : {
        this.cNumberVar = "";
        break;
      }
      case f = "sNameVar" : {
        this.sNameVar = "";
        break;
      }
      case f = "sNumberVar" : {
        this.sNumberVar = "";
        break;
      }
      case f = "pCodeVar" : {
        this.pCodeVar = "";
        break;
      }
      case f = "provinceVar" : {
        this.provinceVar = "";
        break;
      }
      default : {
        break;
      }
      

    }
    
  }

  async register()
  {     this.saveInProgress=true;
      await this.apiService.registerUser(this.userObj).subscribe(r=> {
        console.log(JSON.stringify(r));
        (JSON.stringify(r));

      })
      await this.apiService.registerCustomerAddress(this.customerAddressObj).subscribe(x=> {
        console.log(JSON.stringify(x));
        (JSON.stringify(x));
      })
      await this.apiService.registerCustomer(this.customerObj).subscribe(z=> {
        console.log(JSON.stringify(z));
        (JSON.stringify(z));
        this.getResult(z);
      })
      this.usrEx = false;
      
    
  }
  getResult(z:any)
  {
    
    if(z==true)
    {this.saveInProgress=false;
      this.router.navigate(['/login']);
    }else{
      return null;}
  }
  //used to send JSON object to api service with all information to register a new client
  async signUp(){
    if(this.isValidFN && this.isValidLN && this.isValidUN && this.isValidEM && this.isValidP && this.isValidPC && this.isValidSN && this.isValidSNo && this.isValidCN && this.isValidPCo)
    {
      this.userObj.UserName = this.uNameVar;
      this.userObj.UserPassword = this.passwordVar;
  
      this.customerAddressObj.Postal_Code = this.pCodeVar;
      this.customerAddressObj.Province = this.provinceVar;
      this.customerAddressObj.StreetName = this.sNameVar;
      this.customerAddressObj.StreetNumber = this.sNumberVar;
  
      this.customerObj.Contact_Number = this.cNumberVar;
      this.customerObj.Email_Address = this.eMailVar;
      this.customerObj.First_Name = this.fNameVar;
      this.customerObj.Last_Name = this.lNameVar;
      //help with customer_ID in Address
      //this.customerObj.ClientTypeID  = 1;
      
      console.log(JSON.stringify(this.userObj));
      console.log(JSON.stringify(this.customerAddressObj));
      console.log(JSON.stringify(this.customerObj));
  
  
      await this.apiService.doesUserExist(this.userObj.UserName).toPromise().then(r => {
        console.log(r);
        if(r=="true")
        {
  
          this.usrEx = true;
        }else{
          this.register();
        }
        
      })
       
    }else{
      console.log('Form input not valid!');
      this.succReg = true;
    }
  }
}

