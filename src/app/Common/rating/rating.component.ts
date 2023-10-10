import { Component,  EventEmitter,  Input,  OnInit, Output  } from '@angular/core';
//import * as $ from 'jquery';

@Component({
  selector: 'FH-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  ngOnInit(): void {
    this.selectedRating[this.componentID]=this.starrating;
    //console.log(this.starrating);
    //console.log(this.selectedRating)
  }

  @Input() starrating:string='';
  @Input() componentID:number=0; // this will be the id of the coach or anything else with which this is connected
  @Output() rated=new EventEmitter<any>();

  //starrating='3';
  selectedRating:{
    [componentID:string]:string
  }={};

  constructor() { }


 
  onRatingChanges = (componentID:number,rating:string)=>{
    this.selectedRating[componentID]=rating;
    this.rated.emit({Rating:this.selectedRating[componentID],ID:componentID});
  }


}
