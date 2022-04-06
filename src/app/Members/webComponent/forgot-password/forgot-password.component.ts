import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthRequestDTO } from '../../Models/DTO/AuthRequestDTO';
import{ResetPasswordDTO} from '../../Models/DTO/ResetPasswordDTO'
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'FH-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  isPasswordFieldsHidden:boolean=true;
  email:string='';
  password:string;
  rePassword:string;

  userId:number;
  errorMessage:string='';
  isErrored:boolean=false;
  isSuccess:boolean=false;

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
  }

  GetUserIDByEmail(formValue:NgForm){
    //console.log(formValue.controls.email.value);
    this.isErrored=false;
    let user:AuthRequestDTO={
      email:formValue.form.controls.email.value,
      password:null,
      clientURL:null
    }
    this.authService.getUserIdByEmail(user)
    .subscribe(response => {
      //console.log(response);
      this.userId=response;
      this.isPasswordFieldsHidden=false;
    },
    error=>{
      this.isErrored=true;
      this.errorMessage=error.error.errorMessage;
    }
    )
  }

  ReSetUserPassword(formValue:NgForm){
    console.log(formValue.controls.password.value)
    console.log(this.userId);

    this.isErrored=false;
    const resetPasswordDto:ResetPasswordDTO={
      userId:this.userId,
      password:formValue.controls.password.value,
    }
    this.authService.reSetUserPassword(resetPasswordDto)
    .subscribe(()=>{
      //console.log("succ");
      this.isSuccess=true;
    }),
    error=>{
      //console.log(error);
      this.isErrored=true;
      this.errorMessage=error.error.errorMessage;
    }
  }


}
