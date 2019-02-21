import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScheduleOfAttentionService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findByLocalIdAndStatusEqualToOne(localId: Number): Observable<any>{

    let URL_SCHEDULE_OF_ATTENTION = this.URL_API + 'ScheduleOfAttention/findByLocalIdAndStatusEqualToOne/' + localId;

    return this.httpClient.get(URL_SCHEDULE_OF_ATTENTION, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateStartAndFinishFindByIdAndStatusEqualToOne(id: Number, start: String, finish: String): Observable<any>{

    let scheduleOfAttention: any = {

      id: id,
      start: start,
      finish: finish

    };

    scheduleOfAttention = JSON.stringify(scheduleOfAttention);

    let URL_SCHEDULE_OF_ATTENTION = this.URL_API + 'ScheduleOfAttention/updateStartAndFinishFindByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_SCHEDULE_OF_ATTENTION, scheduleOfAttention, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
