import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-fried-rolls',
  templateUrl: './fried-rolls.component.html',
  styleUrls: ['./fried-rolls.component.css']
})
export class FriedRollsComponent implements OnInit{
  constructor(private itemService: ItemService, public router: Router) {
  }

  categoryId: number = 2;
  itemsList: ItemVM[] = [];

  ngOnInit(): void {
    this.itemService.getItemsByCategoryId(this.categoryId).subscribe((data) => {
      this.itemsList = data;
    });
  }
}
