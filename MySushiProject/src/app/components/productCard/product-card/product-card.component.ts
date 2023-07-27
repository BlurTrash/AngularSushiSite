import { Component, Input, OnInit } from '@angular/core';

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

  constructor() {}

  ngOnInit(): void {
  }
}
