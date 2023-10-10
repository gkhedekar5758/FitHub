import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ICoachClassResponseDTO } from 'src/app/DataModels/DTO/ResponseDTO/ICoachClassResponseDTO';
import { ICoach } from 'src/app/DataModels/coach.model';
import { Rating } from 'src/app/DataModels/rating.model';
import {EnvironmentUrlService} from '../../Common/Services/environment.url.service'
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { ICoachRatingResponseDTO } from 'src/app/Members/Models/DTO/ResponseDTO/ICoachRatingResponseDTO';

@Injectable({
  providedIn: 'root',
})
export class CoachService {
  //private URL_Switch="DOCKER" ; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
  private BASE_URL_Segment: string = 'coach/';
  
  constructor(private _http: HttpClient,private _environmentService:EnvironmentUrlService) {
    
  }

  
  public getCoachesByClassID = (classID: number) => {
    return this._http.get<ICoach[]>(
      this.generateURL(this._environmentService.URL_Switch) + `getCoachesByClassID/${classID}`
    );
  };

  public getCoachByCoachID = (coachID: number) => {
    return this._http.get<ICoachClassResponseDTO>(
      this.generateURL(this._environmentService.URL_Switch) + `getCoachByCoachID/${coachID}`
    );
  };

  public getCoachRatingByUserID = (coachID:number, UserID:number) => {
    let httpParameter = new HttpParams()
      .set('coachID', String(coachID))
      .set('userID',String(UserID));

    return this._http.get<Rating>(this.generateURL(this._environmentService.URL_Switch) + `getCoachRatingByUserID`, {
      params: httpParameter,
    }).pipe(catchError(res=>of({}))); // 404 was sent from api
  };

  public addCoachRatingByUser = (rating:Rating) => {
    return this._http.post(this.generateURL(this._environmentService.URL_Switch) + `addCoachRatingByUser`, rating);
  };

  public updateCoachRatingByUser=(coachID:number,userID:number,rating:Rating)=>{
    return this._http.put(this.generateURL(this._environmentService.URL_Switch)+`updateCoachRatingByUser`,rating,{
      params:new HttpParams()
      .set('coachID',String(coachID))
      .set('userID',String(userID))
    })
  }

  public getAllCoachRatingByUser=(userID:number)=>{
    return this._http.get<ICoachRatingResponseDTO[]>(this.generateURL(this._environmentService.URL_Switch)+'getallcoachratingbyuser/'+userID);
  }
  private generateURL=(URLswitch:string)=>{
    let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
    return api_URL+this.BASE_URL_Segment;
  }
}
