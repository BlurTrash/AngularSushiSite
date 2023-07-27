import { Injectable } from '@angular/core';
import { BreadCrumbItem } from 'src/app/models/breadCrumbItem';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {
  constructor() { }

  breadCrumbs: BreadCrumbItem[] = [];

  public setBreadCrumbs(breadCrumbs: BreadCrumbItem[]) {
    this.breadCrumbs = breadCrumbs;
  }

  public getBreadCrumbs() {
    return this.breadCrumbs;
  }

  public updateLabelForRoute(url: string, newLabel: string) {
    if(this.breadCrumbs.length > 0) {
      let crumb = this.findBreadCrumb(url);
      if(crumb) {
        crumb.label = newLabel;
      }
    }
  }

  public clearBreadCrumbs() {
    this.breadCrumbs = [];
  }

  private findBreadCrumb(url: string) {
    for(let crumb of this.breadCrumbs) {
      if(crumb.url === url) {
        return crumb;
      }
    }
    return null;
  }
}
