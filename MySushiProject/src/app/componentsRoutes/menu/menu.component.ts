import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { CategoryService, CategoryVM } from 'src/app/services/categoryService/category.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  //categories: Category[] = [];
  categories: CategoryVM[] = [];

  constructor(public router: Router, private categoryService: CategoryService){
  }

  ngOnInit(): void {
    //this.categoryService.getData().subscribe({next: (data:any)=> this.categories = data["categories"]});

    this.categoryService.getAllCategories().subscribe((data) => {
      this.categories = data;
     /*  this.categories.forEach((category) => {
        category.imageData = 'data:image/png;base64,' + category.imageData;
        category.iconData = 'data:image/png;base64,' + category.iconData;
      }); */

      /* let firstobj = data[0];
      let img = 'data:image/png;base64,' + firstobj.imageData;
      let icon = 'data:image/png;base64,' + firstobj.iconData;

      this.categories[0] = new Category(firstobj.id as number, firstobj.name as string, img, icon, firstobj.url as string);
      console.log(this.categoriesTest[0]); */
    });
  }

}
