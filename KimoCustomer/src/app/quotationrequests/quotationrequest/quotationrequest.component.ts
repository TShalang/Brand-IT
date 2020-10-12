import { NgForOf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/shared/customer.model';
import { CustomerService } from 'src/app/shared/customer.service';
import { QuotationrequestService } from 'src/app/shared/quotationrequest.service';
import { QuotereqlineComponent } from '../quotereqline/quotereqline.component';

@Component({
  selector: 'app-quotationrequest',
  templateUrl: './quotationrequest.component.html',
  styleUrls: ['./quotationrequest.component.css']
})
export class QuotationrequestComponent implements OnInit {

  cusList: Customer[];
  isValid: boolean = true;

  constructor(public service: QuotationrequestService,
    private dialog:MatDialog, public customerService: CustomerService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
    this.resetForm();
    this.customerService.getCustomer().then(res => this.cusList = res as Customer[]);
    
  }

  resetForm(form?:NgForm){
    if(form=null)
    form.resetForm();
    this.service.formData = {
    Request_ID: null,    
    Customer_ID: 0,    
    Date: new Date()
    };
    this.service.reqLine=[];
  }

  AddReq(reqLineIndex, Request_ID){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width="30%";
    dialogConfig.data = {reqLineIndex, Request_ID};
  this.dialog.open(QuotereqlineComponent, dialogConfig);
  
  }

  onDeleteReqLine(reqLineID: number, i: number){
    //if (designLineID !=null)
    //this.service.formData.DeletedOrderItemIDs += designLineID + ",";
    this.service.reqLine.splice(i,1);
  }

  validateForm(){
    this.isValid = true;
    if(this.service.formData.Customer_ID==0)
    this.isValid = false;
    else if(this.service.reqLine.length==0)
    this.isValid = false;
    
    return this.isValid;
  }

  onSubmit(form:NgForm){
    if(this.validateForm())
    {
      this.service.saveReq().subscribe(res => {
        this.resetForm();
        this.toastr.success('Quotation request sent','Success!');
        this.router.navigate(['designs']);
      })
    }

  }
}
