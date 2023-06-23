import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {EnvironmentUrlService} from '../../Common/Services/environment.url.service'
import { ITestimonyDTO } from 'src/app/DataModels/DTO/ITestimonyDTO';
import { Observable,of } from 'rxjs';

import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TestimonyService {
  //private URL_Switch="DOCKER" ; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
  private BASE_URL_Segment: string = 'testimony/';
  constructor(private _http: HttpClient,private _environmentService:EnvironmentUrlService) {}

  // public getUserTestimony = (UserID: number) => {
  //   return this._http.get<string>(this.generateURL(this._environmentService.URL_Switch) + `getUserTestimony/${UserID}`, {
  //     headers:new HttpHeaders({
  //       "Accept":"application/json"
  //     })
  //   });
  // };

  public getUserTestimony = (UserID: number) => {
    return this._http.get<ITestimonyDTO>(this.generateURL(this._environmentService.URL_Switch,UserID)+`getUserTestimony`, {
      headers:new HttpHeaders({
        "Accept":"application/json"
      })
    }).pipe(catchError(res=>of(""))); // this means that 404 is sent from API
  };

  public createUserTestimony =(testimony:ITestimonyDTO)=>{
    return this._http.post<ITestimonyDTO>(this.generateURL(this._environmentService.URL_Switch,testimony.userID)+`createUserTestimony`,testimony);
  }

  public modifyTestimony=(testimonyDTO:ITestimonyDTO) =>{
    return this._http.patch(this.generateURL(this._environmentService.URL_Switch,testimonyDTO.userID)+`updateUserTestimony/${testimonyDTO.testimonyID}`,testimonyDTO);
  }
  // private generateURL=(URLswitch:string)=>{
  //   let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
  //   return api_URL+this.BASE_URL_Segment;
  // }
  private generateURL=(URLswitch:string,userID:number)=>{
    let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
    return api_URL+`user/${userID}/${this.BASE_URL_Segment}`;
  }
}
