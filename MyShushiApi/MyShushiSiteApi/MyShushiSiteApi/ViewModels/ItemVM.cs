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
}
