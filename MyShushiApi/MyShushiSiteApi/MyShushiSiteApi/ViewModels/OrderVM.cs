namespace MyShushiSiteApi.ViewModels
{
    public class OrderVM
    {
        public long Id { get; set; }
        public List<OrderLineVM> OrderLines { get; set; }
        public string Address { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? UserId { get; set; }
        public int StatusId { get; set; } 
        public string? Comment { get; set; }
        public decimal FullPrice { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
    }
}
