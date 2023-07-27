import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './componentsRoutes/home/home.component';
import { MenuComponent } from './componentsRoutes/menu/menu.component';
import { Routes, RouterModule } from '@angular/router';
import { PromotionsComponent } from './componentsRoutes/promotions/promotions.component';
import { DeliveryComponent } from './componentsRoutes/delivery/delivery.component';
import { ContactsComponent } from './componentsRoutes/contacts/contacts.component';
import { HttpClientModule } from '@angular/common/http';
import { BakedRollesComponent } from './componentsRoutes/menu/categories/baked-rolles/baked-rolles.component';
import { FriedRollsComponent } from './componentsRoutes/menu/categories/fried-rolls/fried-rolls.component';
import { RollsComponent } from './componentsRoutes/menu/categories/rolls/rolls.component';
import { SetsComponent } from './componentsRoutes/menu/categories/sets/sets.component';
import { SushiComponent } from './componentsRoutes/menu/categories/sushi/sushi.component';
import { PizzaComponent } from './componentsRoutes/menu/categories/pizza/pizza.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { AdminPanelComponent } from './componentsRoutes/admin-panel/admin-panel.component';
import { CategoriesComponent } from './componentsRoutes/admin-panel/menu-list/categories/categories.component';
import { ItemsComponent } from './componentsRoutes/admin-panel/menu-list/items/items.component';
import { ToppingsComponent } from './componentsRoutes/admin-panel/menu-list/toppings/toppings.component';
import { UsersComponent } from './componentsRoutes/admin-panel/menu-list/users/users.component';
import { OrdersComponent } from './componentsRoutes/admin-panel/menu-list/orders/orders.component';
import { ProductComponent } from './componentsRoutes/menu/product/product.component';
import { BreadcrumbService } from './services/breadcrumbService/breadcrumb.service';
import { ProductCardComponent } from './components/productCard/product-card/product-card.component';

const pizzasRoutes: Routes = [
  { path: 'pizza/:id', data: { breadcrumb: 'Товар'}, component: ProductComponent}
]

const menuRoutes: Routes = [
  { path: 'baked-rolls', data: { breadcrumb: 'Запеченые Роллы'}, component: BakedRollesComponent},
  { path: 'fried-rolls', data: { breadcrumb: 'Обжаренные Роллы'}, component: FriedRollsComponent},
  { path: 'rolls', data: { breadcrumb: 'Роллы'}, component: RollsComponent},
  { path: 'sets', data: { breadcrumb: 'Сеты'}, component: SetsComponent},
  { path: 'sushi', data: { breadcrumb: 'Суши'}, component: SushiComponent},
  { path: 'pizzas', data: { breadcrumb: 'Пицца'}, component: PizzaComponent},
  { path: 'pizzas/:id', data: { breadcrumb: 'Пицца'}, component: ProductComponent },
  { path: 'baked-rolls/:id', data: { breadcrumb: 'Запеченые Роллы'}, component: ProductComponent },
  { path: 'fried-rolls/:id', data: { breadcrumb: 'Обжаренные Роллы'}, component: ProductComponent },
  { path: 'rolls/:id', data: { breadcrumb: 'Роллы'}, component: ProductComponent },
  { path: 'sets/:id', data: { breadcrumb: 'Сеты'}, component: ProductComponent },
  { path: 'sushi/:id', data: { breadcrumb: 'Суши'}, component: ProductComponent }
 /*  { path: 'pizzas', data: { breadcrumb: 'Пицца'}, component: PizzaComponent, children: pizzasRoutes} */

]

const adminPanelRoutes: Routes = [
  { path: 'categories', data: { breadcrumb: 'Категории'}, component: CategoriesComponent},
  { path: 'items', data: { breadcrumb: 'Товары'}, component: ItemsComponent},
  { path: 'orders', data: { breadcrumb: 'Заказы'}, component: OrdersComponent},
  { path: 'users', data: { breadcrumb: 'Пользователи'}, component: UsersComponent},
  { path: 'toppings', data: { breadcrumb: 'Топпинги'}, component: ToppingsComponent}
]

const appRoutes: Routes = [
  { path: '', data: { breadcrumb: 'Главная'}, component: HomeComponent},
  { path: 'admin-panel', data: { breadcrumb: 'Админ-панель'}, component: AdminPanelComponent, children: adminPanelRoutes},
  { path: 'menu', data: { breadcrumb: 'Меню'}, component: MenuComponent, children: menuRoutes},
  { path: 'promotions', data: { breadcrumb: 'Акции'}, component: PromotionsComponent},
  { path: 'delivery', data: { breadcrumb: 'Доставка'}, component: DeliveryComponent},
  { path: 'contacts', data: { breadcrumb: 'Контакты'}, component: ContactsComponent},
  { path: '**', redirectTo: '/'}
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    PromotionsComponent,
    DeliveryComponent,
    ContactsComponent,
    BakedRollesComponent,
    FriedRollsComponent,
    RollsComponent,
    SetsComponent,
    SushiComponent,
    PizzaComponent,
    BreadcrumbComponent,
    AdminPanelComponent,
    CategoriesComponent,
    ItemsComponent,
    ToppingsComponent,
    UsersComponent,
    OrdersComponent,
    ProductComponent,
    ProductCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes, {scrollPositionRestoration: 'enabled'}),
    FormsModule,
    HttpClientModule
  ],
  providers: [BreadcrumbService],
  bootstrap: [AppComponent]
})
export class AppModule { }
