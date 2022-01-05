import { Component, OnInit } from '@angular/core';
import { FormControl, Validator, Validators } from '@angular/forms'

@Component({
  selector: 'FH-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

email:string;
password:string;


  constructor() { }

  ngOnInit(): void {

  }
  Login(){

    // console.log(formLogin.controls)
  }

}
