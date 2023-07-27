import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-sushi',
  templateUrl: './sushi.component.html',
  styleUrls: ['./sushi.component.css']
})
export class SushiComponent implements OnInit {
  constructor(private itemService: ItemService, public router: Router) {
  }

  categoryId: number = 5;
  itemsList: ItemVM[] = [];

  ngOnInit(): void {
    this.itemService.getItemsByCategoryId(this.categoryId).subscribe((data) => {
      this.itemsList = data;
    });
  }
}
