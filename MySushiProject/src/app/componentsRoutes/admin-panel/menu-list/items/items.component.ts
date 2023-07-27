import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CategoryService, CategoryVM } from 'src/app/services/categoryService/category.service';
import { ItemService, ItemVM } from 'src/app/services/itemService/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {
  constructor(private categoryService: CategoryService, private itemService: ItemService){}

  @ViewChild('itmForm') public itemForm : NgForm | undefined;
  @ViewChild("modalwindowborder", {static: false}) modWindowBorder: ElementRef | undefined;
  @ViewChild("modalwindow", {static:false}) modWindow: ElementRef | undefined;
  @ViewChild("modaldeletewindowborder", {static: false}) modDeleteWindowBorder: ElementRef | undefined;
  @ViewChild("modaldeletewindow", {static:false}) modDeleteWindow: ElementRef | undefined;
  @ViewChild("addButton", {static:false}) addItemButton: ElementRef | undefined;
  @ViewChild("updateButton", {static:false}) updateItemButton: ElementRef | undefined;

  selectedCategory: CategoryVM | null | undefined;
  categories: CategoryVM[] = [];
  items: ItemVM[] = [];

  itemId: number = 0;
  itemCategoryId: number = 0;
  itemName: string | undefined = "";
  itemUrlName: string | undefined = "";
  itemDesription: string | undefined;
  itemIngredients: string | undefined;
  itemImageData: string | undefined;
  itemPrice: number = 0;
  itemWeight: number = 0;
  itemCount: number = 1;
  filePhoto: any;

  filePhotoInput: any;
  errorMessage: string = "";

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((data) => {
      this.categories = data;
      this.selectedCategory = this.categories[0];
      this.selectionChangedCategory();
   });
  }

  selectionChangedCategory() {
    if(this.selectedCategory) {
      this.itemService.getItemsByCategoryId(this.selectedCategory.id).subscribe((data) => {
        this.items = data;
      });
    }
  }

  openModalWindow(isNew: boolean, item: ItemVM = new ItemVM()) : void {
    if(this.modWindowBorder !== undefined && this.modWindow !== undefined && this.selectedCategory) {
      this.itemId = item.id;
      this.itemName = item.name;
      this.itemUrlName = item.urlName;
      if(isNew) {
        this.itemCategoryId = this.selectedCategory?.id;
      }
      else{
        this.itemCategoryId = item.categoryId;
      }
      this.itemDesription = item.desription;
      this.itemIngredients = item.ingredients;
      this.itemImageData = item.imageUrl;
      this.itemPrice = item.price;
      this.itemWeight = item.weight;
      this.itemCount = item.count;
      this.filePhotoInput = undefined;

      const previewImg = document.getElementById('previewPhoto') as HTMLImageElement;
      if(this.itemImageData) {
        previewImg.style.display = "block";
      }
      else {
        previewImg.style.display = "none";
      }

      if(this.addItemButton && this.updateItemButton)
      {
        if(isNew) {
          this.addItemButton.nativeElement.style.display = "block";
          this.updateItemButton.nativeElement.style.display = "none";
        }
        else {
          this.addItemButton.nativeElement.style.display = "none";
          this.updateItemButton.nativeElement.style.display = "block";
        }
      }

      this.modWindowBorder.nativeElement.style.display = "block";
      this.modWindow.nativeElement.style.display = "block";
    }
  }

  closeModalWindow() : void {
    if(this.modWindowBorder !== undefined && this.modWindow !== undefined) {
      this.itemId = 0;
      this.itemCategoryId = 0;
      this.itemName  = "";
      this.itemUrlName = "";
      this.itemDesription = "";
      this.itemIngredients = "";
      this.itemImageData = ""
      this.itemPrice = 0;
      this.itemWeight = 0;
      this.itemCount = 1;
      this.errorMessage = "";
      this.filePhoto = undefined;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "none";
      }

      this.modWindowBorder.nativeElement.style.display = "none";
      this.modWindow.nativeElement.style.display = "none";

      (this.itemForm as NgForm).resetForm();
      (this.itemForm as NgForm).form.reset();
    }
  }

  openDeleteWindow(item: ItemVM) : void {
    if(this.modDeleteWindow !== undefined && this.modDeleteWindowBorder !== undefined) {
      this.itemId = item.id;

      this.modDeleteWindowBorder.nativeElement.style.display = "block";
      this.modDeleteWindow.nativeElement.style.display = "block";
    }
  }

  closeDeleteWindow() : void {
    if(this.modDeleteWindow !== undefined && this.modDeleteWindowBorder !== undefined) {
      this.itemId = 0;
      this.modDeleteWindowBorder.nativeElement.style.display = "none";
      this.modDeleteWindow.nativeElement.style.display = "none";
    }
  }

  onFilePhotoSelected(event: any) {
    const file = event.target.files[0];
    this.filePhoto = event.target.files[0];
    const preview = document.getElementById('previewPhoto') as HTMLImageElement;
    const reader = new FileReader();

    reader.addEventListener("loadend", ()=> {
      this.itemImageData = reader.result as string;
    }, false);

    if (file) {
      reader.readAsDataURL(file);
      preview.style.display = "block";
    }
  }

  onFilePhotoClick() {
    if(this.itemImageData) {
      this.itemImageData = "";
      const preview = document.getElementById('previewPhoto') as HTMLImageElement;
      preview.style.display = "none";
    }
  }

  addItem() {
    let result = this.itemService.post(this.itemId, this.itemName, this.itemUrlName, this.itemDesription, this.itemIngredients, this.filePhoto, this.itemPrice, this.itemWeight, this.itemCount, this.itemCategoryId);
    result.subscribe(s => {
      /* this.categoryService.getAllCategories().subscribe((data) => {
        this.selectionChangedCategory();
        this.closeModalWindow();
     }); */
      this.selectionChangedCategory();
      this.closeModalWindow();
    },
    (error: HttpErrorResponse) => {
      this.errorMessage = error.error? error.error : error.statusText;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "block";
      }
    });
  }

  updateItem() {
    let result = this.itemService.put(this.itemId, this.itemName, this.itemUrlName, this.itemDesription, this.itemIngredients, this.filePhoto, this.itemPrice, this.itemWeight, this.itemCount, this.itemCategoryId);
    result.subscribe(s => {
      this.selectionChangedCategory();
      this.closeModalWindow();
    },
    (error: HttpErrorResponse) => {
      this.errorMessage = error.error? error.error : error.statusText;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "block";
      }
    });
  }

  deleteItem() {
    let result = this.itemService.delete(this.itemId);
    result.subscribe(s => {
      this.selectionChangedCategory();
      this.closeDeleteWindow();
    });
  }
}
