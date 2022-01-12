import { Component, OnInit } from '@angular/core';
import { Form, FormControl, Validator, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { IUser } from '../../Models/IUser';
import {AuthService} from '../../Services/auth.service';

@Component({
  selector: 'FH-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

email:string;
password:string;


  constructor(private authService:AuthService,private routerService:Router) { }

  ngOnInit(): void {

  }
  Login(formValue){
    //console.log(formValue);
    const login={...formValue};
    const user:IUser={
      email:formValue.email,
      password:formValue.password
    }
    this.authService.login(user)
      .subscribe( response => {
        const JWTToken=(<any>response).token;
        //console.log(JWTToken.result);
        localStorage.setItem("JWTToken",JWTToken.result);
        this.routerService.navigate(['/classes'])
      },
      error=>{
        console.log(error);
      });
  }

}
