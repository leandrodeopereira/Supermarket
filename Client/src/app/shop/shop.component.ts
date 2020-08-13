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
  brandIdSelected = 0;
  brands: IBrand[];
  products: IProduct[];
  productTypeIdSelected = 0;
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
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  getProducts(): void{
    this.shopService.getProducts(this.brandIdSelected, this.productTypeIdSelected).subscribe(
      response => {
      this.products = response.data;
    }, error => {
      console.log(error);
    });
  }

  getProductTypes(): void{
    this.shopService.getProductTypes().subscribe(
      response => {
      this.productTypes = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number): void {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onProductTypeSelected(typeId: number): void {
    this.productTypeIdSelected = typeId;
    this.getProducts();
  }
}
