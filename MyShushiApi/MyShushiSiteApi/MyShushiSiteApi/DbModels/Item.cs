using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Таблица товаров
    /// </summary>
    public partial class Item
    {
        public Item()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string? Desription { get; set; }
        public string Ingredients { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public long? CategoryId { get; set; }
        public string UrlName { get; set; } = null!;
        public decimal Count { get; set; }
        public string? FullUrl { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
