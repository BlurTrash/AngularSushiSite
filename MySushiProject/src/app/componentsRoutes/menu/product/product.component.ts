import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Subscription, filter } from 'rxjs';
import { BreadcrumbService } from 'src/app/services/breadcrumbService/breadcrumb.service';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit, OnDestroy {
  id: number | undefined;
  product: ItemVM = new ItemVM();
  similarProducts: ItemVM[] = [];
  listener: Subscription | undefined;

  constructor(private breadcrumbService: BreadcrumbService, private router: Router, private itemService: ItemService, private activateRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.listener = this.router.events.pipe(filter((event)=> event instanceof NavigationEnd))
    .subscribe(()=> {
      this.id = this.activateRoute.snapshot.params['id'];
      this.itemService.get(this.id).subscribe(p => {
        this.product = p;
        if(this.product) {
          this.breadcrumbService.updateLabelForRoute(this.router.url, this.product.name as string);
          this.itemService.getItemsByCategoryId(this.product.categoryId).subscribe(d => {
            let otherItems = d.filter(i => i.id !== this.product.id);
            this.similarProducts = this.getRandomItems(otherItems, 3);

            console.log(this.similarProducts);
          });
        }
      });
    });

    this.id = this.activateRoute.snapshot.params['id'];
    this.itemService.get(this.id).subscribe(p => {
      this.product = p;
      if(this.product) {
        this.breadcrumbService.updateLabelForRoute(this.router.url, this.product.name as string);
        this.itemService.getItemsByCategoryId(this.product.categoryId).subscribe(d => {
          let otherItems = d.filter(i => i.id !== this.product.id);
          this.similarProducts = this.getRandomItems(otherItems, 3);

          console.log(this.similarProducts);
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.listener?.unsubscribe();
  }

  getRandomItems(items: ItemVM[], count: number) {
    let similarItems: ItemVM[] = [];
    let numbers: number[] = [];
    let limit = items.length >= count ? count : items.length;

    if(items.length > 0) {
      while(similarItems.length < limit) {
        let randomNumber = Math.floor(Math.random() * items.length);
        if(!numbers.includes(randomNumber)) {
          numbers.push(randomNumber);
          let randItem = items[randomNumber];
          similarItems.push(randItem);
        }
      }
    }

    return similarItems;
  }
}
