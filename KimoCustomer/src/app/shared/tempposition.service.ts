import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TemppositionService {

  constructor(private http:HttpClient) { }

  getPositionList(){
    return this.http.get(environment.apiURL +'/Template_Position').toPromise();
   }
   getJobTypeList(){
    return this.http.get(environment.apiURL +'/Job_Type').toPromise();
   }
}
