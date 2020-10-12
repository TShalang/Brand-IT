import { Injectable } from '@angular/core';
import { AppComponent } from './app.component';

@Injectable({
  providedIn: 'root'
})
export class GlobalvariablesService {
  // TODO: set the default to 0 when login is implemented
  public Customer_ID = 0;
  public User_ID = 0;
  public Address_ID =0;
  public First_Name ="";
  public Last_Name ="";
  public Contact_Number="";
  public Email_Address="";
  public isLoggedIn = false;
  public verified = false;
  public streetName = "";
  public streetNumber ="";
  public postalCode = "";
  public province = "";



  public remID=0;
  public heading="";
  
  constructor() { }

  //logout function
  public logout() {
    this.Customer_ID = 0;
    this.User_ID = 0;
    this.Address_ID =0;
    this.First_Name ="";
    this.Last_Name ="";
    this.Contact_Number="";
    this.Email_Address="";
    this.isLoggedIn=false;

    sessionStorage.clear();
    //location.href=location.href;
  }

 
  //CustomerID
  public getCustomer_ID(): number{
    return this.Customer_ID;
  }
  public setCustomer_ID( newV:number){
    this.Customer_ID = newV;
  }
  //Verified
  public getVerified(){
    return this.verified;
  }
  public setVerified(status:boolean){
    this.verified=status;
  }
  //UserID
  public getUserID(): number{
    return this.User_ID;
  }
  public setUserID( newV:number){
    this.User_ID = newV;
  }

  //CustomerAddressID
  public getAddress_ID(): number{
    return this.Address_ID;
  }
  public setAddress_ID( newV:number){
    this.Address_ID = newV;
  }
  //CustomerName
  public getFirst_Name(): string{
    return this.First_Name;
  }
  public setFirst_Name( newV:string){
    this.First_Name = newV;
  }
  //CustomerSurname
  public getLast_Name(): string{
    return this.Last_Name;
  }
  public setLast_Name( newV:string){
    this.Last_Name = newV;
  }
  //CustomerCell
  public getContact_Number(): string{
    return this.Contact_Number;
  }
  public setContact_Number( newV:string){
    this.Contact_Number = newV;
  }
  //Customer  Email
  public getEmail_Address(): string{
    return this.Email_Address;
  }
  public setEmail_Address( newV:string){
    this.Email_Address = newV;
  }
  //isLoggedIn
  public getLoggedInStatus(): boolean{
    return this.isLoggedIn;
  }
  public setLoggedInStatus( newV:boolean){
    this.isLoggedIn = newV;
  }

    //streetName
    public getStreetName(): string{
      return this.streetName;
    }
    public setStreetName(newV:string){
      this.streetName=newV;
    }
    //streetNumber
    public getStreetNumber(): string{
      return this.streetNumber;
    }
    public setStreetNumber(newV:string){
      this.streetNumber=newV;
    }
    //postalCode
    public getPostalCode(): string{
      return this.postalCode;
    }
    public setPostalCode(newV:string){
      this.postalCode=newV;
    }
    //province
    public getProvince(): string{
      return this.province;
    }
    public setProvince(newV:string){
      this.province=newV;
    }


}
