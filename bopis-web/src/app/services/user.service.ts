import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  login(run: string, password: string): Observable<any>{

    let user: any = {

      run: run,
      password: password

    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/login';

    return this.httpClient.post(URL_USER, user, {headers: this.headers});
  }

  updatePasswordByEmailAndStatusEqualToOne(email: string): Observable<any>{

    let user: any = {

      email: email

    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/updatePasswordByEmailAndStatusEqualToOne';

    return this.httpClient.put(URL_USER, user, {headers: this.headers});
  }

  findAllCount(): Observable<any>{

    let URL_USER = this.URL_API + 'User/findAllCount';

    return this.httpClient.get(URL_USER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findAllPaged(page: Number, filter: String, sort: Number, column: String): Observable<any>{

    let URL_USER = '';

    if(filter == null){

      URL_USER = this.URL_API + 'User/findAllPaged/' + page  + '?sort=' + sort + '&column=' + column;

    }else{
      
      URL_USER = this.URL_API + 'User/findAllPaged/' + page  + '?filter=' + filter + '&sort=' + sort + '&column=' + column;

    }

    return this.httpClient.get(URL_USER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateStatusFindById(id: Number, status: Boolean): Observable<any>{

    let user: any = {

      id: id,
      status: status
    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/updateStatusFindById';

    return this.httpClient.put(URL_USER, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updatePasswordByIdAndStatusEqualToOne(id: Number, password: String): Observable<any>{

    let user: any = {

      id: id,
      password: password
    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/updatePasswordByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_USER, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updatebyIdAndStatusEqualToOne(id: Number, profileId: Number, localId: Number, fullName: String, run: String, email: String): Observable<any>{

    let user: any = {

      id: id,
      profileId: profileId,
      localId: localId,
      fullName: fullName,
      run: run,
      email: email
    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/updatebyIdAndStatusEqualToOne';

    return this.httpClient.put(URL_USER, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  create(profileId: Number, localId: Number, fullName: String, run: String, email: String, password: String): Observable<any>{

    let user: any = {

      profileId: profileId,
      localId: localId,
      fullName: fullName,
      run: run,
      email: email,
      password: password
    };

    user = JSON.stringify(user);

    let URL_USER = this.URL_API + 'User/create';

    return this.httpClient.post(URL_USER, user, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findAllStatusEqualToOne(): Observable<any>{

    let URL_USER = this.URL_API + 'User/findAllStatusEqualToOne';

    return this.httpClient.get(URL_USER, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

}
