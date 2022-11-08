import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthRequestDTO } from '../Models/DTO/AuthRequestDTO';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ResetPasswordDTO } from '../Models/DTO/ResetPasswordDTO';
import {SocialAuthService} from 'angularx-social-login';
import {GoogleLoginProvider} from 'angularx-social-login';
import { ExternalAuthDTO } from '../Models/DTO/ExternalAuthDTO';
import {RegisterUserDTO} from '../Models/DTO/RegisterUserDTO';
import {AuthResponseDTO} from '../Models/DTO/ResponseDTO/AuthResponseDTO';
import {EnvironmentUrlService} from '../../Common/Services/environment.url.service';



//@Injectable()

@Injectable({
  providedIn: 'root'
})
export class AuthService {

//  private BASE_URL: string = 'http://localhost:5000/api/auth/';
//private URL_Switch="DOCKER" ; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
private BASE_URL_Segment: string = 'auth/';

  constructor(private _http: HttpClient, private _jwtService: JwtHelperService,private _externalAuthService:SocialAuthService,private _environmentService:EnvironmentUrlService) { }

  //internal login using email and password
  public login = (user: AuthRequestDTO): any => {
    return this._http.post<AuthResponseDTO>(this.generateURL(this._environmentService.URL_Switch) + 'login', user, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      })
    });
  }

  //check if user is authenticated and the token is still valid
  //NOTE: as per the post I am not creaeting the observable as of now
  // if needed in future will create it
  // return true if the jwt token is there and token is NOT expired
  public isUserAuthenticated = (): boolean => {
    const JWTToken = JSON.parse(localStorage.getItem("JWTToken"));
    //console.log(this._jwtService.decodeToken(JWTToken));
    return JWTToken && !this._jwtService.isTokenExpired(JWTToken);
  }
  public isTokenExpired=():boolean=>{
    const JWTToken=JSON.parse(localStorage.getItem("JWTToken"));
    console.log(this._jwtService.isTokenExpired(JWTToken));
    if(!JWTToken) return true;
    return this._jwtService.isTokenExpired(JWTToken);
  }

  //logout from the application
  public logout = () => {
    localStorage.removeItem("JWTToken");
    localStorage.removeItem("User");
  }
  ///get the user id using email for password reset
  public getUserIdByEmail=(user:AuthRequestDTO):any=>{

    let parameters=new HttpParams().append('email',user.email);
    return this._http.get(this.generateURL(this._environmentService.URL_Switch)+'getUserIdByEmail',{params: parameters});
  }

  //reset the password in the database
  public reSetUserPassword = (user:ResetPasswordDTO) =>{
    return this._http.post(this.generateURL(this._environmentService.URL_Switch)+'resetUserPassword',user,{
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
    return this._http.post(this.generateURL(this._environmentService.URL_Switch)+'externalGoogleLogin',externalAuthDTO)
  }

  public getCurrentLoggedInUser = () =>{
    return JSON.parse(localStorage.getItem("User"));
  }
  public register=(user:RegisterUserDTO)=>{
    return this._http.post(this.generateURL(this._environmentService.URL_Switch)+'registerUser',user,{
      headers:new HttpHeaders({
        "Content-type":"application/json"
      })
    })
  }

  private generateURL=(URLswitch:string)=>{
    let api_URL= URLswitch=="DOCKER"? this._environmentService.apiURLDocker:this._environmentService.apiURL;
    return api_URL+this.BASE_URL_Segment;
  }
}
