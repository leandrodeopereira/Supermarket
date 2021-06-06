import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs/internal/observable/of';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private baseUrl = environment.apiUrl;
  private products: IProduct[] = [];
  private brands: IBrand[] = [];
  private productTypes: IProductType[] = [];

  constructor(private http: HttpClient) {}

  getBrands(): Observable<IBrand[]> {
    if (this.brands && this.brands.length > 0) {
      return of(this.brands);
    }

    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
      .pipe(
        map(response => {
          this.brands = response;
          return this.brands;
        })
      );
  }

  getProducts(shopParams: ShopParams): Observable<IPagination> {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map((response) => {
          this.products = response.body.data;
          return response.body;
        })
      );
  }

  getProduct(id: number): Observable<IProduct> {
    const product = this.products.find(p => p.id === id);
    if (product) {
      return of(product);
    }

    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getProductTypes(): Observable<IProductType[]> {
    if (this.productTypes && this.productTypes.length > 0) {
      return of(this.productTypes);
    }

    return this.http.get<IProductType[]>(this.baseUrl + 'products/types')
      .pipe(
        map(response => {
          this.productTypes = response;
          return this.productTypes;
        })
      );
  }
}
