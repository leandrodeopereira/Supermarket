import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private baseUrl = 'https://localhost:44387/api/';

  constructor(private http: HttpClient) {}

  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getProducts(): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + 'products');
  }

  getProductTypes(): Observable<IProductType[]> {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }
}
