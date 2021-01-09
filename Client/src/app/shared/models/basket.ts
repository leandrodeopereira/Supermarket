import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
  deliveryMethodId?: number; 
}

export interface IBasketItem {
  brand: string;
  id: number;
  price: number;
  productName: string;
  productType: string;
  pictureUrl: string;
  quantity: number;
}

export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
