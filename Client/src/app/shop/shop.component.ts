import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  brands: IBrand[];
  products: IProduct[];
  productTypes: IProductType[];
  shopParams: ShopParams;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ];
  totalCount: number;

  constructor(private shopService: ShopService) {
      this.shopParams = this.shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getBrands();
    this.getProducts(true);
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

  getProducts(useCache = false): void{
    this.shopService.getProducts(useCache).subscribe(
      response => {
      this.products = response.data;
      this.shopParams = this.shopService.getShopParams()
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.shopService.setShopParams(this.shopParams);
      this.totalCount = response.count;
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
    const params = this.shopService.getShopParams()
    params.brandId = brandId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(page: number): void {
    const params = this.shopService.getShopParams()
    params.pageNumber = page;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }

  onProductTypeSelected(typeId: number): void {
    const params = this.shopService.getShopParams()
    params.typeId = typeId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    const params = this.shopService.getShopParams()
    params.sort = sort;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSearch(): void {
    const params = this.shopService.getShopParams()
    params.search = this.searchTerm.nativeElement.value;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset(): void {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
