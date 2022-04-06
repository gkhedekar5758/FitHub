import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Form, FormControl, NgForm, Validator, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { AuthRequestDTO } from '../../Models/DTO/AuthRequestDTO';
import {ExternalAuthDTO} from '../../Models/DTO/ExternalAuthDTO'
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



  constructor(private authService:AuthService,private routerService:Router) { }

  ngOnInit(): void {
    //console.log("init fired");

  }
  public Login=(formValue:NgForm)=>{

    const user:AuthRequestDTO={
      email:formValue.form.controls.email.value,
      password:formValue.form.controls.password.value,
      clientURL:"http://localhost:4200/members/forgotPassword"
    }
    this.authService.login(user)
      .subscribe( response => {
        const JWTToken=(<any>response).token;
        //console.log(JWTToken.result);
        localStorage.setItem("JWTToken",JWTToken.result);
        this.routerService.navigate(['/classes'])
      },
      error=>{

        this.isErrored=true;
        this.errorMessage=error.error.errorMessage;
        if(error.error.errorMessage === undefined){
          this.errorMessage="Something bad happen, Try again later !!"
        }
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
    const JWTToken=(<any>response).token;
        //console.log(JWTToken.result);
    localStorage.setItem("JWTToken",JWTToken.result);
  }, error =>{
    this.isErrored=true;
    this.errorMessage=error.error.errorMessage;
    this.authService.externalLogoutGoogle();
  })
  }

}
