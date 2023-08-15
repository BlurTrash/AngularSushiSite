# AngularSushiSite - это SPA веб-плижоние сайта по доставки суши (находится в разработке)  
Описание:  
Приложение состоит из 2х частей:  
1)Клиент написанный на Angular с использованием TypeScript и Bootstrap
2)Сервис написанный на .Net 6 - ASP.Net WebApi (REST-full сервис, с Ajax-запросами и CRUD архитектурой), взаимодействующий с  
БД на PostgreSQL (через ORM EntityFramework). Используется подход DB-First (сначало создаются модели-таблицы БД и на их основе  
создаются модели для сервиса)  
  
Приложение на текущий момент находится в разработке (сейчас активно разрабатываю корзину товаров, вдальнейшем добавлю авторизацию  
и доступ к админ-панели только для администраторов, и доработаю саму админ-панель для управления разделами, товарами, и заказами пользователей  
и разработку по второстепенным маршрутам и страницам таких как "Акция" "Дотсавка" и тп)  
  
![mainHeader](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/87e296fc-c74f-4219-a103-98e63f63a8b1)  
Основная страница приложения 
  
![mainBody1](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/3088e288-a366-4025-916f-a5f5de6762f1)   
![mainBody2](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/d2c8a44d-96d0-4843-9cb8-ca9298977f43)  
Несколько элементов главной страницы, на второй слайде изобранеж компонент карточки товара (сам раздел запрашивает у сервера  
и отображает некоторые из последних новинок) вдальнешем тут будет находиться слайдер  

![mainFooter](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/b95086a1-6466-4201-bc77-9370be7557e5)  
Футер плижения  
  
![menu](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/fd27308b-4e47-4276-afdb-d6041b56ac0e)  
Раздел меню  
  
![right-menu](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/cda08299-98af-41e5-9665-958dd47028ab)  
Боковое меню  
  
![card-item-component](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/a9f17146-0553-4341-a5cb-ce9b4e07596c)  
Раздел меню Роллов  
  
![Admin-panel](https://github.com/BlurTrash/AngularSushiSite/assets/69421015/a5b79df0-f68a-43f2-92f5-4c863afde224)  
Админ панель, раздел редактирования товаров  
  
Это часть снимков клиентского приложения, все что касается сервиса находится в папке API  
