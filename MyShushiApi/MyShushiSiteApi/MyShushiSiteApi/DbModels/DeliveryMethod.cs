using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Способ доставки
    /// </summary>
    public partial class DeliveryMethod
    {
        public DeliveryMethod()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
