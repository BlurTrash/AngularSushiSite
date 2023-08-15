import { Component, ElementRef, ViewChild } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MySushiProject';

  @ViewChild("menubg", {static: false}) menuBurger: ElementRef | undefined;
  @ViewChild("menubl", {static: false}) menuMobile: ElementRef | undefined;


  constructor(public router: Router)
  {
  }

  openMenuBurger() : void {
    if(this.menuBurger !== undefined && this.menuMobile !== undefined) {
      /* this.menuBurger.nativeElement.classList.add(''); */
      this.menuBurger.nativeElement.style.display = "block";
      this.menuMobile.nativeElement.style.right = "0";
    }
  }

  closeMenuBurger() : void {
    if(this.menuBurger !== undefined && this.menuMobile !== undefined) {
      /* this.menuBurger.nativeElement.classList.add(''); */
      this.menuBurger.nativeElement.style.display = "none";
      this.menuMobile.nativeElement.style.right = "-280px";
    }
  }

  openDropdownCart() : void {
    let cartElement = document.getElementById("cart-menu");
    if(cartElement) {
      cartElement.style.display = "block";
    }
  }
}
