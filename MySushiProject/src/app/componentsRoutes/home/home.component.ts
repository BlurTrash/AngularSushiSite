import { Component, OnInit } from '@angular/core';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  newestProducts: ItemVM[] = [];

  constructor(private itemsService: ItemService){}

  ngOnInit(): void {
    this.itemsService.getNewestItems(3).subscribe(data => {
      this.newestProducts = [...data];
    });
  }

}
