import { Component, OnInit } from '@angular/core';
import { IUser } from '../../Models/IUser';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop'
import { Rating } from 'src/app/DataModels/rating.model';
import { AuthService } from '../../Services/auth.service';
import { IClass } from 'src/app/DataModels/class.model';
import { ClassService } from 'src/app/Common/Services/class.service';

@Component({
  selector: 'FH-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {

  //user information
  loggedInUser:IUser;
  //as of now hard coding these and then we wil get it from DB
  availableClasses: IClass[];
  //[
  //   {name:'Zumba',Price:340},
  // {name:'Kick Boxing',Price:250},
  // {name:'Calesthenics',Price:780},{name:'Aerobic',Price:450},{name:'Yoga',Price:450}];
    enrolledClasses: any[] = [];
    ratingResponse:Rating;
  
  constructor(private authService:AuthService,private classService:ClassService) { }

  ngOnInit(): void {
    this.loggedInUser=this.authService.getCurrentLoggedInUser();
    this.classService.getClasses()
    .subscribe(response=>{
      this.availableClasses=response;
      console.log(this.availableClasses);
    }
      
    );
    
  }

  drop = (event: CdkDragDrop<string[]>) => {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      let price=event.container.data[0];

      console.log(price);
    }
    else {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
      console.log(event.container.data[0]);
    }
  }
  //TODO: implement this
  changeRating(value){}
}
