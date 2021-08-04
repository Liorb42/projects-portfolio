import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { LogInResModel } from '../models/login-response.model';
import { RegisterResModel } from '../models/register-response.model';

@Injectable({
  providedIn: 'root',
})
export class GameHttpService {
  // private baseUrl = 'https://game-service-sela-soa-project.azurewebsites.net';
  private baseUrl = 'http://localhost:5000';

  private headers = new HttpHeaders({
    'content-type': 'application/json',
  });
  private data: any;
  private errorMessage: String;

  constructor(private http: HttpClient) {}

  loginPlayer(name: string, password: string): Observable<LogInResModel> {
    const body = JSON.parse(JSON.stringify({ name: name, password: password }));

    return this.http
      .post<LogInResModel>(`${this.baseUrl}/logIn`, body, {
        headers: this.headers,
      })
      .pipe(map((res) => <LogInResModel>res));
  }

  registerPlayer(
    name: string,
    email: String,
    password: string
  ): Observable<RegisterResModel> {
    const body = JSON.stringify({
      name: name,
      email: email,
      password: password,
    });
    return this.http
      .put<RegisterResModel>(`${this.baseUrl}/register`, body, {
        headers: this.headers,
      })
      .pipe(map((res) => <RegisterResModel>res));
  }
}
