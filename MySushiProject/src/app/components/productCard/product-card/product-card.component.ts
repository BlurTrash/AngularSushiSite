import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cartService/cart.service';
import { ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() name: string | undefined;
  @Input() ingridients: string | undefined;
  @Input() imageUrl: string | undefined;
  @Input() price: number | undefined;
  @Input() itemId: number | undefined;
  @Input() itemLink: any;
  @Input() item: ItemVM | undefined;

  constructor(public cartService: CartService) {}

  ngOnInit(): void {
  }

  pushItemInCart(): void {
    if(this.item) {
      this.cartService.addItemInCart(this.item);
    }
  }
}
