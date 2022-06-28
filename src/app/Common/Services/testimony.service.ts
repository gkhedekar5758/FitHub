import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TestimonyService {
  private BASE_URL: string = 'http://localhost:5000/api/testimony/';
  constructor(private _http: HttpClient) {}

  public getUserTestimony = (UserID: number) => {
    return this._http.get<string>(this.BASE_URL + `getUserTestimony/${UserID}`, {
      headers:new HttpHeaders({
        "Accept":"application/json"
      })
    });
  };
}
