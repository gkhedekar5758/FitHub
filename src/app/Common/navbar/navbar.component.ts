import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Members/Services/auth.service';

@Component({
  selector: 'FH-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {


  constructor(public authService:AuthService,private routerService:Router) { }

  ngOnInit(): void {

  }

  public logout=()=>{
    this.authService.logout();
    this.routerService.navigate(['/']);
  }

}
