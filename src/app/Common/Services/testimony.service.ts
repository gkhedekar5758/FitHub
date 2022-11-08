import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {EnvironmentUrlService} from '../../Common/Services/environment.url.service'

@Injectable({
  providedIn: 'root',
})
export class TestimonyService {
  //private URL_Switch="DOCKER" ; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
  private BASE_URL_Segment: string = 'testimony/';
  constructor(private _http: HttpClient,private _environmentService:EnvironmentUrlService) {}

  public getUserTestimony = (UserID: number) => {
    return this._http.get<string>(this.generateURL(this._environmentService.URL_Switch) + `getUserTestimony/${UserID}`, {
      headers:new HttpHeaders({
        "Accept":"application/json"
      })
    });
  };

  private generateURL=(URLswitch:string)=>{
    let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
    return api_URL+this.BASE_URL_Segment;
  }
}
