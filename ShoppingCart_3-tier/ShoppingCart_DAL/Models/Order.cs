namespace ShoppingCart_DAL.Models
{
    public class Order : Common
    {
        public int id { get; set; }
        public decimal orderTotal { get; set; }
        public decimal orderItemTotal { get; set; }
        public decimal shippingCharge { get; set; }
        public int deliveryAddressId { get; set; }
        public int customerId { get; set; }
        public string? orderStatus { get; set; }
        public bool isDeleted { get; set; }
    }
}
