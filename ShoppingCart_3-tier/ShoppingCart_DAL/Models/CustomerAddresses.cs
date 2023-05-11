namespace ShoppingCart_DAL.Models
{
    public class CustomerAddresses : Common
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int addressId { get; set; }
    }
}
