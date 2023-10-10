import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from 'src/app/Members/Services/auth.service';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class FitHubHttpInterceptorInterceptor implements HttpInterceptor {

  constructor(private _authService: AuthService, private _router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   // console.log('interceptor fired')
    // if(this._authService.isTokenExpired()){
    //   this._authService.logout();
    //   alert('Token Expired !!');
    //   this._router.navigate(['../members/login'])
    //   //return;
    // }
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            //client side error
            alert(`Error : ${error.error.message}`);
          } else {
            //server side error
            errorMessage = `Error code : ${error.status} \n Message : ${error.message}`
            if (error.status === 404) {
              //alert('The server is not found, please try again later');
              errorMessage = 'The server is not found, please try again later';
              //console.log(error);

            }
            if (error.status === 500) {
              //alert('There was a technical issue on our side, please try again later');

              errorMessage = 'There was a technical issue on our side, please try again later';
            }
            if (error.status === 400) {
              errorMessage = error.error.errorMessage == undefined ? `There was a mistake on your end. Request was not correct` : `There was a mistake on your end. ${error.error.errorMessage}`;
            }
            if (error.status === 401) {
              //alert ('You need to be logged in to see the page')
              console.log(error.error.errorMessage);

              errorMessage = error.error.errorMessage;
              //this._router.navigate(['../members/login']);
            }
          }
          return throwError(errorMessage);
        })
      )
  }
}
