import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  getOrdersFromUser(): Observable<object> {
    return this.httpClient.get(this.baseUrl + 'orders');
  }

  getOrderDetailed(id: number): Observable<object> {
    return this.httpClient.get(this.baseUrl + 'orders/' + id);
  }
}
