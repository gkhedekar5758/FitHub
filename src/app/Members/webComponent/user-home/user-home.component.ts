import { Component, OnInit } from '@angular/core';
import { IUser } from '../../Models/IUser';
import {
  CdkDrag,
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Rating } from 'src/app/DataModels/rating.model';
import { AuthService } from '../../Services/auth.service';
import { IClass } from 'src/app/DataModels/class.model';
import { ClassService } from 'src/app/Common/Services/class.service';
import { ICoachRatingResponseDTO } from '../../Models/DTO/ResponseDTO/ICoachRatingResponseDTO';
import { CoachService } from 'src/app/Common/Services/coach.service';
import { forkJoin } from 'rxjs';
import { ITestimonyDTO } from 'src/app/DataModels/DTO/ITestimonyDTO';
import { TestimonyService } from 'src/app/Common/Services/testimony.service';

@Component({
  selector: 'FH-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css'],
})
export class UserHomeComponent implements OnInit {
  //user information
  loggedInUser: IUser;

  availableClasses: IClass[];
  enrolledClasses: IClass[] = [];
  coachRatings: ICoachRatingResponseDTO[];
  ratingResponse: Rating;
  userTestimonyDTO: ITestimonyDTO;

  gymTotalPrice: number = 0; // total gym membership cost
  classesTotalPrice: number = 0; // all classes membership cost
  datafromServer: boolean = false;

  //coachRating:string;
  constructor(
    private authService: AuthService,
    private classService: ClassService,
    private coachService: CoachService,
    private testimonyService: TestimonyService
  ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authService.getCurrentLoggedInUser();
    let classResponse$ = this.classService.getClasses();
    let coachRating$ = this.coachService.getAllCoachRatingByUser(
      this.loggedInUser.userID
    );
    let userTestimony$ = this.testimonyService.getUserTestimony(
      this.loggedInUser.userID
    );

    forkJoin([classResponse$, coachRating$, userTestimony$]).subscribe(
      (resp) => {
        this.availableClasses = resp[0] as IClass[];
        this.coachRatings = resp[1] as ICoachRatingResponseDTO[];
        this.userTestimonyDTO = resp[2] as ITestimonyDTO;
        console.log(this.coachRatings);
      },
      (error) => console.log(error),
      () => {
        this.datafromServer = true;
      }
    );
  }

  drop = (event: CdkDragDrop<IClass[]>) => {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      let price = event.container.data[0].pricePerSession;

      //console.log(price);
      //console.log(event)
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      // this is the place where the movement happens
      //from select to deselect and vice versa,
      console.log(event.currentIndex);
      let price = event.container.data[event.currentIndex].pricePerSession;
      // console.log(price);
      // console.log(this.classesTotalPrice);
      this.classesTotalPrice = +this.classesTotalPrice + +price;
    }
  };

  //once class is selected you can't go back
  noReturnPredicate() {
    return false;
  }

  Reload() {
    window.location.reload();
  }
  changeRating(value) {
    let ratingToUpdate: Rating;
    let coachRating = value.Rating;
    let coachID = value.ID;
    //first we will have to make call to get the rating id of this
    //coach so that we can check if it is update or create
    this.coachService
      .getCoachRatingByUserID(coachID, this.loggedInUser.userID)
      .subscribe(
        (response) => {
          this.ratingResponse = response as Rating;
          console.log(response);
        },
        (error) => {
          console.log(error);
        },
        () => {
          ratingToUpdate = {
            ratingID: this.ratingResponse.ratingID||0,
            coachID: coachID,
            userID: this.loggedInUser.userID,
            ratingValue: coachRating,
          };
          //now we have answer from rating
          //observable so let's go to filing part
          if (ratingToUpdate.ratingID === 0) {
            this.coachService
              .addCoachRatingByUser(ratingToUpdate)
              .subscribe(() => alert('rating added Succefully'));
          } else if (ratingToUpdate.ratingID > 0) {
            this.coachService
              .updateCoachRatingByUser(
                this.ratingResponse.coachID,
                this.loggedInUser.userID,
                ratingToUpdate
              )
              .subscribe(() => alert('rating updated successfully'));
          }
        }
      );
  }

  addGymMembership(value) {
    // console.log(value.currentTarget.getAttribute('value'));
    let priceOfMembership = value.currentTarget.getAttribute('value');
    // console.log(typeof(priceOfMembership));
    // console.log(typeof(this.gymTotalPrice))
    this.gymTotalPrice = +this.gymTotalPrice + +priceOfMembership;
  }
  SubmitTestimony(value) {
    if (this.userTestimonyDTO.testimonyID === 0) {
      var testimony: ITestimonyDTO = {
        testimonyID: 0,
        testimony: value.form.controls.txtTestimony.value,
        userID: this.loggedInUser.userID,
      };
      this.testimonyService.createUserTestimony(testimony).subscribe(
        (response: ITestimonyDTO) => {
          this.userTestimonyDTO = response;
          alert('testimony added');
        },
        (error) => {
          console.log(error);
        }
      );
    } else if (this.userTestimonyDTO.testimonyID > 0) {
      var testimony: ITestimonyDTO = {
        testimonyID: this.userTestimonyDTO.testimonyID,
        testimony: value.form.controls.txtTestimony.value,
        userID: this.loggedInUser.userID,
      };
      this.testimonyService.modifyTestimony(testimony).subscribe(
        () => alert('Updated Succefully'),
        (error) => console.log(error)
      );
    }
  }
}
