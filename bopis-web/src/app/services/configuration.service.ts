import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  private headers = new HttpHeaders().set('Content-Type','application/json');
  private URL_API : string = environment.URL_API;

  constructor(private httpClient: HttpClient) { }

  findByIdAndStatusEqualToOne(id: Number): Observable<any>{

    let URL_CONFIGURATION = this.URL_API + 'Configuration/findByIdAndStatusEqualToOne/' + id;

    return this.httpClient.get(URL_CONFIGURATION, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  findAllPaged(page: Number, sort: Number, column: String): Observable<any>{

    let URL_CONFIGURATION = this.URL_API + 'Configuration/findAllPaged/' + page + '?sort=' + sort + '&column=' + column;

    return this.httpClient.get(URL_CONFIGURATION, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }

  updateValueAndReadOnlyByIdAndStatusEqualToOne(id: Number, value: String, readOnly: Boolean): Observable<any>{

    let configuration: any = {

      id: id,
      value: value,
      readOnly: readOnly
    };

    configuration = JSON.stringify(configuration);

    let URL_CONFIGURATION = this.URL_API + 'Configuration/updateValueAndReadOnlyByIdAndStatusEqualToOne';

    return this.httpClient.put(URL_CONFIGURATION, configuration, {headers: this.headers.append('Authorization', localStorage.getItem('token'))});
  }
}
