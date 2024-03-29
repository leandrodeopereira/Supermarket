import { Component, OnInit } from '@angular/core';

import { BasketService } from './basket.service';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketTotals$: Observable<IBasketTotals>;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
  }

  removeBasketItem(item: IBasketItem): void {
    this.basketService.removeItemFromBasket(item);
  }

  incrementItemQuantity(item: IBasketItem): void {
    this.basketService.incrementItemQuantity(item);
  }

  decrementItemQuantity(item: IBasketItem): void {
    this.basketService.decrementItemQuantity(item);
  }
}
