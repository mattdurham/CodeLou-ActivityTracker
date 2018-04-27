import { Injectable, Inject } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ActivityTrackerService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  // An observable is a stream of events that you can listen to.
  public createUser(): Observable<User> {
    return this.http
      .post<User>(
        this.baseUrl + 'api/Users',
        '',
        this.getAuthHeaders()
      );
  }

  public getMe(): Observable<User> {
    return this.http
      .get<User>(
        this.baseUrl + 'api/Users/Me',
        this.getAuthHeaders()
      );
  }

  private getAuthHeaders() {
    const accessToken = localStorage.getItem('access_token');
    const idToken = localStorage.getItem('id_token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + accessToken,
        'ID': idToken
      })
    };
    return httpOptions;
  }


}
