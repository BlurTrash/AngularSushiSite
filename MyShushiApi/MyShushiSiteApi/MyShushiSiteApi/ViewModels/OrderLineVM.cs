namespace MyShushiSiteApi.ViewModels
{
    public class OrderLineVM
    {
        public long Id { get; set; }
        public ItemVM Item { get; set; }
        public long OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}
