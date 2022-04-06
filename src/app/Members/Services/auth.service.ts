import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthRequestDTO } from '../Models/DTO/AuthRequestDTO';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UrlSerializer } from '@angular/router';
import { ResetPasswordDTO } from '../Models/DTO/ResetPasswordDTO';
import {SocialAuthService} from 'angularx-social-login';
import {GoogleLoginProvider} from 'angularx-social-login';
import { ExternalAuthDTO } from '../Models/DTO/ExternalAuthDTO';

//@Injectable()

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL: string = 'http://localhost:5000/api/auth/';

  constructor(private _http: HttpClient, private _jwtService: JwtHelperService,private _externalAuthService:SocialAuthService) { }

  //internal login using email and password
  public login = (user: AuthRequestDTO): any => {
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

  //logout from the application
  public logout = () => {
    localStorage.removeItem("JWTToken");
  }
  ///get the user id using email for password reset
  public getUserIdByEmail=(user:AuthRequestDTO):any=>{

    let parameters=new HttpParams().append('email',user.email);
    return this._http.get(this.BASE_URL+'getUserIdByEmail',{params: parameters});
  }

  //reset the password in the database
  public reSetUserPassword = (user:ResetPasswordDTO) =>{
    return this._http.post(this.BASE_URL+'resetUserPassword',user,{
      headers:new HttpHeaders({
        "Content-type": "application/json"
      })
    })
  }

  public externalLoginGoogle = ()=>{
    return this._externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  public externalLogoutGoogle=()=>{
    this._externalAuthService.signOut();
  }

  public validateExternalLogin = (externalAuthDTO:ExternalAuthDTO) =>{
    return this._http.post(this.BASE_URL+'externalGoogleLogin',externalAuthDTO)
  }
}
