import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(): void {
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
      this.breadcrumbService.set('@productDetails', product.name);
    }, error => {
      console.log(error);
    });
  }

}
