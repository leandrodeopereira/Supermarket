import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IBrand } from '../shared/models/brand';
import { IPagination, Pagination } from '../shared/models/pagination';
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
  private pagination = new Pagination();
  private shopParams = new ShopParams()
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

  getProducts(): Observable<IPagination> {
    let params = new HttpParams();

    if (this.shopParams.brandId !== 0) {
      params = params.append('brandId', this.shopParams.brandId.toString());
    }

    if (this.shopParams.typeId !== 0) {
      params = params.append('typeId', this.shopParams.typeId.toString());
    }

    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }

    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageNumber.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map((response) => {
          this.products = [...this.products, ...response.body.data];
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }

  getShopParams(){
    return this.shopParams;
  }

  setShopParams(params: ShopParams){
    this.shopParams = params;
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
