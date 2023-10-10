import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/Members/Services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService:AuthService,private router:Router) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      //console.log('auth guard fired')
      if(!this.authService.isUserAuthenticated()){
        this.authService.logout();
        alert("You need to be logged in to visit the page.")
        this.router.navigate(['../members/login'],{queryParams:{returnUrl:state.url}});

        return false;
      }
    return true;
  }

}
