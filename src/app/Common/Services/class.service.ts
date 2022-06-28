import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IClass } from 'src/app/DataModels/class.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClassService {

  private BASE_URL:string ='http://localhost:5000/api/class/';


  constructor(private _http:HttpClient) { }

  //get all the classes from the DB to show on class component
  public getClasses = () =>{
    return this._http.get<IClass[]>(this.BASE_URL+'getClasses');   // THIS IS WORKING

  }
}
