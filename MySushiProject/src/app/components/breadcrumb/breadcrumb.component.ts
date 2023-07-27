import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, Routes } from '@angular/router';
import { Subscription, filter } from 'rxjs';
import { BreadCrumbItem } from 'src/app/models/breadCrumbItem';
import { BreadcrumbService } from 'src/app/services/breadcrumbService/breadcrumb.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit, OnDestroy {
  static readonly ROUTE_DATA_BREADCRUMB = 'breadcrumb';
  breadCrumbs: BreadCrumbItem[] = [];
  listener: Subscription | undefined;

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService){}

  ngOnInit(): void {
    this.listener = this.router.events.pipe(filter((event)=> event instanceof NavigationEnd))
    .subscribe(()=> {
     /*  this.breadCrumbs = this.createBreadcrumbsTest(this.router, this.activatedRoute.root);
      this.breadcrumbService.setBreadCrumbs(this.breadCrumbs); */


      let crumbs: BreadCrumbItem[] = [];
      let routeUrl = "";
      let label = "Главная";
      crumbs.push(new BreadCrumbItem(label, routeUrl));
      /* this.createBreadcrumbs(this.activatedRoute.root, crumbs); */
      this.createBreadcrumbsTest2(this.activatedRoute.root, crumbs);
      this.breadCrumbs = crumbs;
      this.breadcrumbService.setBreadCrumbs(this.breadCrumbs);
    });

    /* this.breadCrumbs = this.createBreadcrumbsTest(this.router, this.activatedRoute.root);
    this.breadcrumbService.setBreadCrumbs(this.breadCrumbs);
 */
    let crumbs: BreadCrumbItem[] = [];
    let routeUrl = "";
    let label = "Главная";
    crumbs.push(new BreadCrumbItem(label, routeUrl));
    /* this.createBreadcrumbs(this.activatedRoute.root, crumbs); */
    this.createBreadcrumbsTest2(this.activatedRoute.root, crumbs);
    this.breadCrumbs = crumbs;
    this.breadcrumbService.setBreadCrumbs(this.breadCrumbs);
  }

  ngOnDestroy(): void {
    this.listener?.unsubscribe();
    this.breadcrumbService.clearBreadCrumbs();
  }

  private createBreadcrumbs(route: ActivatedRoute, breadcrumbs: BreadCrumbItem[], url: string = '#'): void {
    let rooturl = route.snapshot.url.map(segment => segment.path).join('/');

    const children: ActivatedRoute[] = route.children;

    if(children.length === 0)
      return;

    for(const child of children) {
      let routeUrl = child.snapshot.pathFromRoot.map(v => v.url.map(segment => segment.path).join('/')).join('/');

      const label = child.snapshot.data[BreadcrumbComponent.ROUTE_DATA_BREADCRUMB];
      if(label !== null || label !== undefined){
        breadcrumbs.push(new BreadCrumbItem(label, routeUrl));
      }

      this.createBreadcrumbs(child, breadcrumbs, url);
    }
  }

  private createBreadcrumbsTest(router: Router, activatedRoute: ActivatedRoute) {
    let crumbs: BreadCrumbItem[] = [];
    let urlSegment = router.url.split('/');
    let constractUrlStr = "";
    let currentActiveRoute = activatedRoute;

    for(const segment of urlSegment) {
      if(segment === "") {
        constractUrlStr += segment;
        crumbs.push(new BreadCrumbItem("Главная", constractUrlStr));
        constractUrlStr += "/";

        if(currentActiveRoute.children.length !== 0) {
          currentActiveRoute = currentActiveRoute.children[0];
        }
      }
      else {
        constractUrlStr += segment;
        let label =  currentActiveRoute.snapshot.data[BreadcrumbComponent.ROUTE_DATA_BREADCRUMB];
        crumbs.push(new BreadCrumbItem(label, constractUrlStr));
        constractUrlStr += "/";

        if(currentActiveRoute.children.length !== 0) {
          currentActiveRoute = currentActiveRoute.children[0];
        }
      }
    }
    return crumbs;
  }

  private createBreadcrumbsTest2(route: ActivatedRoute, breadcrumbs: BreadCrumbItem[], url: string = '#') {
    let rooturl = route.snapshot.url.map(segment => segment.path).join('/');

    const children: ActivatedRoute[] = route.children;

    console.log("дочерние: " + children.length);
    console.log(children);
    if(children.length === 0)
      return;

    for(const child of children) {
      let segments = child.snapshot.url.map(segment => segment.path);
      console.log("Сегменты: " + segments.length);
      console.log(segments);
      let newUrl = url;
      if(segments.length > 1) {
        const label = child.snapshot.data[BreadcrumbComponent.ROUTE_DATA_BREADCRUMB];
        newUrl = newUrl + "/" + segments[0];
        if(label !== null || label !== undefined){
          breadcrumbs.push(new BreadCrumbItem(label, newUrl));
          newUrl = newUrl + "/" + segments[1];
          let paramId = child.snapshot.params['id'];
          breadcrumbs.push(new BreadCrumbItem(paramId, newUrl));
        }
      }
      else {
        let routeUrl = child.snapshot.pathFromRoot.map(v => v.url.map(segment => segment.path).join('/')).join('/');
        newUrl = routeUrl;
        const label = child.snapshot.data[BreadcrumbComponent.ROUTE_DATA_BREADCRUMB];
        console.log("Маршрут: " + routeUrl);
        console.log("Метка: " + label);
        if(label !== null || label !== undefined){
          breadcrumbs.push(new BreadCrumbItem(label, routeUrl));
        }
      }

      this.createBreadcrumbsTest2(child, breadcrumbs, newUrl);
    }
  }
}
