import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { Customer } from './customer';
import { User } from './shared/user';
import { CustomerAddress } from './customer-address';
import { exist } from './exist';
import { Otp } from './otp';
import { VerifyOTP } from './verify-otp';
import { Newpass } from './newpass';
import { map } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    "Access-Control-Allow-Origin": '*'
  })
};

const httpText = {
  headers: new HttpHeaders({
    'Content-Type':  'text/plain',
    "Access-Control-Allow-Origin": '*'
  })
};



@Injectable({
  providedIn: 'root'
})
export class ApiService {
  
  apiURL: string = 'https://localhost:44369/api/'; // Todo add API URL

  constructor(private http : HttpClient) { }


  public doesUserExist(usr:string){
    return this.http.get(this.apiURL+'Customer/doesUserExist/'+usr);
  }

  public registerUser(UserObj: User){
    return this.http.post<User>(this.apiURL+'Customer/registerNewUser',UserObj,httpOptions);
  }

  public registerCustomerAddress(CustomerAddressObj: CustomerAddress){
    return this.http.post<CustomerAddress>(this.apiURL+'Customer/registerNewCustomerAddress',CustomerAddressObj, httpOptions);
  }

  public registerCustomer(CustomerObj: Customer){
    return this.http.post<Customer>(this.apiURL+'Customer/registerNewCustomer', CustomerObj, httpOptions);
  }

  public updateCustomerAddress(id:number, CustomerAddressObj: CustomerAddress){
    return this.http.post(this.apiURL+'Customer/UpdateCustomerAddress/'+id,CustomerAddressObj);
  }




  //resend OTP via email
  public resendOTP(resendInfo:Otp) {
    return this.http.post(this.apiURL+'Customer/ResendOTP',resendInfo, httpOptions);
  }

  //request password reset
  public requestPasswordReset(usrn:string) {
    return this.http.post(this.apiURL+'Customer/RequestPasswordReset/'+usrn,usrn, httpOptions);
  }

  //check enterer OTP for password reset
  public verifyResetOTP(dataX:VerifyOTP) {
    return this.http.post(this.apiURL+'Customer/VerifyResetOTP',dataX, httpOptions);
  }

  //set new pass
  public setNewPass(pss:Newpass) {
    return this.http.post(this.apiURL+'Customer/SetNewPass',pss, httpOptions);
  }

  //verify account with OTP
  public verifyWithOTP(otpObj:Otp) {
    return this.http.post(this.apiURL+'Customer/VerifyWithOTP',otpObj, httpOptions);
  }

 
  CustomerLogin(username, password) {
    console.log(123)
    return this.http.get('https://localhost:44369/api/Customer/CustomerLogin/' + username +'/'+ password).pipe(map(result=>result));
  }

  public getAddressData(cusAddID:number) {
    return this.http.get(this.apiURL+'Customer/getAddressData/'+cusAddID);
  }
  public LoginCustomer(user : User) {
    return this.http.post<User>(this.apiURL+'Customer/CustomerLogin',user, httpOptions);
  }
  //-------------------load products for shop page-----------
  /*public getProducts() {
    return this.http.get(this.apiURL+'Vetter/VProduct/SearchProducts');
  }*/
}


 /* public registerUser(user: User) : Observable<User> {
    return this.http.post<User>("http://localhost:50981/api/User/addUserLogin", user, httpOptions); // Todo add API path
  }*/






