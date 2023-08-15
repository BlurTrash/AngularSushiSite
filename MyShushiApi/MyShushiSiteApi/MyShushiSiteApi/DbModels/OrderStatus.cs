using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
