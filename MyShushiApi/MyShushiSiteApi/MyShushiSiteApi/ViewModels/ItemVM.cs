using MyShushiSiteApi.DbModels;

namespace MyShushiSiteApi.ViewModels
{
    public class ItemVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Desription { get; set; }
        public string Ingredients { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Count { get; set; } = 1;
        public int CategoryId { get; set; }
        public string FullUrl { get; set; }
    }

    ////тест структура класса заказ
    //public class OrderVMsfs
    //{
    //    public int Id { get; set; }
    //    public List<CartLineVM> CartLines { get; set; } //Список позиций
    //    public string Address { get; set; } //адрес
    //    public int PaymentId { get; set; } //при получении картой | при получении наличными , отдельная таблица для типов оплат?
    //    public int DeliveryMethodId { get; set; } //бесплатная доставка | самовывоз , отдельная таблица для типов доставки?
    //    public int? UserId { get; set; } //id пользователя, может быть нулл если пользователь не зареген
    //    public int StatusId { get; set; } //статус заказа, сформирован | в работе | отработан , отдельная таблица для типов доставки?
    //    public string? Comment { get; set; } //комментарий
    //    public decimal FullPrice { get; set; } //общая стоимость заказа
    //    public string? Email { get; set; }
    //    public string UserName { get; set; }
    //    public string UserPhone { get; set; }
    //}

    ////тест позиция заказа
    //public class CartLineVMsfs
    //{
    //    public Item Item { get; set; } //или ItemId ?
    //    public int Quantity { get; set; } //количество элементов
    //    public decimal Price { get; set; } //общая стоимость позиции
    //    public decimal UnitPrice { get; set; } //текущая цена товара
    //}
}
