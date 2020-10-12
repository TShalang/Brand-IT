import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { QuotationrequestService } from '../shared/quotationrequest.service';

@Component({
  selector: 'app-quotationrequests',
  templateUrl: './quotationrequests.component.html',
  styleUrls: ['./quotationrequests.component.css']
})

export class QuotationrequestsComponent implements OnInit {
  reqList;

  constructor(public service: QuotationrequestService) { }

  
  ngOnInit() {
    this.service.getRequestList().then(res => this.reqList = res);
  }

}