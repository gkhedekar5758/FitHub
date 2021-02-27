import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'FH-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent implements OnInit {
  subscribeTxtEmail: string;
  constructor() {}

  ngOnInit(): void {}

  subscribeNewsLetter(): void {
    //console.log('i am in');
    alert("Your email is added. Check the newsletter weekly in your inbox");
    //TODO: do something with email id
    this.subscribeTxtEmail="";
  }
}
