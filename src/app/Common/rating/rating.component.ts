import { Component,  EventEmitter,  Input,  OnInit, Output  } from '@angular/core';
//import * as $ from 'jquery';

@Component({
  selector: 'FH-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  @Input() starrating:string;
  @Output() rated=new EventEmitter<string>();

  //starrating='3';

  constructor() { }


  ngOnInit(): void {
    

  }

  onRatingChange = (val)=>{
    this.rated.emit(val);
  }


}
