import { Component, OnInit } from '@angular/core';

import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
})
export class CheckoutReviewComponent implements OnInit {
  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }
}
