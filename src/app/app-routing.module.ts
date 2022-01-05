import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClassDetailComponent } from './webComponent/classes/class-detail.component';
import { ClassesComponent } from './webComponent/classes/classes.component';
import { HomeComponent } from './webComponent/home/home.component';
import { MassageComponent } from './webComponent/massage/massage.component';



const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'classes', component: ClassesComponent },
  { path: 'classes/:name', component: ClassDetailComponent },
  { path: 'massage', component: MassageComponent },
  {path:'members', loadChildren:()=>import('./Members/members.module').then(module=>module.MembersModule)},
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),

  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
