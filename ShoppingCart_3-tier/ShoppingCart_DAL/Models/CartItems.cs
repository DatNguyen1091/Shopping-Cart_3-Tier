namespace ShoppingCart_DAL.Models
{
    public class CartItems : Common
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public int cartId { get; set; }
        public int productId { get; set; }
        public bool isDeleted { get; set; }
    }
}
