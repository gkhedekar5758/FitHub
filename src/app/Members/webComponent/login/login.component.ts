import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Form, FormControl, NgForm, Validator, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { AuthRequestDTO } from '../../Models/DTO/AuthRequestDTO';
import {ExternalAuthDTO} from '../../Models/DTO/ExternalAuthDTO'
import { AuthResponseDTO } from '../../Models/DTO/ResponseDTO/AuthResponseDTO';
import {AuthService} from '../../Services/auth.service';

@Component({
  selector: 'FH-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

email:string='';
password:string;
isErrored:boolean=false;
errorMessage:string='';

_returnURL:string;



  constructor(private authService:AuthService,private routerService:Router,private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    //console.log("init fired");
    this._returnURL= this.activatedRoute.snapshot.queryParams['returnUrl']||'members/userhome'||'/';

  }
  public Login=(formValue:NgForm)=>{

    const user:AuthRequestDTO={
      email:formValue.form.controls.email.value,
      password:formValue.form.controls.password.value,
      clientURL:"/members/forgotPassword",
      clientRegURL:'/members/register'
    }
    this.authService.login(user)
      .subscribe( response => {
        let apiResponse=<AuthResponseDTO>response;
        const JWTToken= <any>apiResponse.token;

        localStorage.setItem("JWTToken",JSON.stringify(JWTToken.result));
        localStorage.setItem("User",JSON.stringify(apiResponse.user));
        this.routerService.navigate([this._returnURL]);
      },
      error=>{

        this.isErrored=true;
        console.log( error);
        this.errorMessage=error;
        // if(error.error.errorMessage === undefined){
        //   this.errorMessage="Something bad happen, Try again later !!"
        // }
        //console.log( error.error.errorMessage);
      });
     formValue.resetForm();
  }

public  LoginExternalGoogle=()=> {
    //console.log("ah");
    this.isErrored=false;
    this.authService.externalLoginGoogle()
    .then( response => {
      console.log(response);
       const externalAuthDTO:ExternalAuthDTO={
         idToken:response.idToken,
         provider:response.provider
       }
       this.validateExternalAuthentication(externalAuthDTO);
      //https://levelup.gitconnected.com/how-to-sign-in-with-google-in-angular-and-use-jwt-based-net-core-api-authentication-rsa-6635719fb86c
    },
    error=>{
      console.log(error);
    })
  }

  //validate the google token on the API side
private  validateExternalAuthentication = (externalAuthDTO:ExternalAuthDTO)=>{
  this.authService.validateExternalLogin(externalAuthDTO)
  .subscribe( response =>{
    let apiResponse= <AuthResponseDTO>response;
    const JWTToken=<any>apiResponse.token;
        //console.log(JWTToken.result);
    localStorage.setItem("JWTToken",JWTToken.result);
    localStorage.setItem("User",JSON.stringify(apiResponse.user));
    this.routerService.navigate(['/classes'])
  }, error =>{
    this.isErrored=true;
    this.errorMessage=error.error.errorMessage;
    this.authService.externalLogoutGoogle();
  })
  }

}
