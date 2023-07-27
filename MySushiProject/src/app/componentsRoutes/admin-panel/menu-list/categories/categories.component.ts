import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Client, CategoryViewModel } from 'src/app/Client/http-client';
import { CategoryService, CategoryVM } from 'src/app/services/categoryService/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  constructor(private categoryService: CategoryService){}
  @ViewChild('catForm') public categoryForm : NgForm | undefined;
  @ViewChild("modalwindowborder", {static: false}) modWindowBorder: ElementRef | undefined;
  @ViewChild("modalwindow", {static:false}) modWindow: ElementRef | undefined;
  @ViewChild("modaldeletewindowborder", {static: false}) modDeleteWindowBorder: ElementRef | undefined;
  @ViewChild("modaldeletewindow", {static:false}) modDeleteWindow: ElementRef | undefined;
  @ViewChild("addButton", {static:false}) addCategoryButton: ElementRef | undefined;
  @ViewChild("updateButton", {static:false}) updateCategoryButton: ElementRef | undefined;

  categories: CategoryVM[] = [];
  categoryId: number = 0;
  categoryName: string | undefined = "";
  categoryUrl: string | undefined = "";
  categoryImageData: string | undefined = "";
  categoryIconData: string | undefined = "";
  filePhotoInput: any;
  fileIconInput: any;
  filePhoto: any;
  fileIcon: any;
  filePhotoName: string | null = null;
  fileIconName: string | null = null;

  errorMessage: string = "";
  //httpClient: Client = new Client("https://localhost:7088");

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((data) => {
       this.categories = data;
      /*  this.categories.forEach((category) => {
         category.imageData = 'data:image/png;base64,' + category.imageData;
         category.iconData = 'data:image/png;base64,' + category.iconData;
       }); */
    });
  }

  openModalWindow(isNew: boolean, category: CategoryVM = new CategoryVM()) : void {
    if(this.modWindowBorder !== undefined && this.modWindow !== undefined) {

      this.categoryId = category.id;
      this.categoryName = category.name;
     /*  if(category.url){
        this.categoryUrl = category.url.replace("/menu/", '');
      }
      else {
        this.categoryUrl = category.url;
      } */
      this.categoryUrl = category.url;

      this.categoryImageData = category.imageData;
      this.categoryIconData = category.iconData;
      this.filePhotoInput = undefined;
      this.fileIconInput = undefined;

      const previewImg = document.getElementById('previewPhoto') as HTMLImageElement;
      const previewIcon = document.getElementById('previewIcon') as HTMLImageElement;
      if(this.categoryImageData) {
        previewImg.style.display = "block";
      }
      else {
        previewImg.style.display = "none";
      }

      if(this.categoryIconData) {
        previewIcon.style.display = "block";
      }
      else {
        previewIcon.style.display = "none";
      }

      if(this.addCategoryButton && this.updateCategoryButton)
      {
        if(isNew) {
          this.addCategoryButton.nativeElement.style.display = "block";
          this.updateCategoryButton.nativeElement.style.display = "none";
        }
        else {
          this.addCategoryButton.nativeElement.style.display = "none";
          this.updateCategoryButton.nativeElement.style.display = "block";
        }
      }

      this.modWindowBorder.nativeElement.style.display = "block";
      this.modWindow.nativeElement.style.display = "block";
    }
  }

  /* form: NgForm */
  closeModalWindow() : void {
    if(this.modWindowBorder !== undefined && this.modWindow !== undefined) {
      this.categoryId = 0;
      this.categoryName = "";
      this.categoryUrl = "";
      this.categoryImageData = "";
      this.categoryIconData = "";
      this.errorMessage = "";
      this.filePhoto = undefined;
      this.fileIcon = undefined;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "none";
      }

      this.modWindowBorder.nativeElement.style.display = "none";
      this.modWindow.nativeElement.style.display = "none";

      (this.categoryForm as NgForm).resetForm();
      (this.categoryForm as NgForm).form.reset();
    }
  }

  openDeleteWindow(category: CategoryVM) : void {
    if(this.modDeleteWindow !== undefined && this.modDeleteWindowBorder !== undefined) {
      this.categoryId = category.id;

      this.modDeleteWindowBorder.nativeElement.style.display = "block";
      this.modDeleteWindow.nativeElement.style.display = "block";
    }
  }

  closeDeleteWindow() : void {
    if(this.modDeleteWindow !== undefined && this.modDeleteWindowBorder !== undefined) {
      this.categoryId = 0;
      this.modDeleteWindowBorder.nativeElement.style.display = "none";
      this.modDeleteWindow.nativeElement.style.display = "none";
    }
  }

  /* convertDataURIToBinary(dataURI: string) : Uint8Array {
    var base64Index = dataURI.indexOf(';base64,') + ';base64,'.length;
    var base64 = dataURI.substring(base64Index);
    var raw = window.atob(base64);
    var rawLength = raw.length;
    var array = new Uint8Array(new ArrayBuffer(rawLength));

    for(let i = 0; i < rawLength; i++) {
      array[i] = raw.charCodeAt(i);
    }

    return array;
  } */

  onFilePhotoSelected(event: any) {
    const file = event.target.files[0];
    this.filePhoto = event.target.files[0];
    const preview = document.getElementById('previewPhoto') as HTMLImageElement;
    const reader = new FileReader();
    /* let byteArray; */

    reader.addEventListener("loadend", ()=> {
      /* preview.src = reader.result as string; */
      this.categoryImageData = reader.result as string;


      /* this.categoryImageData = reader.result as string; //test
      this.categoryIconData = reader.result as string; //test
      byteArray = this.convertDataURIToBinary(reader.result as string); */
    }, false);

    if (file) {
        this.filePhotoName = file.name;
        reader.readAsDataURL(file);
        preview.style.display = "block";
    }
  }

  onFileIconSelected(event: any) {
    const file = event.target.files[0];
    this.fileIcon = event.target.files[0];
    const preview = document.getElementById('previewIcon') as HTMLImageElement;
    const reader = new FileReader();

    reader.addEventListener("loadend", ()=> {
      /* preview.src = reader.result as string; */
      this.categoryIconData = reader.result as string;
    }, false);

    if (file) {
        this.fileIconName = file.name;
        reader.readAsDataURL(file);
        preview.style.display = "block";
    }
  }

  onFilePhotoClick() {
    if(this.categoryImageData) {
      this.categoryImageData = "";
      const preview = document.getElementById('previewPhoto') as HTMLImageElement;
      preview.style.display = "none";
    }
  }

  onFileIconClick() {
    if(this.categoryIconData) {
      this.categoryIconData = "";
      const preview = document.getElementById('previewIcon') as HTMLImageElement;
      preview.style.display = "none";
    }
  }

  addCategory() {
    //let fullUrl = "/menu/" + this.categoryUrl;
    let result = this.categoryService.post(this.categoryId, this.categoryName, this.categoryUrl, this.filePhoto, this.fileIcon);

    result.subscribe(s => {
      this.categoryService.getAllCategories().subscribe((data) => {
        this.categories = [...data];
        this.closeModalWindow();
     });
    },
    (error: HttpErrorResponse) => {
      this.errorMessage = error.error? error.error : error.statusText;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "block";
      }
    });
  }

  updateCategory() {
    //let fullUrl = "/menu/" + this.categoryUrl;
    let result = this.categoryService.put(this.categoryId, this.categoryName, this.categoryUrl, this.filePhoto, this.fileIcon);
    result.subscribe(s => {
      this.categoryService.getAllCategories().subscribe((data) => {
         this.categories = [...data];
         this.closeModalWindow();
      });
    },
    (error: HttpErrorResponse) => {
      this.errorMessage = error.error? error.error : error.statusText;
      let errorElement = document.getElementById('error');
      if(errorElement) {
        errorElement.style.display = "block";
      }
    });
  }

  deleteCategory() {
    let result = this.categoryService.delete(this.categoryId);
    result.subscribe(s => {
      this.categoryService.getAllCategories().subscribe((data) => {
        this.categories = [...data];
        this.closeDeleteWindow();
     });
    });
    //реаализовать перенаправление на страницу ошибки, в случае статуса запроса не ок
  }
}
