import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http'
import { ICoachClassResponseDTO } from 'src/app/DataModels/DTO/ResponseDTO/ICoachClassResponseDTO';
import { ICoach } from 'src/app/DataModels/coach.model';


@Injectable({
  providedIn: 'root'
})
export class CoachService {

  private BASE_URL:string ='http://localhost:5000/api/coach/';
  constructor(private _http:HttpClient) { }

  public getCoachesByClassID =(classID:number) =>{
    return this._http.get<ICoach[]>(this.BASE_URL+`getCoachesByClassID/${classID}`);
  }

  public getCoachByCoachID = (coachID:number) =>{
    return this._http.get<ICoachClassResponseDTO>(this.BASE_URL+`getCoachByCoachID/${coachID}`);
  }

  public getCoachRatingByUserID=(coachID,UserID)=>{
    let httpParameter=new HttpParams()
      .set("coachID",coachID)
      .set("userID",UserID);

    return this._http.get<number>(this.BASE_URL+`getCoachRatingByUserID`,{params:httpParameter});

  }
}
