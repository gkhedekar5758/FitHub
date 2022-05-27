import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoachService } from 'src/app/Common/Services/coach.service';
import { ICoachClassResponseDTO } from 'src/app/DataModels/DTO/ResponseDTO/ICoachClassResponseDTO';

@Component({
  selector: 'FH-coach',
  templateUrl: './coach.component.html',
  styleUrls: ['./coach.component.css']
})
export class CoachComponent implements OnInit {

  coachResponse:ICoachClassResponseDTO;
  coachID:number;

  constructor(private route:ActivatedRoute,private coachService:CoachService) { }

  ngOnInit(): void {

    this.route.params.forEach((param)=>{
      this.coachID=param[`id`];
    })

    this.getCoachForInit().then(()=>{

    }
    )
  }

  getCoachForInit =()=>{
    return new Promise<void>((resolve) =>{

        this.coachService.getCoachByCoachID(this.coachID)
        .subscribe((response)=>{
          this.coachResponse=response;
          resolve();
        },
        error => {
          console.log(error);
        }
        )

    })
  }
}
