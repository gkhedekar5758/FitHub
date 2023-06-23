import { error, stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
//import { error } from 'console';
import { forkJoin } from 'rxjs';
import { CoachService } from 'src/app/Common/Services/coach.service';
import { TestimonyService } from 'src/app/Common/Services/testimony.service';
import { ITestimonyDTO } from 'src/app/DataModels/DTO/ITestimonyDTO';
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
  initialTestimony: string ;
  coachRating: string = '0';

  userTestimonyDTO: ITestimonyDTO;
  //userTestimony: string;

  constructor(
    private route: ActivatedRoute,
    private coachService: CoachService,
    private authService: AuthService,
    private testimonyService: TestimonyService
  ) { }

  ngOnInit(): void {
    this.currentLoggedUser = this.authService.getCurrentLoggedInUser();
    console.log(this.currentLoggedUser.userInfo);
    this.route.params.forEach((param) => {
      this.coachID = param[`id`];
    });

    //#region TRYFORKJOIN
    let coachResponse$ = this.coachService.getCoachByCoachID(this.coachID);
    let coachRating$ = this.coachService.getCoachRatingByUserID(
      this.coachID,
      this.currentLoggedUser.userID
    );
    let testiMony$ = this.testimonyService.getUserTestimony(this.currentLoggedUser.userID)

    forkJoin([coachResponse$, coachRating$, testiMony$]).subscribe((response) => {
      //console.log(response);
      this.coachResponse = response[0] as ICoachClassResponseDTO;
      this.ratingResponse = response[1] as Rating;
      this.userTestimonyDTO = response[2] as ITestimonyDTO;
      //console.log(response);
    });
    //#endregion
    //this.initialTestimony=this.userTestimonyDTO.testimony;
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
          this.coachService.getCoachRatingByUserID(
            this.coachID,
            this.currentLoggedUser.userID
          ).subscribe((res) => {
            this.ratingResponse = res;
          })
          alert('Rating added succesfully');
        },
        (error) => console.log(error)
      );
    } else if (this.ratingResponse.ratingID > 0) {
      this.coachService
        .updateCoachRatingByUser(
          this.coachID,
          this.currentLoggedUser.userID,
          rating
        )
        .subscribe(
          () => {
            alert('rating updated succesfully');
          },
          (error) => console.log(error)
        );
    }
  };

  SubmitTestimony = () => {

    if (this.userTestimonyDTO.testimonyID === 0) {
      var testimony: ITestimonyDTO = {
        testimonyID: 0,
        testimony: this.userTestimonyDTO.testimony,
        userID: this.currentLoggedUser.userID
      }
      this.testimonyService.createUserTestimony(testimony)
        .subscribe((response: ITestimonyDTO) => {
          this.userTestimonyDTO = response;
          alert("testimony added")
        }, (error) => {
          console.log(error)
        })
    } else if (this.userTestimonyDTO.testimonyID > 0 ) {
      
      var testimony: ITestimonyDTO = {
        testimonyID: this.userTestimonyDTO.testimonyID,
        testimony: this.userTestimonyDTO.testimony,
        userID: this.currentLoggedUser.userID
      }
      this.testimonyService.modifyTestimony(testimony)
        .subscribe(() => alert("Updated Succefully")
          , error => console.log(error))
    }

  };
}
