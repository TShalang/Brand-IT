import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DesignService } from 'src/app/shared/design.service';
import { Designline } from 'src/app/shared/designline.model';
import { Jobtype } from 'src/app/shared/jobtype.model';
import { Tempposition } from 'src/app/shared/tempposition.model';
import { TemppositionService } from 'src/app/shared/tempposition.service';

@Component({
  selector: 'app-designline',
  templateUrl: './designline.component.html',
  styleUrls: ['./designline.component.css']
})
export class DesignlineComponent implements OnInit {
  formData:Designline;
  posList: Tempposition[];
  jobList: Jobtype[];
  isValid:  boolean = true;

  constructor(@Inject(MAT_DIALOG_DATA) public data,
  public dialogRef: MatDialogRef<DesignlineComponent>, 
  public posService: TemppositionService,
  public designService: DesignService) { }

  ngOnInit(): void {
    this.posService.getPositionList().then(res => this.posList = res as Tempposition[]);
    this.posService.getJobTypeList().then(res => this.jobList = res as Jobtype[]);
    if (this.data.designLineIndex == null)
    this.formData = {
      Design_Line_ID: null,
      Design_ID: this.data.Design_ID,
      Template_Position_ID: 2,
      Job_Type_ID: 2,
      Width: 2,
      Height: 2,
      Image: '',
      Text: 'hey',     
    }
    else
    this.formData = Object.assign({}, this.designService.designLine[this.data.designLineIndex]);

  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      if (this.data.designLineIndex == null)
        this.designService.designLine.push(form.value);
      else
      this.designService.designLine[this.data.designLineIndex] = form.value;
      this.dialogRef.close();
    }
  }
    validateForm(formData: Designline) {
      this.isValid = true;
      if (formData.Template_Position_ID == 0)
        this.isValid = false;
      else if (formData.Job_Type_ID == 0)
        this.isValid = false;
      else if (formData.Width <= 0)
        this.isValid = false;
      else if (formData.Height <= 0)
        this.isValid = false;
      return this.isValid;
    }
  }



