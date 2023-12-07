import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './webComponent/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './webComponent/forgot-password/forgot-password.component';
import { FieldMatcherDirective } from '../Common/Directives/field-matcher.directive';
import { RegistrationComponent } from './registration/registration.component';
import { UserHomeComponent } from './webComponent/user-home/user-home.component';
import { AuthGuard } from '../Common/Guards/auth.guard';
import { SharedComponentModule } from '../Common/shared-component.module';

const userRoot: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'userhome', component: UserHomeComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    LoginComponent,
    ForgotPasswordComponent,
    FieldMatcherDirective,
    RegistrationComponent,
    UserHomeComponent,
  ],
  imports: [
    RouterModule.forChild(userRoot),
    ReactiveFormsModule,
    SharedComponentModule,
  ],
  providers: [],
  exports: [],
})

//if user interact with members arena then only activate this
export class MembersModule {}
