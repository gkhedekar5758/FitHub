import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Common/navbar/navbar.component';
import { FooterComponent } from './Common/footer/footer.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './webComponent/home/home.component';
import { ClassesComponent } from './webComponent/classes/classes.component';
import { ClassDetailComponent } from './webComponent/classes/class-detail.component';
import { MassageComponent } from './webComponent/massage/massage.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { GoogleLoginProvider, SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { CoachComponent } from './webComponent/classes/coach.component';
import { ContactusComponent } from './webComponent/contactus/contactus.component';
import { FitHubHttpInterceptorInterceptor } from './Common/Interceptors/fit-hub-http-interceptor.interceptor';
import { SharedComponentModule } from './Common/shared-component.module';
import { CommonModule } from '@angular/common';



export function getJWTToken() {

  return localStorage.getItem("JWTToken")?.slice(1, -1); // this needs to be done token was going with " at start and end
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
    ContactusComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: getJWTToken,
        allowedDomains: ["localhost:5000"],
        disallowedRoutes: []
      }
    }),
    SocialLoginModule,
    SharedComponentModule

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: FitHubHttpInterceptorInterceptor,
      multi: true
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autologin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('415339241205-os8kum8al4q1r5csp0gpcq110vcjnv44.apps.googleusercontent.com')
          },
        ],
      } as SocialAuthServiceConfig


    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
