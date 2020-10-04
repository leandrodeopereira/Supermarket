import { IAddress } from './address';

export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: IAddress;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  deliveryMethod: string;
  orderDate: string;
  orderItems: IOrderItem[];
  status: number;
  shippingPrice: number;
  shipToAddress: IAddress;
  subtotal: number;
  total: number;
}

export interface IOrderItem {
  pictureUrl: string;
  price: number;
  productId: number;
  productName: string;
  quantity: number;
}
