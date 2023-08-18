import { DOCUMENT } from '@angular/common';
import { Component, ElementRef, Inject, OnInit, Renderer2, ViewChild } from '@angular/core';
import { CartService } from 'src/app/services/cartService/cart.service';

declare const ymaps: any;

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  map: any;
  latitude = 41.3;
  longitude = 69.3;
  /* @ViewChild('yamapsss') el!: ElementRef; */

  constructor(public cartService: CartService){}

  ngOnInit(): void {
    ymaps.ready(this.createMap.bind(this));
  }

  private createMap(): void {
    //[55.76, 37.64]
    this.map = new ymaps.Map('map', {
      center: [54.19, 37.61],
      zoom: 12
    });

    this.map.geoObjects.add(new ymaps.Placemark([54.19243182643644,37.58518530197855], {
      balloonContent: 'Ресторан <strong>"Sushi House"</strong>',
      iconCaption: '"Sushi House"'
      }, { preset: 'islands#greenDotIconWithCaption' }));
  }
}
