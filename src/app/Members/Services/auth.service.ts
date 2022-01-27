import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUser } from '../Models/IUser';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL: string = 'http://localhost:5000/api/auth/';

  constructor(private _http: HttpClient, private _jwtService: JwtHelperService) { }

  //internal login using email and password
  public login = (user: IUser): any => {
    return this._http.post(this.BASE_URL + 'login', user, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      })
    });
  }

  //check if user is authenticated and the token is still valid
  //NOTE: as per the post I am not creaeting the observable as of now
  // if needed in future will create it
  public isUserAuthenticated = (): boolean => {
    const JWTToken: string = localStorage.getItem("JWTToken");
    return !!JWTToken && !this._jwtService.isTokenExpired(JWTToken);
  }

  public logout = () => {
    localStorage.removeItem("JWTToken");
  }
}
