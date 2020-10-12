import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Quotationrequest } from './quotationrequest.model';
import { Quotereqline } from './quotereqline.model';

@Injectable({
  providedIn: 'root'
})
export class QuotationrequestService {
  formData: Quotationrequest;
  reqLine: Quotereqline[];
  

  constructor(private http: HttpClient) { }


  getReqList() {
    return this.http.get(environment.apiURL + '/Quotation_Request').toPromise();
  }

  saveReq() {    
    var body = {
      ...this.formData,
      Quotation_Request_Line: this.reqLine
    };
    return this.http.post(environment.apiURL + '/Quotation_Request', body);
  }

  getRequestList() {
    return this.http.get(environment.apiURL + '/Quotation_Request').toPromise();
  }

  getWebList() {
    return this.http.get(environment.apiURL + '/Web_Link').toPromise();
  }

  getSizeList() {
    return this.http.get(environment.apiURL + '/Product_Size').toPromise();
  }
}