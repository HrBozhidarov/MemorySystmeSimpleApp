import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class IdentityService {
  private readonly domainUrl = 'https://localhost:44353/';
  private readonly registerUrl = this.domainUrl + 'identity/register';
  private readonly loginUrl = this.domainUrl + 'identity/login';

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

  public getToken(): any {
    return localStorage.getItem("token");
  }

  public isLoggedIn() {
    return this.getToken() != null;
  }
}
