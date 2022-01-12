import { Injectable } from '@angular/core';
import{HttpClient, HttpHeaders} from '@angular/common/http';
import { IUser } from '../Models/IUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL:string='http://localhost:5000/api/auth/';

  constructor(private _http:HttpClient) { }

  public login:any = (user:IUser) =>{
    return this._http.post(this.BASE_URL+'login',user,{
      headers : new HttpHeaders({
      "Content-type":"application/json"
    })
  });
  }

  public isUserAuthenticated = ():boolean =>{
    const JWTToken=localStorage.getItem("JWTToken");
    return !!JWTToken;
  }

  public logout = ()=>{
    localStorage.removeItem("JWTToken");
  }
}
