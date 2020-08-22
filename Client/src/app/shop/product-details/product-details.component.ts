import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.breadcrumbService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  addProductToBasket(): void {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  loadProduct(): void {
    this.shopService
      .getProduct(+this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          this.product = product;
          this.breadcrumbService.set('@productDetails', product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
