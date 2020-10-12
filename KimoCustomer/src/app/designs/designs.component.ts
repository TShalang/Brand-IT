import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-designs',
  templateUrl: './designs.component.html',
  styleUrls: ['./designs.component.css']
})
export class DesignsComponent implements OnInit {

  designs: string[];
  designline: string[];
  productType: Object=null;

  constructor(private httpService: HttpClient, private router:Router) { }

  ngOnInit() {
    this.httpService.get('https://localhost:44369/api/Designs/getDesign/'+localStorage["Product_Type_ID"]).subscribe (data => {
      this.designs = data as string [];
  });
}
  requestdesigndetails(id) {
    localStorage["Design_ID"] = id;
    this.httpService.get('https://localhost:44369/api/Design/GetDesignID/'+id).subscribe (res => {
      this.designline = res as string [];
    });
  }

  generate() {
    this.router.navigate(["quotationrequest"]);
  }

  
}
