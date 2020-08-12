import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IPagination } from '../shared/models/pagination';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private baseUrl = 'https://localhost:44387/api/';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<IPagination>{
    return this.http.get<IPagination>(this.baseUrl + 'products');
  }
}
