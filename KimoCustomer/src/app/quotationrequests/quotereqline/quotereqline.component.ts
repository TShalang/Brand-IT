import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DesignService } from 'src/app/shared/design.service';
import { Quotationrequest } from 'src/app/shared/quotationrequest.model';
import { QuotationrequestService } from 'src/app/shared/quotationrequest.service';
import { Quotereqline } from 'src/app/shared/quotereqline.model';
import { Size } from 'src/app/size';
import { Weblinks } from 'src/app/weblinks.model';

@Component({
  selector: 'app-quotereqline',
  templateUrl: './quotereqline.component.html',
  styleUrls: ['./quotereqline.component.css']
})
export class QuotereqlineComponent implements OnInit {

  formData: Quotereqline;
  //posList: Tempposition[];
  //jobList: Jobtype[];
  webList: Weblinks[];
  sizeList: Size[];
  isValid: boolean = true;
  designID: any;

  constructor(@Inject(MAT_DIALOG_DATA) public data,
    public dialogRef: MatDialogRef<QuotereqlineComponent>, private reqService: QuotationrequestService,
    public service:DesignService)
  //public posService: TemppositionService, 
  { }

  ngOnInit(): void {
    //this.posService.getPositionList().then(res => this.posList = res as Tempposition[]);
    //this.posService.getJobTypeList().then(res => this.jobList = res as Jobtype[]);
    this.reqService.getSizeList().then(res => this.sizeList = res as Size[]);
    this.reqService.getWebList().then(res => this.webList = res as Weblinks[]);
    this.designID = this.service.getDesign(localStorage["Design_ID"]).subscribe(designID => this.designID = designID );
     console.log(this.designID)


    // this.service.GetDesign(localStorage["Request_ID"]).subscribe(x=>{
    //   this.reqList = x;
    


    if (this.data.reqLineIndex == null)
      this.formData = {
        Request_Line_ID: null,
        Request_ID: this.data.Request_ID,
        Design_ID: localStorage["Design_ID"] ,
        Product_Size_ID: 0,
        Colour: '',
        Web_Link_ID: 0,
        Product_Code: '',
        Quantity: 0,

      }
    else
      this.formData = Object.assign({}, this.reqService.reqLine[this.data.reqLineIndex]);

  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      if (this.data.reqLineIndex == null)
    this.reqService.reqLine.push(form.value)

    else
      this.reqService.reqLine[this.data.reqLineIndex] = form.value;
    
    this.dialogRef.close();
    }
  }
  validateForm(formData: Quotereqline) {  
    this.isValid = true;
    if (formData.Product_Size_ID == 0)
      this.isValid = false;
    else if (formData.Colour == '')
      this.isValid = false;
    else if (formData.Web_Link_ID == 0)
      this.isValid = false;
    else if (formData.Product_Code == '')
      this.isValid = false;
    else if (formData.Quantity == 0)
      this.isValid = false;
    return this.isValid;
  }
}
