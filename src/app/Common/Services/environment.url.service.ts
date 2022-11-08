import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {

  constructor() { }

  public URL_Switch: string = "DEBUG"; //DOCKER or DEBUG  //TODO: this is for debugging purpose only
  public apiURL: string = environment.baseURL;
  public apiURLDocker: string = environment.baseURLDocker;
}
