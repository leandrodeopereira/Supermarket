import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
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
