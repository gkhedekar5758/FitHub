import { Component,  OnInit  } from '@angular/core';
//import * as $ from 'jquery';

@Component({
  selector: 'FH-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  starrating='3';

  constructor() { }


  ngOnInit(): void {

    
  }

  onRatingChange = (val)=>{
    console.log(val);
  }


}
