import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-baked-rolles',
  templateUrl: './baked-rolles.component.html',
  styleUrls: ['./baked-rolles.component.css']
})
export class BakedRollesComponent implements OnInit {
  constructor(private itemService: ItemService, public router: Router) {
  }

  categoryId: number = 1;
  itemsList: ItemVM[] = [];

  ngOnInit(): void {
    this.itemService.getItemsByCategoryId(this.categoryId).subscribe((data) => {
      this.itemsList = data;
    });
  }
}
