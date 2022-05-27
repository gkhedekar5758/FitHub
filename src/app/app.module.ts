import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Common/navbar/navbar.component';
import { FooterComponent } from './Common/footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './webComponent/home/home.component';
import { ClassesComponent } from './webComponent/classes/classes.component';
import { ClassDetailComponent } from './webComponent/classes/class-detail.component';
import { MassageComponent } from './webComponent/massage/massage.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import{JwtHelperService,JwtModule} from '@auth0/angular-jwt';
import { GoogleLoginProvider,  SocialLoginModule,SocialAuthServiceConfig } from 'angularx-social-login';
import { CoachComponent } from './webComponent/classes/coach.component';
import { RatingComponent } from './Common/rating/rating.component';



export function getJWTToken(){
  return localStorage.getItem("JWTToken");
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    ClassesComponent,
    ClassDetailComponent,
    MassageComponent,
    CoachComponent,
    RatingComponent,
    //FieldMatcherDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:getJWTToken,
        allowedDomains:["localhost:5000"],
        disallowedRoutes:[]
      }
    }),
    SocialLoginModule

  ],
  providers: [
    //SocialAuthService
    {
      provide:'SocialAuthServiceConfig',
      useValue:{
        autologin:false,
        providers:[
          {
            id:GoogleLoginProvider.PROVIDER_ID,
            provider:new GoogleLoginProvider('415339241205-os8kum8al4q1r5csp0gpcq110vcjnv44.apps.googleusercontent.com')
          },
        ],
      } as SocialAuthServiceConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
