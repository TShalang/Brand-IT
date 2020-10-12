import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { Prodtypes } from '../products/prodtype.model';
import { Designs } from '../products/prodtype.model';
import { DesignImage } from '../products/prodtype.model';
import { DesignLine } from '../products/prodtype.model';

import { DesignService } from '../shared/design.service';
import { Router } from '@angular/router';
import { CATCH_STACK_VAR } from '@angular/compiler/src/output/abstract_emitter';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/output_ast';
import { Weblinks } from '../weblinks.model';
import { Prodsize } from '../prodsize.model';
import { Quote } from '@angular/compiler';
import { Quotereqline } from '../shared/quotereqline.model';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {


  types: Prodtypes[];
  designs: DesignImage[];
  designline: Designs[];
  lines : DesignLine[];
  designType : any;
  request = [];
  Pcolor : string;
  qV : string;
  Pcode : string;
  quantity : string;

  formData: Quotereqline;

  webLinks : Weblinks[];
  sizes : Prodsize[];

  constructor(public service : DesignService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.service.getTypes().then(res => this.types = res as Prodtypes[]);
    this.service.getWebLinks().then(res => this.webLinks = res as Weblinks[]);
    this.service.getSizes().then(res => this.sizes = res as Prodsize[]);
    
  }
  
  store(id, name){
    this.service.getProductTypeImage(name).then(resp => this.designs = resp as DesignImage[]);
    this.designType = this.service.getDesign(localStorage["Product_Type_ID"]).subscribe(designType => this.designType = designType );
    console.log(this.designType)
    this.service.getProductTypes(id,localStorage["Customer_ID"]).then(respo => this.designline = respo as Designs[]);
    localStorage["Product_Type_ID"] = id;
  }

  storeDesign(id){
    this.service.getDesignLines(id).then(response => this.lines = response as DesignLine[]);
    localStorage["Design_ID"] = id;
  }

  addToRequest(color,website,productCode,quantity){
    //just for now
    localStorage["Size_ID"] = 1;
    localStorage["Website_ID"] = 1;
    var req = {
      Product_Type_ID: localStorage["Product_Type_ID"],
      Design_ID: localStorage["Design_ID"],
      Size: localStorage["Size_ID"],
      Color: color,
      Website: website,
      ProductCode: productCode,
      Quantity: quantity
    };
    this.request.push(req);
  }

  delete(i){
    this.request.splice(i,1);
  }

  addRequest(){
    
      
    console.log(this.request.length)
    for(var i=0;i<this.request.length;i++)
    {
      this.service.createRequest(
        localStorage["Customer_ID"],
        this.request[i].Design_ID,
        this.request[i].Size,
        this.request[i].Color,
        this.request[i].Website,
        this.request[i].ProductCode,
        this.request[i].Quantity,
        );
    }
    this.router.navigate(["designs"]);
}

  navigate()
  {
    this.router.navigate(["design"]);
  }

}