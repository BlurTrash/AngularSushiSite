import { Injectable } from '@angular/core';
import { ItemVM } from '../itemService/item.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  orderLines: OrderLineVM[] = [];

  constructor() { }

  addItemInCart(item: ItemVM) {
    let newItemLine = new OrderLineVM();
    newItemLine.id = 0;
    newItemLine.item = item;
    newItemLine.orderId = 0;
    newItemLine.quantity = 1;
    newItemLine.unitPrice = item.price;
    newItemLine.price = item.price;

    this.orderLines.push(newItemLine);
  }
}

export class OrderLineVM
{
    id: number = 0;
    item?: ItemVM | undefined;
    orderId: number = 0;
    quantity: number = 1;
    unitPrice: number = 0;
    price: number = 0;
}
