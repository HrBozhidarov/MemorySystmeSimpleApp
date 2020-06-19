import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class IdentityService {
  private readonly registerUrl = URLS.DOMAIN_URL + 'identity/register';
  private readonly loginUrl = URLS.DOMAIN_URL + 'identity/login';

  constructor(private http: HttpClient) { }

  public register(payload: any): Observable<any> {
    return this.http.post(this.registerUrl, payload);
  }

  public login(payload: any): Observable<any> {
    return this.http.post(this.loginUrl, payload);
  }

  public saveToken(token: any) {
    localStorage.setItem("token", token);
  }

  public logout() {
    localStorage.removeItem("token");
  }

  public getToken(): any {
    return localStorage.getItem("token");
  }

  public isLoggedIn() {
    return this.getToken() != null;
  }
}
