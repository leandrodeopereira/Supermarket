import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrder, IOrderToCreate } from '../shared/models/order';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  createOrder(order: IOrderToCreate): Observable<IOrder> {
    return this.httpClient.post<IOrder>(this.baseUrl + 'orders', order);
  }

  getDeliveryMethods(): Observable<IDeliveryMethod[]> {
    return this.httpClient.get(this.baseUrl + 'orders/deliveryMethods').pipe(
      map((deliveryMethod: IDeliveryMethod[]) => {
        return deliveryMethod.sort((a, b) => b.price - a.price);
      })
    );
  }
}
