namespace MyShushiSiteApi.ViewModels
{
    public class CategoryVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        //public byte[] ImageData { get; set; }
        //public byte[] IconData { get; set; }

        public string ImageData { get; set; }
        public string IconData { get; set; }
        public string FullUrl { get; set; }
    }
}
