import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CheckoutAddressComponent } from './checkout-address/checkout-address.component';
import { CheckoutComponent } from './checkout.component';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutPaymentComponent } from './checkout-payment/checkout-payment.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    CheckoutAddressComponent,
    CheckoutComponent,
    CheckoutDeliveryComponent,
    CheckoutPaymentComponent,
    CheckoutReviewComponent,
    CheckoutSuccessComponent,
  ],
  imports: [CommonModule, CheckoutRoutingModule, SharedModule],
})
export class CheckoutModule {}
