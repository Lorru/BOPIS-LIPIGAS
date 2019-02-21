import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findAllCount(): Observable<any>{

    let URL_LOCAL = this.URL_API + 'Local/findAllCount';

    return this.httpClient.get(URL_LOCAL, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findByStatusEqualToOneCount(): Observable<any>{

    let URL_LOCAL = this.URL_API + 'Local/findByStatusEqualToOneCount';

    return this.httpClient.get(URL_LOCAL, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateOpenFindByIdAndStatusEqualToOne(userId: Number, id: Number, open: Boolean): Observable<any>{

    let user: any = {

      id: userId,
      local:{
        id: id,
        open: open
      }
    };

    user = JSON.stringify(user);

    let URL_LOCAL = this.URL_API + 'Local/updateOpenFindByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_LOCAL, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findAllPaged(page: Number, filter: String, sort: Number, column: String): Observable<any>{

    let URL_LOCAL = '';

    if(filter == null){

      URL_LOCAL = this.URL_API + 'Local/findAllPaged/' + page  + '?sort=' + sort + '&column=' + column;

    }else{
      
      URL_LOCAL = this.URL_API + 'Local/findAllPaged/' + page  + '?filter=' + filter + '&sort=' + sort + '&column=' + column;

    }

    return this.httpClient.get(URL_LOCAL, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateStatusFindById(userId: Number, id: Number, status: Boolean): Observable<any>{

    let user: any = {

      id: userId,
      local:{
        id: id,
        status: status
      }
    };

    user = JSON.stringify(user);

    let URL_LOCAL = this.URL_API + 'Local/updateStatusFindById';

    return this.httpClient.put(URL_LOCAL, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateByIdAndStatusEqualToOne(userId: Number, id: Number, latitude: Number, length: Number, name: String, radio: Number): Observable<any>{

    let user: any = {

      id: userId,
      local:{
        id: id,
        latitude: latitude,
        length: length,
        name: name,
        radio: radio
      }
    };

    user = JSON.stringify(user);

    let URL_LOCAL = this.URL_API + 'Local/updateByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_LOCAL, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  create(userId: Number, latitude: Number, length: Number, name: String, radio: Number): Observable<any>{

    let user: any = {

      id: userId,
      local:{
        latitude: latitude,
        length: length,
        name: name,
        radio: radio
      }
    };

    user = JSON.stringify(user);

    let URL_LOCAL = this.URL_API + 'Local/create';

    return this.httpClient.post(URL_LOCAL, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findAllStatusEqualToOne(): Observable<any>{

    let URL_LOCAL = this.URL_API + 'Local/findAllStatusEqualToOne';

    return this.httpClient.get(URL_LOCAL, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
