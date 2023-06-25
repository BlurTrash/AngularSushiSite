import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Subscription, filter } from 'rxjs';
import { BreadCrumbItem } from 'src/app/models/breadCrumbItem';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit, OnDestroy {
  static readonly ROUTE_DATA_BREADCRUMB = 'breadcrumb';
  breadCrumbs: BreadCrumbItem[] = [];
  listener: Subscription | undefined;

  constructor(private router: Router, private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.listener = this.router.events.pipe(filter((event)=> event instanceof NavigationEnd))
    .subscribe(()=> {
      let crumbs: BreadCrumbItem[] = [];
      let routeUrl = "";
      let label = "Главная";
      crumbs.push(new BreadCrumbItem(label, routeUrl));

      this.createBreadcrumbs(this.activatedRoute.root, crumbs);
      this.breadCrumbs = crumbs;
    });

    let crumbs: BreadCrumbItem[] = [];
      let routeUrl = "";
      let label = "Главная";
      crumbs.push(new BreadCrumbItem(label, routeUrl));

      this.createBreadcrumbs(this.activatedRoute.root, crumbs);
      this.breadCrumbs = crumbs;
  }

  ngOnDestroy(): void {
    this.listener?.unsubscribe();
  }

  private createBreadcrumbs(route: ActivatedRoute, breadcrumbs: BreadCrumbItem[], url: string = '#'): void {
    let rooturl = route.snapshot.url.map(segment => segment.path).join('/');
    console.log(rooturl);

    const children: ActivatedRoute[] = route.children;

    if(children.length === 0)
      return;

    for(const child of children) {
      let routeUrl = child.snapshot.pathFromRoot.map(v => v.url.map(segment => segment.path).join('/')).join('/');
     /*  if(routeUrl !== ''){
        url += `${routeUrl}`;
      } */

      const label = child.snapshot.data[BreadcrumbComponent.ROUTE_DATA_BREADCRUMB];
      if(label !== null || label !== undefined){
        breadcrumbs.push(new BreadCrumbItem(label, routeUrl));
      }

      this.createBreadcrumbs(child, breadcrumbs, url);
    }
  }
}
