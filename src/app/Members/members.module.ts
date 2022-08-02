import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './webComponent/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './webComponent/forgot-password/forgot-password.component';
import { FieldMatcherDirective } from '../Common/field-matcher.directive';
import { RegistrationComponent } from './registration/registration.component';

const userRoot: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'register', component: RegistrationComponent },
];

@NgModule({
  declarations: [
    LoginComponent,
    ForgotPasswordComponent,
    FieldMatcherDirective,
    RegistrationComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userRoot),

    FormsModule,
    ReactiveFormsModule,
    //SocialLoginModule
  ],
  providers: [],
  exports: [
    //SocialLoginModule
  ],
})

//if user interact with members arena then only activate this
export class MembersModule {}
