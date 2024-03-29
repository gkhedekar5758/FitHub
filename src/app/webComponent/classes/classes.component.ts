import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import {ClassService} from 'src/app/Common/Services/class.service'
import{IClass} from 'src/app/DataModels/class.model'

@Component({
  selector: 'FH-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.css']
})
export class ClassesComponent implements OnInit {

  classes:IClass[];

  constructor(private classService:ClassService,private _router:Router) { }

  ngOnInit(): void {
    //WORKING CODE START
    this.classService.getClasses()
    .subscribe(response => {
      //console.log(response)
      this.classes=response;
    },
    error=>{
      console.log(error);
      //alert("something bad");
    })

    //WORKING CODE END

  }


}
