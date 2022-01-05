import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { IClass } from '../../DataModels/class.model'

@Component({
  selector: 'FH-class-detail',
  templateUrl: './class-detail.component.html',
  styleUrls: ['./class-detail.component.css']
})
export class ClassDetailComponent implements OnInit {

  classDetail:IClass;
  nameOfClass:string;
  constructor(private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.forEach((param:Params) => {
      //this.nameOfClass=param['name'];
      this.classDetail=classDetailsArray.find(c => c.name == param['name']);
    })
    //this.classDetail=classDetailsArray.find(c => c.name == this.nameOfClass);
  }

}

const classDetailsArray :IClass[]=[
{
  name:'zumba',
  picture:'assets/images/class/zumba.jpg',
  description:'Zumba is an exercise fitness program that combines Latin Music and easy to follow dance step. Zumba involves dance and aerobic movements performed to energetic music. The choreography incorporates hip-hop, soca, samba, salsa, merengue and mambo. Squats and lunges are also included. '
},
{
  name:'Kick Boxing',
  picture:'assets/images/class/kickbox.jpg',
  description:'Kickboxing is a stand-up combat sport based on kicking and punching, historically developed from karate mixed with boxing. Kickboxing is practiced for self-defence, general fitness, or as a contact sport.'
},
{
  name:'Yoga',
  picture:'assets/images/class/yoga.jpg',
  description:'The term "Yoga" in the Western world often denotes a modern form of hatha yoga and yoga as exercise, consisting largely of the postures or asanas. The practice of yoga has been thought to date back to pre-vedic Indian traditions; possibly in the Indus valley civilization around 3000 BCE'
},
{
  name:'Aerobic',
  picture:'assets/images/class/aerobic.jpg',
  description:'Aerobic exercise (also known as cardio or cardio-respiratory exercise) is physical exercise of low to high intensity that depends primarily on the aerobic energy-generating process. "Aerobic" is defined as "relating to, involving, or requiring free oxygen", and refers to the use of oxygen to adequately meet energy demands during exercise via aerobic metabolism. Aerobic exercise is performed by repeating sequences of light-to-moderate intensity activities for extended periods of time.'
},
{
  name:'Calesthenics',
  picture:'assets/images/class/calesthenic.jpg',
  description:'Urban calisthenics is a form of street workout; calisthenics groups perform exercise routines in urban areas. Individuals and groups train to perform advanced calisthenics skills such as muscle-ups, levers, and various freestyle moves such as spins and flips.'
},
{
  name:'Body Building',
  picture:'assets/images/class/body.jpg',
  description:'Bodybuilding is the use of progressive resistance exercise to control and develop one\'s musculature (muscle building) by muscle hypertrophy for aesthetic purposes. It is distinct from similar activities such as powerlifting because it focuses on physical appearance instead of strength.'
},
{
  name:'Cross Fit',
  picture:'assets/images/class/crossfit.jpg',
  description:'CrossFit is promoted as both a physical exercise philosophy and a competitive fitness sport, incorporating elements from high-intensity interval training, Olympic weightlifting, plyometrics, powerlifting, gymnastics, girevoy sport, calisthenics, strongman, and other exercises.'
},
{
  name:'Weight Loss',
  picture:'assets/images/class/weightloss.jpg',
  description:'Your weight is a balancing act, but the equation is simple: If you eat more calories than you burn, you gain weight. And if you eat fewer calories and burn more calories through physical activity, you lose weight.'
}
];
