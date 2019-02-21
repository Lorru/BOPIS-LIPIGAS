import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(dateStart: String, dateFinish: String, localId: String, userId: String, orderStatusId: String): Observable<any>{

    let URL_USER = this.URL_API + 'Report/findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId/' + dateStart + '/' + dateFinish + '/' + localId + '/' + userId + '/' + orderStatusId;

    return this.httpClient.get(URL_USER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(dateStart: String, dateFinish: String, localId: String, userId: String, orderStatusId: String): Observable<any>{

    let URL_USER = this.URL_API + 'Report/findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel/' + dateStart + '/' + dateFinish + '/' + localId + '/' + userId + '/' + orderStatusId;

    return this.httpClient.get(URL_USER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
