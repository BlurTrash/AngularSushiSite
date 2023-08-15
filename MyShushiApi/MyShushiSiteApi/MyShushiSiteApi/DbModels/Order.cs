using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Таблица заказов
    /// </summary>
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public long Id { get; set; }
        public string Address { get; set; } = null!;
        public int PaymentId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? UserId { get; set; }
        public int StatusId { get; set; }
        public string? Comment { get; set; }
        public decimal FullPrice { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPhone { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
