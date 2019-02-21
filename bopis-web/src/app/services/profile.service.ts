import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findByAllStatusEqualToOne(): Observable<any>{

    let URL_PROFILE = this.URL_API + 'Profile/findByAllStatusEqualToOne';

    return this.httpClient.get(URL_PROFILE, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
