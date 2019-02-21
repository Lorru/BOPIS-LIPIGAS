import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CylinderByLocalService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  updateStatusFindById(id: Number, status: Boolean): Observable<any>{

    let cylinderByLocal: any = {

      id: id,
      status: status

    };

    cylinderByLocal = JSON.stringify(cylinderByLocal);

    let URL_CYLINDER_BY_LOCAL = this.URL_API + 'CylinderByLocal/updateStatusFindById';

    return this.httpClient.put(URL_CYLINDER_BY_LOCAL, cylinderByLocal, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByLocalIdAndStatusEqualToOne(localId: Number): Observable<any>{

    let URL_CYLINDER_BY_LOCAL = this.URL_API + 'CylinderByLocal/findByLocalIdAndStatusEqualToOne/' + localId;

    return this.httpClient.get(URL_CYLINDER_BY_LOCAL, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateByIdAndStatusEqualToOne(id: Number, zonePrice: Number, discount: Number, finalPrice: Number): Observable<any>{

    let cylinderByLocal: any = {

      id: id,
      zonePrice: zonePrice,
      discount: discount,
      finalPrice: finalPrice

    };

    cylinderByLocal = JSON.stringify(cylinderByLocal);

    let URL_CYLINDER_BY_LOCAL = this.URL_API + 'CylinderByLocal/updateByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_CYLINDER_BY_LOCAL, cylinderByLocal, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
