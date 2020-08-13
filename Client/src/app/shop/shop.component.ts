import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IProductType } from '../shared/models/productType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  brands: IBrand[];
  products: IProduct[];
  productTypes: IProductType[];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getBrands();
    this.getProducts();
    this.getProductTypes();
  }

  getBrands(): void{
    this.shopService.getBrands().subscribe(
      response => {
      this.brands = response;
    }, error => {
      console.log(error);
    });
  }

  getProducts(): void{
    this.shopService.getProducts().subscribe(
      response => {
      this.products = response.data;
    }, error => {
      console.log(error);
    });
  }

  getProductTypes(): void{
    this.shopService.getProductTypes().subscribe(
      response => {
      this.productTypes = response;
    }, error => {
      console.log(error);
    });
  }
}
