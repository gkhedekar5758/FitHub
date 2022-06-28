import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CoachService } from 'src/app/Common/Services/coach.service';
import { TestimonyService } from 'src/app/Common/Services/testimony.service';
import { ICoachClassResponseDTO } from 'src/app/DataModels/DTO/ResponseDTO/ICoachClassResponseDTO';
import { Rating } from 'src/app/DataModels/rating.model';
import { IUser } from 'src/app/Members/Models/IUser';
import { AuthService } from 'src/app/Members/Services/auth.service';

@Component({
  selector: 'FH-coach',
  templateUrl: './coach.component.html',
  styleUrls: ['./coach.component.css'],
})
export class CoachComponent implements OnInit {
  coachResponse: ICoachClassResponseDTO;
  coachID: number;
  currentLoggedUser: IUser;
  ratingResponse: Rating;
  initialCoachRating: string = '0';
  coachRating: string = '0';

  //UserTestimony:string="";

  constructor(
    private route: ActivatedRoute,
    private coachService: CoachService,
    private authService: AuthService,
    private testimonyService: TestimonyService
  ) {}

  ngOnInit(): void {
    this.currentLoggedUser = this.authService.getCurrentLoggedInUser();
    this.route.params.forEach((param) => {
      this.coachID = param[`id`];
    });

    //#region TRYFORKJOIN
    let coachResponse$ = this.coachService.getCoachByCoachID(this.coachID);
    let coachRating$ = this.coachService.getCoachRatingByUserID(
      this.coachID,
      this.currentLoggedUser.userID
    );
    //let testiMony$= this.testimonyService.getUserTestimony(this.currentLoggedUser.userID)

    forkJoin([coachResponse$, coachRating$]).subscribe((response) => {
      //console.log(response);
      this.coachResponse = response[0] as ICoachClassResponseDTO;
      this.ratingResponse = response[1] as Rating;
      //console.log(typeof( this.coachRating));
      // if(this.initialCoachRating===null)
      //   this.initialCoachRating='0';
      //console.log(response);
      //this.UserTestimony=String(response[2]);
    });
    //#endregion
  }
  changeRating = (value) => {
    this.coachRating = value;
    console.log(this.coachRating);
    const rating: Rating = {
      ratingID: this.ratingResponse.ratingID,
      coachID: this.coachID,
      userID: this.currentLoggedUser.userID,
      ratingValue: this.coachRating,
    };
    if (this.ratingResponse.ratingID === 0) {
      //User did change the rating from nothing to something
      this.coachService.addCoachRatingByUser(rating).subscribe(
        () => {
          alert('Rating added succesfully');
        },
        (error) => console.log(error)
      );
    } else if (this.ratingResponse.ratingID > 0) {
      //TODO- PUT request here
    }
  };

  SubmitReviewandTestimony = () => {
    //https://cassiomolin.com/2021/07/29/should-http-put-create-a-resource-if-it-does-not-exist/
    //https://stackoverflow.com/questions/56240547/should-http-put-create-a-resource-if-it-does-not-exist
    // the resource identifier is creted by server so i should create the resource with POST
    // and update with PUT
  };
}
