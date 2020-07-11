import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class MemoryService {
  private readonly createUrl = URLS.DOMAIN_URL + 'pictures/create';
  private readonly myMemoriesUrl = URLS.DOMAIN_URL + 'users/myMemories';

  constructor(private http: HttpClient) { }

  public create(payload: any): Observable<any> {
    return this.http.post(this.createUrl, payload);
  }

  public myMemories(): Observable<any> {
    return this.http.get(this.myMemoriesUrl);
  }
}
