using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Позиции заказов
    /// </summary>
    public partial class OrderLine
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
