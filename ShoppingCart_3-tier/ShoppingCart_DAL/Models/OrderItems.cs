namespace ShoppingCart_DAL.Models
{
    public class OrderItems : Common
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
    }
}
