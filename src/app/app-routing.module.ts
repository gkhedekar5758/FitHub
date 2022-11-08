import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClassDetailComponent } from './webComponent/classes/class-detail.component';
import { ClassesComponent } from './webComponent/classes/classes.component';
import { CoachComponent } from './webComponent/classes/coach.component';
import { HomeComponent } from './webComponent/home/home.component';
import { MassageComponent } from './webComponent/massage/massage.component';
import {AuthGuard} from './Common/Guards/auth.guard';
import { ContactusComponent } from './webComponent/contactus/contactus.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'classes', component: ClassesComponent },
  { path: 'classes/:name', component: ClassDetailComponent },
  { path: 'massage', component: MassageComponent },
  {path:'contact', component:ContactusComponent },
  {path:'coach/:id',component:CoachComponent,canActivate:[AuthGuard]},
  {
    path: 'members',
    loadChildren: () =>
      import('./Members/members.module').then((module) => module.MembersModule),
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
