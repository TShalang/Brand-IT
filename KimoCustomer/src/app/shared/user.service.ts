import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
 

  constructor(private http: HttpClient) { }

  RegisterUser(user: User){
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post(environment.apiURL + 'User/createUser', user, httpOptions);
  }
  // getDeliveries(session : any) {
  //   const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  //   return this.http.post(environment.ApiURL + 'User/getDeliveries', session, httpOptions);
  // }

  // getOrderItems(session : any) {
  //   const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  //   return this.http.post(environment.ApiURL + 'User/getOrderItems', session, httpOptions);
  // }

  // getDeliveriesAdmin(session : any) {
  //   const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  //   return this.http.post(environment.ApiURL + 'User/getDeliveriesAdmin', session, httpOptions);
  // }

  getUserDetails(session : any) {
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post(environment.apiURL + 'User/getUserDetails', session, httpOptions);
  }

  Login(user : User){
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post(environment.apiURL + 'User/Login', user, httpOptions)
  }
}
