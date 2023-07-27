import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-pizza',
  templateUrl: './pizza.component.html',
  styleUrls: ['./pizza.component.css']
})
export class PizzaComponent implements OnInit {
  constructor(private itemService: ItemService, public router: Router) {
  }

  categoryId: number = 6;
  pizzaList: ItemVM[] = [];

  ngOnInit(): void {
    this.itemService.getItemsByCategoryId(this.categoryId).subscribe((data) => {
      this.pizzaList = data;
    });
  }

}
