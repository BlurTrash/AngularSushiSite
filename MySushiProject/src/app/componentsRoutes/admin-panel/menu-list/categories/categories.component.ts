import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Client, CategoryViewModel } from 'src/app/Client/http-client';
import { CategoryService } from 'src/app/services/categoryService/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {
  constructor(private categoryService: CategoryService){}

  categoryName: string = "";
  categoryUrl: string = "";
  categoryImageData: string = "";
  categoryIconData: string = "";
  filePhoto: any;
  fileIcon: any;
  filePhotoName: string | null = null;
  fileIconName: string | null = null;
  //httpClient: Client = new Client("https://localhost:7088");

  convertDataURIToBinary(dataURI: string) : Uint8Array {
    var base64Index = dataURI.indexOf(';base64,') + ';base64,'.length;
    var base64 = dataURI.substring(base64Index);
    var raw = window.atob(base64);
    var rawLength = raw.length;
    var array = new Uint8Array(new ArrayBuffer(rawLength));

    for(let i = 0; i < rawLength; i++) {
      array[i] = raw.charCodeAt(i);
    }

    return array;
  }


  onFilePhotoSelected(event: any) {
    const file = event.target.files[0];
    this.filePhoto = event.target.files[0];
    const preview = document.getElementById('previewPhoto') as HTMLImageElement;;
    const reader = new FileReader();
    let byteArray;

    reader.addEventListener("loadend", ()=> {
      // convert image file to base64 string
      //console.log('base64', reader.result);
      preview.src = reader.result as string;
      this.categoryImageData = reader.result as string; //test
      this.categoryIconData = reader.result as string; //test
      byteArray = this.convertDataURIToBinary(reader.result as string);
      //console.log('byte array', byteArray);
    }, false);

    if (file) {
        this.filePhotoName = file.name;
        reader.readAsDataURL(file);
    }
  }

  onFileIconSelected(event: any) {
    const file = event.target.files[0];
    this.fileIcon = event.target.files[0];
    const preview = document.getElementById('previewIcon') as HTMLImageElement;;
    const reader = new FileReader();

    reader.addEventListener("loadend", ()=> {
      preview.src = reader.result as string;
    }, false);

    if (file) {
        this.fileIconName = file.name;
        reader.readAsDataURL(file);
    }
  }

  addCategory() {
   /*  let newCategory: CategoryViewModel = new CategoryViewModel();
    newCategory.id = 0;
    newCategory.name = this.categoryName;
    newCategory.url = this.categoryUrl;
    newCategory.iconUrl = "afafaffff";
    newCategory.imageUrl = "sssffeeee";
    console.log(this.categoryImageData)
    newCategory.imageData = this.categoryImageData; */

    /* let result = this.httpClient.post(newCategory);
    result.then(c => console.log(c)); */

   /*  let result = this.httpClient.post(0, this.categoryName, this.categoryUrl, "sssffeeee", "afafaffff",  this.file);
    result.then(c => console.log(c)); */
    let fullUrl = "/menu/" + this.categoryUrl;

    let result = this.categoryService.post(0, this.categoryName, fullUrl, this.filePhoto, this.fileIcon);
    result.subscribe(s => console.log(s));
  }
}
