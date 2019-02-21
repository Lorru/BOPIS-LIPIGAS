import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findByOrderStatusIdCount(): Observable<any>{

    let URL_ORDER = this.URL_API + 'Order/findByOrderStatusIdCount';

    return this.httpClient.get(URL_ORDER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByOrderStatusIdAndLocalIdCount(localId: Number): Observable<any>{

    let URL_ORDER = this.URL_API + 'Order/findByOrderStatusIdAndLocalIdCount/' + localId;

    return this.httpClient.get(URL_ORDER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByLocalIdPaged(page: Number, localId: Number, filter: String, sort: Number, column: String): Observable<any>{

    let URL_ORDER = '';

    if(filter == null){

      URL_ORDER = this.URL_API + 'Order/findByLocalIdPaged/' + page + '/' + localId + '?sort=' + sort + '&column=' + column;

    }else{
      
      URL_ORDER = this.URL_API + 'Order/findByLocalIdPaged/' + page + '/' + localId + '?filter=' + filter + '&sort=' + sort + '&column=' + column;

    }

    return this.httpClient.get(URL_ORDER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateById(orderId: Number, userId: Number): Observable<any>{

    let order: any = {

      id: orderId,
      userId: userId

    };

    order = JSON.stringify(order);

    let URL_ORDER = this.URL_API + 'Order/updateById';

    return this.httpClient.put(URL_ORDER, order, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  deleteById(id: Number): Observable<any>{

    let URL_ORDER = this.URL_API + 'Order/deleteById/' + id;

    return this.httpClient.delete(URL_ORDER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByIdAndClientRunPaged(page: Number, id: Number, clientRun: String, sort: Number, column: String): Observable<any>{

    let URL_ORDER = '';

    if(clientRun == null){

      URL_ORDER = this.URL_API + 'Order/findByIdAndClientRunPaged/' + page + '?id=' + id + '&sort=' + sort + '&column=' + column;

    }else{
      
      URL_ORDER = this.URL_API + 'Order/findByIdAndClientRunPaged/' + page + '?id=' + id + '&clientRun=' + clientRun + '&sort=' + sort + '&column=' + column;

    }

    return this.httpClient.get(URL_ORDER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  create(clientAddress: String, clientFullName: String, clientRun: String, cylinderbyLocalId: Number, quantity: Number, userId: Number): Observable<any>{

    let order: any = {

      clientAddress: clientAddress,
      clientFullName: clientFullName,
      clientRun: clientRun,
      cylinderbyLocalId: cylinderbyLocalId,
      quantity: quantity,
      userId: userId

    };

    order = JSON.stringify(order);

    let URL_ORDER = this.URL_API + 'Order/create';

    return this.httpClient.post(URL_ORDER, order, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

}
