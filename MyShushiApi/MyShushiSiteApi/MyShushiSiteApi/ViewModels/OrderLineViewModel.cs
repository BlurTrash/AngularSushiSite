namespace MyShushiSiteApi.ViewModels
{
    public class OrderLineViewModel
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}
