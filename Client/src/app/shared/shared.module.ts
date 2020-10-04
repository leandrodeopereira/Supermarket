import { NgModule } from '@angular/core';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { CommonModule } from '@angular/common';

import { BasketSummaryComponent } from './components/basket-summary/basket-summary.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { StepperComponent } from './components/stepper/stepper.component';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [
    BasketSummaryComponent,
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    StepperComponent,
    TextInputComponent,
  ],
  imports: [
    CommonModule,
    CdkStepperModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ReactiveFormsModule,
    RouterModule,
  ],
  exports: [
    BasketSummaryComponent,
    BsDropdownModule,
    CarouselModule,
    CdkStepperModule,
    OrderTotalsComponent,
    PagingHeaderComponent,
    PagerComponent,
    ReactiveFormsModule,
    StepperComponent,
    TextInputComponent,
  ]
})
export class SharedModule { }
