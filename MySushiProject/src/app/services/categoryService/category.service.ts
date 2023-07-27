import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl: string = "https://localhost:7088";

  constructor(private http: HttpClient) { }

 /*  getData(){
    return this.http.get('assets/categories/categories.json')
  } */

  getAllCategories() {
    let url_ = this.baseUrl + "/api/Category/GetAll";

    //let response = this.http.get<CategoryVM[]>(url_);
    //response.subscribe(n => console.log(n));

    return this.http.get<CategoryVM[]>(url_);
  }

  post(id: number | undefined, name: string | undefined, url: string | undefined, imageData: any, iconData: any) {
    let url_ = this.baseUrl + "/api/Category/Post";

    const content_ = new FormData();
    if (id === null || id === undefined)
        throw new Error("The parameter 'id' cannot be null.");
    else
        content_.append("Id", id.toString());
    if (name === null || name === undefined)
        throw new Error("The parameter 'name' cannot be null.");
    else
        content_.append("Name", name.toString());
    if (url === null || url === undefined)
        throw new Error("The parameter 'url' cannot be null.");
    else
        content_.append("Url", url.toString());
    if (imageData === null || imageData === undefined)
        throw new Error("The parameter 'imageData' cannot be null.");
    else
        content_.append("ImageData", imageData);
    if (iconData === null || iconData === undefined)
        throw new Error("The parameter 'iconData' cannot be null.");
    else
        content_.append("IconData", iconData);

    return this.http.post(url_, content_);
  }

  put(id: number | undefined, name: string | undefined, url: string | undefined, imageData: any, iconData: any) {
    let url_ = this.baseUrl + "/api/Category/Put";

    const content_ = new FormData();
    if (id === null || id === undefined)
        throw new Error("The parameter 'id' cannot be null.");
    else
        content_.append("Id", id.toString());
    if (name === null || name === undefined)
        throw new Error("The parameter 'name' cannot be null.");
    else
        content_.append("Name", name.toString());
    if (url === null || url === undefined)
        throw new Error("The parameter 'url' cannot be null.");
    else
        content_.append("Url", url.toString());
    if (imageData === null || imageData === undefined)
        content_.append("ImageData", "");
    else
        content_.append("ImageData", imageData);
    if (iconData === null || iconData === undefined)
        content_.append("IconData", "");
    else
        content_.append("IconData", iconData);

    return this.http.put(url_, content_);
  }

  delete(id: number | undefined) {
    let url_ = this.baseUrl + "/api/Category/Delete/" + id;
    if (id === undefined || id === null)
        throw new Error("The parameter 'id' must be defined.");

    return this.http.delete(url_);
  }
}


export class CategoryVM {
  id: number = 0;
  name?: string | undefined;
  url?: string | undefined;
  imageData?: string | undefined;
  iconData?: string | undefined;
  fullUrl?: string | undefined;
}
