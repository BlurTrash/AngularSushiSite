namespace MyShushiSiteApi.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        //public string ImageUrl { get; set; }
        //public string IconUrl { get; set; }
        public IFormFile? ImageData { get; set; }
        public IFormFile? IconData { get; set; }
    }
}
