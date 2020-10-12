import { Type } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DesignService } from '../shared/design.service';
import { Category } from './category.model';
import { Prodtypes } from './prodtype.model';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  catList: Category[];
  types: Prodtypes[];
  
  //selectedId: number;

  constructor(public service : DesignService, private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.service.getCategories().then(res => this.catList = res as Category[]);
    this.service.getTypes().then(res => this.types = res as Prodtypes[]);
  }

  design(id)
  {
    console.log(id);
    localStorage["Product_Type_ID"] = id;
    this.router.navigate(['/design'])

  }

  store(e)
  {
    localStorage["Product_Category_ID"] = e.target.value;
    this.service.getProductType(localStorage["Product_Category_ID"]).then(resp => {
      console.log(resp);
    });
    console.log(this.types)
  }

  onClick(Product_Type_ID: number){
    this.router.navigate(['/design', Product_Type_ID])

  }

 
}


