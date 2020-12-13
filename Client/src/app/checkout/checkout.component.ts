import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';
import { IBasketTotals } from '../shared/models/basket';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
})
export class CheckoutComponent implements OnInit {
  basketTotals$: Observable<IBasketTotals>;
  checkoutForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private basketService: BasketService,
  ) {}

  ngOnInit(): void {
    this.basketTotals$ = this.basketService.basketTotal$;
    this.createCheckoutForm();
    this.populateAddressFormValues();
  }

  createCheckoutForm(): void {
    this.checkoutForm = this.formBuilder.group({
      addressForm: this.formBuilder.group({
        city: [null, Validators.required],
        country: [null, Validators.required],
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        state: [null, Validators.required],
        street: [null, Validators.required],
        zipCode: [null, Validators.required],
      }),
      deliveryForm: this.formBuilder.group({
        deliveryMethod: [null, Validators.required],
      }),
    });
  }

  populateAddressFormValues(): void {
    this.accountService.getUserAddress().subscribe(
      (address) => {
        if (address) {
          this.checkoutForm.get('addressForm').patchValue(address);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
