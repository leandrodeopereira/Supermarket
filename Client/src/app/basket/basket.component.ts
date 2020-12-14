import { Component, OnInit } from '@angular/core';

import { BasketService } from './basket.service';
import { IBasket } from '../shared/models/basket';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }
}
