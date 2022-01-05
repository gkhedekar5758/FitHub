import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './webComponent/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



const userRoot: Routes = [
  { path: 'login', component: LoginComponent },
  //  {path:'register'}
];

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(userRoot),


    FormsModule,
    ReactiveFormsModule
  ],
})

//if user interact with members arena then only activate this
export class MembersModule { }
