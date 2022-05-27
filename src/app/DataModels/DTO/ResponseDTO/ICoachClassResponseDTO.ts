import {IClass} from '../../class.model';

export interface ICoachClassResponseDTO{
  coachID:number,
  coachName:string,
  degree:string,
  photoURL:string,
  classes:IClass[];
}
