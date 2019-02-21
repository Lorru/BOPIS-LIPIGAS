import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderStatusService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findAllStatusEqualToOne(): Observable<any>{

    let URL_ORDER_STATUS = this.URL_API + 'OrderStatus/findAllStatusEqualToOne';

    return this.httpClient.get(URL_ORDER_STATUS, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
