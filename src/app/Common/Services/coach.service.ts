import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ICoachClassResponseDTO } from 'src/app/DataModels/DTO/ResponseDTO/ICoachClassResponseDTO';
import { ICoach } from 'src/app/DataModels/coach.model';
import { Rating } from 'src/app/DataModels/rating.model';

@Injectable({
  providedIn: 'root',
})
export class CoachService {
  private BASE_URL: string = 'http://localhost:5000/api/coach/';
  constructor(private _http: HttpClient) {}

  public getCoachesByClassID = (classID: number) => {
    return this._http.get<ICoach[]>(
      this.BASE_URL + `getCoachesByClassID/${classID}`
    );
  };

  public getCoachByCoachID = (coachID: number) => {
    return this._http.get<ICoachClassResponseDTO>(
      this.BASE_URL + `getCoachByCoachID/${coachID}`
    );
  };

  public getCoachRatingByUserID = (coachID:number, UserID:number) => {
    let httpParameter = new HttpParams()
      .set('coachID', String(coachID))
      .set('userID',String(UserID));

    return this._http.get<Rating>(this.BASE_URL + `getCoachRatingByUserID`, {
      params: httpParameter,
    });
  };

  public addCoachRatingByUser = (rating:Rating) => {
    return this._http.post(this.BASE_URL + `addCoachRatingByUser`, rating);
  };

  public updateCoachRatingByUser=(coachID:number,userID:number,rating:Rating)=>{
    return this._http.put(this.BASE_URL+`updateCoachRatingByUser`,rating,{
      params:new HttpParams()
      .set('coachID',String(coachID))
      .set('userID',String(userID))
    })
  }
}
