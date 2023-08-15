import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Category } from 'src/app/models/Category';
import { Product } from 'src/app/models/Product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  form!: FormGroup;
  public listProduct : Product[] = [];
  public listCategory : Category[] = [];
  private urlWebService : string ='';
  constructor(private productService : ProductService,
              private categoryService : CategoryService) { }

  ngOnInit(): void {
    this.getProduct();
    this.getCategory();
  }
  public getProduct(){
    this.urlWebService ='Product/products';
    this.productService.getProduct(this.urlWebService).subscribe(
      data => {
          this.listProduct = data;
          return this.listProduct;         
        } ,
        error  => {
          console.log(error);
        } 
      
    );
  }


  public getCategory(){
    this.urlWebService ='Category/categories';
    this.categoryService.getCategory(this.urlWebService).subscribe(
      data => {
          this.listCategory = data;
          return this.listCategory;         
        } ,
        error  => {
          console.log(error);
        } 
      
    );
  }

  public supprimerProduit(productId :number | undefined){
    this.urlWebService ='Product';
    this.productService.deleteProduct(this.urlWebService,productId).subscribe(
      data => {
          this.ngOnInit();   
          //return data;     
          location.reload();
        } ,
        error  => {
          console.log(error);
        } 
      
    );
  }

  public supprimerCategory(categoryId :number | undefined){
    this.urlWebService ='Category/categories';
    this.categoryService.deleteCategory(this.urlWebService,categoryId).subscribe(
      data => {
          this.ngOnInit();   
          //return data;     
          location.reload();
        } ,
        error  => {
          console.log(error);
        } 
      
    );
  }


  public updateProduit(product : Product){
      debugger;
      product.productDescription = (<HTMLInputElement>document.getElementById('inputDescriptionupdate'+product.productId)).value;
      product.productName = (<HTMLInputElement>document.getElementById('inputlibelleupdate'+product.productId)).value;
      product.productmanufacturing = new Date((<HTMLInputElement>document.getElementById('inputDateupdate'+product.productId)).value);
      this.urlWebService ='Product';
      this.productService.updateProduit(this.urlWebService,product.productId,product).subscribe(
        data => {
          
            this.ngOnInit();   
            // return data;     
            location.reload();
          } ,
          error  => {
            console.log(error);
          } 
        
      );
    }


    public updateCategory(category : Category){
       category.categoryName = (<HTMLInputElement>document.getElementById('inputlibelleupdatecat'+category.categoryId)).value;
       this.urlWebService ='Category/categories';
       this.categoryService.updateCategory(this.urlWebService,category.categoryId,category).subscribe(
         data => {
          
             this.ngOnInit();   
              //return data;     
              location.reload();
           } ,
           error  => {
             console.log(error);
           } 
        
       );
    }
    
   public addCategory(){
    
   }
  
}
