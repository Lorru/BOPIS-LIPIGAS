import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  updateQuantityByIdAndStatusEqualToOne(id: Number, quantity: Number): Observable<any>{

    let stock: any = {

      id: id,
      quantity: quantity

    };

    stock = JSON.stringify(stock);

    let URL_STOCK = this.URL_API + 'Stock/updateQuantityByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_STOCK, stock, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByLocalId(localId: Number): Observable<any>{

    let URL_STOCK = this.URL_API + 'Stock/findByLocalId/' + localId;

    return this.httpClient.get(URL_STOCK, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByCylinderByLocalIdAndStatusEqualToOne(cylinderByLocalId: Number): Observable<any>{

    let URL_STOCK = this.URL_API + 'Stock/findByCylinderByLocalIdAndStatusEqualToOne/' + cylinderByLocalId;

    return this.httpClient.get(URL_STOCK, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
