import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUserDTO, RegisterUserInfoDTO } from '../Models/DTO/RegisterUserDTO';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'FH-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {

  //#region  getters
  get firstName(): AbstractControl {
    return this.registrationForm.get('firstName');
  }
  get lastName(): AbstractControl {
    return this.registrationForm.get('lastName');
  }
  get mobileNo(): AbstractControl {
    return this.registrationForm.get('userInfo.mobileNo');
  }
  get emMobileNo(): AbstractControl {
    return this.registrationForm.get('userInfo.emergencyMobileNo');
  }
  get height(): AbstractControl {
    return this.registrationForm.get('userInfo.height');
  }
  get weight(): AbstractControl {
    return this.registrationForm.get('userInfo.weight');
  }
  get password(): AbstractControl {
    return this.registrationForm.get('password');
  }
  get confirmPassword(): AbstractControl {
    return this.registrationForm.get('confirmPassword');
  }
  get email() {
    return this.registrationForm.get('email');
  }
  //#endregion
  registrationForm = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),

    userInfo: new FormGroup({
      mobileNo: new FormControl('', [Validators.maxLength(10)]),
      emergencyMobileNo: new FormControl('', [Validators.required, Validators.maxLength(10)]),
      height: new FormControl('', [
        Validators.required,
        Validators.min(20),
        Validators.max(245),
      ]),

      weight: new FormControl('', [
        Validators.required,
        Validators.min(20),
        Validators.max(200),
      ]),
      BMI: new FormControl(''),
    }),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(8),
    ]),
    confirmPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(8),
    ]),
  });
  constructor(private authService: AuthService,private router:Router) { }

  ngOnInit(): void { }

  calculateBMI = (height, weight) => {
    // console.log(height);
    // console.log(weight);
    let meterSquare = Math.pow((height / 100), 2);
    this.registrationForm.get('userInfo.BMI').setValue(Math.floor(weight / meterSquare));

  }

  Register = (formValue) => {
    const userInfo: RegisterUserInfoDTO = {
      height: formValue.userInfo.height,
      weight: formValue.userInfo.weight,
      mobileNo: formValue.userInfo.mobileNo,
      emergencyMobileNo: formValue.userInfo.emergencyMobileNo,
      BMI: formValue.userInfo.BMI
    }
    const user: RegisterUserDTO = {
      email: formValue.email,
      password: formValue.password,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      isExternalProvider: false,
      isActive: true,
      userInfo: userInfo
    }

    this.authService.register(user).subscribe(
      (response: any) => {
        if (response.success)
          alert("Registration Success!! Login with your credentials")
          this.router.navigate(['members/login'])
      }, error => console.log(error))
  }
}
