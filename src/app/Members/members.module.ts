import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './webComponent/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './webComponent/forgot-password/forgot-password.component';
import { FieldMatcherDirective } from '../Common/Directives/field-matcher.directive';
import { RegistrationComponent } from './registration/registration.component';
import { UserHomeComponent } from './webComponent/user-home/user-home.component';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button'
import {DragDropModule} from '@angular/cdk/drag-drop';
import { AuthGuard } from '../Common/Guards/auth.guard';
import { SharedComponentModule } from '../Common/shared-component.module';

const userRoot: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'register', component: RegistrationComponent },
  {path:'userhome',component:UserHomeComponent,canActivate:[AuthGuard]}
];

@NgModule({
  declarations: [
    LoginComponent,
    ForgotPasswordComponent,
    FieldMatcherDirective,
    RegistrationComponent,
    UserHomeComponent,
    //RatingComponent
    
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userRoot),
    // MatCardModule,
    // MatButtonModule,
    // DragDropModule,
    FormsModule,
    ReactiveFormsModule,
    SharedComponentModule,
    //SocialLoginModule
  ],
  providers: [],
  exports: [
    //RatingComponent
  ],
})

//if user interact with members arena then only activate this
export class MembersModule {}
