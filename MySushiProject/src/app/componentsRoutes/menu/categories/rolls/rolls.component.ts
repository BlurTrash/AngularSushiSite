import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-rolls',
  templateUrl: './rolls.component.html',
  styleUrls: ['./rolls.component.css']
})
export class RollsComponent implements OnInit {
  constructor(private itemService: ItemService, public router: Router) {
  }

  categoryId: number = 3;
  itemsList: ItemVM[] = [];

  ngOnInit(): void {
    this.itemService.getItemsByCategoryId(this.categoryId).subscribe((data) => {
      this.itemsList = data;
    });
  }
}
