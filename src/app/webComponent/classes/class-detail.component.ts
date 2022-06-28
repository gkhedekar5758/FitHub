import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { IClass } from '../../DataModels/class.model';
import { ICoach } from '../../DataModels/coach.model';
import { CoachService } from '../../Common/Services/coach.service';
import { ClassService } from 'src/app/Common/Services/class.service';
import { mergeMap } from 'rxjs/operators';

@Component({
  selector: 'FH-class-detail',
  templateUrl: './class-detail.component.html',
  styleUrls: ['./class-detail.component.css'],
})
export class ClassDetailComponent implements OnInit {
  classes: IClass[];

  coaches: ICoach[];
  classDetail: IClass;

  constructor(
    private route: ActivatedRoute,
    private coachService: CoachService,
    private classService: ClassService
  ) {}

  ngOnInit(): void {
    
    this.getClassList().then(() => {
      this.route.params.forEach((param: Params) => {
        this.classDetail = this.classes.find(
          (c) => c.className === param['name']
        );

        this.populateCoachInformation(this.classDetail.classID);
      });
    });
  }

  getClassList = () => {
    return new Promise<void>((resolve) => {
      this.classService.getClasses().subscribe(
        (response) => {
          //console.log(response);
          this.classes = response;
          resolve();
        },
        (error) => {
          console.log(error);
        }
      );
    });
  };

  populateCoachInformation = (classID: number) => {
    //console.log("populatecoach called")
    this.coachService.getCoachesByClassID(classID).subscribe(
      (response) => {
        //console.log(response);
        this.coaches = response;
        //console.log(this.coaches);
      },
      (error) => console.log(error)
    );
  };
}
