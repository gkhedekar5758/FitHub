import { error, stringify } from '@angular/compiler/src/util';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
//import { error } from 'console';
import { Observable, forkJoin } from 'rxjs';
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

  userTestimonyDTO: ITestimonyDTO=null;
  datafromServer:boolean=false;
  //userTestimony: string;

  constructor(
    private route: ActivatedRoute,
    private coachService: CoachService,
    private authService: AuthService,
    private testimonyService: TestimonyService
  ) { }

  
  ngOnInit(): void {
    this.currentLoggedUser = this.authService.getCurrentLoggedInUser();
    //console.log(this.currentLoggedUser.userInfo);
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

    forkJoin([coachResponse$, coachRating$, testiMony$])
    .subscribe((response) => {
      this.coachResponse = response[0] as ICoachClassResponseDTO;
      this.ratingResponse = response[1] as Rating;
      this.userTestimonyDTO = response[2] as ITestimonyDTO;
      console.log(response);
    },(error)=>console.log(error),
    ()=>{
      this.datafromServer=true;
      //This means that the forkjoin has completed fetching
      //all the responses now we can show html code
    });

    //console.log(this.userTestimonyDTO);
  }

  changeRating = (value) => {
    this.coachRating = value.Rating;
    //console.log(value);
    const rating: Rating = {
      ratingID: this.ratingResponse.ratingID||0,
      coachID: this.coachID,
      userID: this.currentLoggedUser.userID,
      ratingValue: this.coachRating,
    };
    if (rating.ratingID === 0) {
      //User did change the rating from nothing to something
      this.coachService.addCoachRatingByUser(rating).subscribe(
        () => {
          // this.coachService.getCoachRatingByUserID(
          //   this.coachID,
          //   this.currentLoggedUser.userID
          // ).subscribe((res) => {
          //   this.ratingResponse = res;
          // })
          alert('Rating added succesfully');
        },
        (error) => console.log(error)
      );
    } else if (rating.ratingID > 0) {
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

  SubmitTestimony = (value:NgForm) => {
    var testimony: ITestimonyDTO = {
      testimonyID: this.userTestimonyDTO.testimonyID,
      testimony: value.form.controls.txtTestimony.value ,
      userID: this.currentLoggedUser.userID
    }
    if (testimony.testimonyID === undefined ) {
      //creating testimony
      
      this.testimonyService.createUserTestimony(testimony)
        .subscribe((response: ITestimonyDTO) => {
          this.userTestimonyDTO = response;
          alert("testimony added")
        }, (error) => {
          console.log(error)
        })
    } else if (testimony.testimonyID > 0 ) {
      //updating testimony
      // var testimony: ITestimonyDTO = {
      //   testimonyID: this.userTestimonyDTO.testimonyID,
      //   testimony: value.form.controls.txtTestimony.value,
      //   userID: this.currentLoggedUser.userID
      // }
      this.testimonyService.modifyTestimony(testimony)
        .subscribe(() => alert("Updated Succefully")
          , error => console.log(error))
    }

  };
}
