import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IClass } from 'src/app/DataModels/class.model';
import { Observable } from 'rxjs';
import {EnvironmentUrlService} from '../../Common/Services/environment.url.service'

@Injectable({
  providedIn: 'root'
})
export class ClassService {

  //private BASE_URL:string ='http://localhost:5000/api/class/';
  //private URL_Switch="DOCKER" ; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
  private BASE_URL_Segment: string = 'class/';


  constructor(private _http:HttpClient,private _environmentService:EnvironmentUrlService) { }

  //get all the classes from the DB to show on class component
  public getClasses = () =>{
    return this._http.get<IClass[]>(this.generateURL(this._environmentService.URL_Switch)+'getClasses');   // THIS IS WORKING

  }
  private generateURL=(URLswitch:string)=>{
    let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
    return api_URL+this.BASE_URL_Segment;
  }
}
