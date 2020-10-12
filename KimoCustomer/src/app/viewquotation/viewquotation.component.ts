import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-viewquotation',
  templateUrl: './viewquotation.component.html',
  styleUrls: ['./viewquotation.component.css']
})
export class ViewquotationComponent implements OnInit {

  quotations: string[];
  quotationdetail: string[];
  constructor (private httpService: HttpClient) { }
  
  ngOnInit() {
    this.httpService.get('https://localhost:44369/api/Quotation/getQuotations').subscribe (data => {
      this.quotations = data as string [];
    });
}
  requestdetails(id) {
    this.httpService.get('https://localhost:44369/api/Quotation/GetQuotationID/'+id).subscribe (res => {
      this.quotationdetail = res as string [];
    });
}
}
