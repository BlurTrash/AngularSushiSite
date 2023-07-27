import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  baseUrl: string = "https://localhost:7088";

  constructor(private http: HttpClient) { }

  get(itemId: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/Get/" + itemId;
    if (itemId === undefined || itemId === null)
        throw new Error("The parameter 'itemId' must be defined.");
    return this.http.get<ItemVM>(url_);
  }

  getNewestItems(newestItemsCount: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/GetNewestItems/" + newestItemsCount;
    if (newestItemsCount === undefined || newestItemsCount === null)
        throw new Error("The parameter 'newestItemsCount' must be defined.");
    return this.http.get<ItemVM[]>(url_);
  }

  getItemsByCategoryId(categoryId: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/GetItemsByCategoryId/" + categoryId;
    if (categoryId === undefined || categoryId === null)
        throw new Error("The parameter 'categoryId' must be defined.");
    return this.http.get<ItemVM[]>(url_);
  }

  post(id: number | undefined, name: string | undefined, urlName: string | undefined ,desription: string | undefined, ingredients: string | undefined, imageData: any, price: number | undefined, weight: number | undefined, count: number | undefined, categoryId: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/Post";

    const content_ = new FormData();
        if (id === null || id === undefined)
            throw new Error("The parameter 'id' cannot be null.");
        else
            content_.append("Id", id.toString());
        if (name === null || name === undefined)
            throw new Error("The parameter 'name' cannot be null.");
        else
            content_.append("Name", name.toString());
        if (urlName === null || urlName === undefined)
            throw new Error("The parameter 'urlName' cannot be null.");
        else
            content_.append("UrlName", urlName.toString());
        if (desription === null || desription === undefined)
            content_.append("Desription", "");
        else
            content_.append("Desription", desription.toString());
        if (ingredients === null || ingredients === undefined)
            throw new Error("The parameter 'ingredients' cannot be null.");
        else
            content_.append("Ingredients", ingredients.toString());
        if (imageData === null || imageData === undefined)
            throw new Error("The parameter 'imageData' cannot be null.");
        else
            content_.append("ImageData", imageData);
        if (price === null || price === undefined)
            throw new Error("The parameter 'price' cannot be null.");
        else
            content_.append("Price", price.toString());
        if (weight === null || weight === undefined)
            throw new Error("The parameter 'weight' cannot be null.");
        else
            content_.append("Weight", weight.toString());
        if (count === null || count === undefined)
            throw new Error("The parameter 'count' cannot be null.");
        else
            content_.append("Count", count.toString());
        if (categoryId === null || categoryId === undefined)
            throw new Error("The parameter 'categoryId' cannot be null.");
        else
            content_.append("CategoryId", categoryId.toString());

    return this.http.post(url_, content_);
  }

  put(id: number | undefined, name: string | undefined, urlName: string | undefined ,desription: string | undefined, ingredients: string | undefined, imageData: any, price: number | undefined, weight: number | undefined, count: number | undefined, categoryId: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/Put";

    const content_ = new FormData();
        if (id === null || id === undefined)
            throw new Error("The parameter 'id' cannot be null.");
        else
            content_.append("Id", id.toString());
        if (name === null || name === undefined)
            throw new Error("The parameter 'name' cannot be null.");
        else
            content_.append("Name", name.toString());
        if (urlName === null || urlName === undefined)
            throw new Error("The parameter 'urlName' cannot be null.");
        else
            content_.append("UrlName", urlName.toString());
        if (desription === null || desription === undefined)
            content_.append("Desription", "");
        else
            content_.append("Desription", desription.toString());
        if (ingredients === null || ingredients === undefined)
            throw new Error("The parameter 'ingredients' cannot be null.");
        else
            content_.append("Ingredients", ingredients.toString());
        if (imageData === null || imageData === undefined)
            content_.append("ImageData", "");
        else
            content_.append("ImageData", imageData);
        if (price === null || price === undefined)
            throw new Error("The parameter 'price' cannot be null.");
        else
            content_.append("Price", price.toString());
        if (weight === null || weight === undefined)
            throw new Error("The parameter 'weight' cannot be null.");
        else
            content_.append("Weight", weight.toString());
        if (count === null || count === undefined)
            throw new Error("The parameter 'count' cannot be null.");
        else
            content_.append("Count", count.toString());
        if (categoryId === null || categoryId === undefined)
            throw new Error("The parameter 'categoryId' cannot be null.");
        else
            content_.append("CategoryId", categoryId.toString());

    return this.http.put(url_, content_);
  }

  delete(id: number | undefined) {
    let url_ = this.baseUrl + "/api/Item/Delete/" + id;
    if (id === undefined || id === null)
        throw new Error("The parameter 'id' must be defined.");

    return this.http.delete(url_);
  }
}

export class ItemVM {
  id: number = 0;
  name?: string | undefined;
  urlName?: string | undefined;
  desription?: string | undefined;
  ingredients?: string | undefined;
  imageUrl?: string | undefined;
  price: number = 0;
  weight: number = 0;
  count: number = 1;
  categoryId: number = 0;
  fullUrl?: string | undefined;
}
