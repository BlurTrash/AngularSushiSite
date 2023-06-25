﻿using System;
using System.Collections.Generic;

namespace MyShushiSiteApi.DbModels
{
    /// <summary>
    /// Таблица разделов меню
    /// </summary>
    public partial class Category
    {
        public long Id { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
    }
}
