import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Category } from '../models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private options: any ;
  private urlApi :string = environment.baseUrl;

  constructor(private httpClient: HttpClient) { }

  public getCategory(urlWebService : string): Observable<Category[]> {

    this.options ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
      }),
      
      withCredentials: true
    };
    return this.httpClient.get<Category[]>(this.urlApi+urlWebService,this.options)
    .pipe(
      map((response: any) => {
        if(response){
          console.info('Category has been loaded');
          return response;         
        }  })
    );
    
  }
  public deleteCategory (urlWebService : string,idCategory:number | undefined): Observable<boolean> {

    this.options ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
      }),
      withCredentials: true
    };
    return this.httpClient.delete<boolean>(this.urlApi+urlWebService+'/'+idCategory,this.options)
    .pipe(
      map((response: any) => {
        if(response){
          console.info('Category has been deleted');
          return response;
          
        }  })
    ); 

  }


  public updateCategory (urlWebService : string,idCategory:number | undefined , category : Category): Observable<Category> {

    this.options ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
      }),
      withCredentials: true
    };
    return this.httpClient.put<Category>(this.urlApi+urlWebService+'/'+idCategory,category,this.options)
    .pipe(
      map((response: any) => {
        if(response){
          console.info('Category has been updated');
          return response;
          
        }  })
    ); 

  }






}
